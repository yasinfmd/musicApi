using MusicApp.Helper.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicApp.Helper.Concrate
{
    public class HelperManager : IHelper
    {
        public string UploadStorage(string name,string fileName )
        {

            string fName = Path.GetFileNameWithoutExtension(name);
            string extension = Path.GetExtension(fileName);
            fName = fName + "_" + Guid.NewGuid().ToString() + extension;
            string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\", fName);

            return path;

            // string fName = Path.GetFileNameWithoutExtension(item.Name);
            // string extension = Path.GetExtension(item.FileName);

            // string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
            //pathBuild= Path.Combine(pathBuild, fileName);
        }
    }
}
