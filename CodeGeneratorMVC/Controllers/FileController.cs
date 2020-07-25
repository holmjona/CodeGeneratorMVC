using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeGeneratorMVC.Controllers {
    public class FileController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile fupFile) {
            string fPath = Path.GetFullPath("Uploads")+ "\\" + fupFile.FileName;
            using (FileStream fs = new FileStream(fPath , FileMode.Create)) {
                fupFile.CopyTo(fs);
                fs.Close();
            }
            List<string> messages = new List<string>();
            List<ProjectClass> classes = SQLScriptConversion.generateObjects(fPath,ref messages);
            ProjectVariable nameSpaceObject = new ProjectVariable(1, "CodeGeneratorObjects");
            DALClass dalClassObject = getNewDAL(nameSpaceObject);
            foreach(ProjectClass pClass in classes) {
                pClass.DALClassVariable = dalClassObject;
                pClass.NameSpaceVariable = nameSpaceObject;
                messages.Add(ClassGenerator.getEntireClass(pClass,"Me", CodeGeneration.Language.CSharp,CodeGeneration.Format.ASPX, ref messages));
            }
            return View(messages);
        }

        private DALClass getNewDAL(ProjectVariable nameSpace) {
            ConnectionString readConn = new ConnectionString(1, "Reader", "db_reader");
            ConnectionString editConn = new ConnectionString(2, "Editor", "db_editor");
                        DALClass dal =  new DALClass();
            dal.NameSpaceName = nameSpace;
            dal.Name = "NewDAL";
            dal.ReadOnlyConnectionString = readConn;
            dal.EditOnlyConnectionstring = editConn;
           
            return dal;
        }
    }
}
