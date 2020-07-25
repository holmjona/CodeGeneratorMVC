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
            foreach(ProjectClass pClass in classes) {
                messages.Add(ClassGenerator.getEntireClass(pClass,"Me", CodeGeneration.Language.CSharp,CodeGeneration.Format.ASPX, ref messages));
            }
            return View(messages);
        }
    }
}
