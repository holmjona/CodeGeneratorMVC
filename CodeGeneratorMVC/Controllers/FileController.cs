﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeGeneratorMVC.Controllers {
    public class FileController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile fupFile, string uniquekey) {
            Project thisProject = new Project() { Key = uniquekey };
            List<ProjectFile> thisFiles = new List<ProjectFile>();
            string uploadFolder = Path.GetFullPath("Uploads") + "\\" + uniquekey + "\\";
            string filePath = uploadFolder + fupFile.FileName;
            // create directory for upload
            Directory.CreateDirectory(uploadFolder);
            // 
            string saveFolderPath = Path.GetFullPath("Projects") + @"\" + thisProject.Key;
            // copy uploaded file to upload directory.
            using (FileStream fs = new FileStream(filePath, FileMode.Create)) {
                fupFile.CopyTo(fs);
                fs.Close();
            }
            List<string> messages = new List<string>();
            List<ProjectClass> classes = SQLScriptConversion.generateObjects(filePath, ref messages);
            ProjectVariable nameSpaceObject = new ProjectVariable(1, "CodeGeneratorObjects");
            DALClass dalClassObject = getNewDAL(nameSpaceObject);
            if (!Directory.Exists(saveFolderPath)) {
                // no directory; create it
                Directory.CreateDirectory(saveFolderPath);
            }
            StringBuilder sprocScripts = new StringBuilder();
            foreach (ProjectClass pClass in classes) {
                String fileName = pClass.Name.Capitalized() + ".cs";
                pClass.DALClassVariable = dalClassObject;
                pClass.NameSpaceVariable = nameSpaceObject;
                string classContent = ClassGenerator.getEntireClass(pClass, "Me", CodeGeneration.Language.CSharp, CodeGeneration.Format.ASPX, ref messages);
                // save file to project folder. 
                using (StreamWriter sw = new StreamWriter(saveFolderPath + @"\" + fileName, false)) {
                    sw.Write(classContent);
                }
                thisFiles.Add(new ProjectFile() {
                    Name = fileName, PhysicalPath = saveFolderPath + @"\",
                    Project = thisProject
                });

                //sprocScripts.Append(StoredProcsGenerator.getAddSprocText(pClass));
            }

            //TODO: Not doing Sprocs yet, Still need to figure out how to get them to work.
            //using (StreamWriter sw = new StreamWriter(saveFolderPath + @"\StoredProcedures.sql", false)) {
            //    sw.Write(sprocScripts.ToString());
            //    thisFiles.Add(new ProjectFile() {
            //        Name = "StoredProcedures.sql", PhysicalPath = saveFolderPath + @"\",
            //        Project = thisProject
            //    });
            //}

            thisProject.Files = thisFiles;
            thisProject.ConversionMessages = messages;
            //return View(thisProject);

            //https://stackoverflow.com/questions/13510204/json-net-self-referencing-loop-detected
            TempData["CurrentProject"] = Newtonsoft.Json.JsonConvert.SerializeObject(thisProject,
                new Newtonsoft.Json.JsonSerializerSettings() {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return RedirectToAction("List", new { uniquekey = thisProject.Key });
        }

        private DALClass getNewDAL(ProjectVariable nameSpace) {
            ConnectionString readConn = new ConnectionString(1, "Reader", "db_reader");
            ConnectionString editConn = new ConnectionString(2, "Editor", "db_editor");
            DALClass dal = new DALClass();
            dal.NameSpaceName = nameSpace;
            dal.Name = "NewDAL";
            dal.ReadOnlyConnectionString = readConn;
            dal.EditOnlyConnectionstring = editConn;

            return dal;
        }

        public IActionResult List(string uniquekey) {
            Project myProject = null;
            if (TempData["CurrentProject"] != null) {
                myProject = Newtonsoft.Json.JsonConvert.DeserializeObject<Project>(TempData["CurrentProject"].ToString());
                foreach(ProjectFile pf in myProject.Files) {
                    pf.Project = myProject;
                }
            } else {
                string projectFolder = Path.GetFullPath("Projects\\" + uniquekey);
                // get files for project
                if (Directory.Exists(projectFolder)) {

                    // build project
                    myProject = new Project();
                    myProject.Key = uniquekey;

                    // add files to project
                    string[] fileNames = Directory.GetFiles(projectFolder);
                    foreach (string fileName in fileNames) {
                        ProjectFile pf = new ProjectFile();
                        pf.Name = Path.GetFileName(fileName);
                        pf.PhysicalPath = fileName;
                        pf.Project = myProject;
                        myProject.Files.Add(pf);
                    }
                } else {
                    // project does not exist.
                    return RedirectToAction("Index","Home");
                }
            }

            return View(myProject);
        }


        [HttpGet]
        public IActionResult Download(string file) {
            string fullPath = Path.GetFullPath("Projects") + @"\" + file;
            FileInfo fInfo = new FileInfo(fullPath);

            byte[] fileData = new byte[fInfo.Length];
            using (FileStream fs = fInfo.OpenRead()) {
                fs.Read(fileData);
            }

            ContentDisposition content = new ContentDisposition();
            content.FileName = fInfo.Name;
            content.Inline = false;

            Response.Headers.Add("Content-Disposition", content.ToString());
            return File(fileData, "text/text");
        }
    }
}
