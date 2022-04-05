using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Helpers
{
    public class FileManager
    {
        public static string Save(string folderpath,IFormFile file,string wwrooth)
        {
            string newfilename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(wwrooth, folderpath, newfilename);
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return newfilename;
        }
        public static bool Delete(string folderpath, string file, string wwwroot)
        {
            string path = Path.Combine(wwwroot, folderpath, file);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
