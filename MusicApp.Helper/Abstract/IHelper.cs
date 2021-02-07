using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Helper.Abstract
{
    public interface IHelper
    {
        string UploadStorage(string name, string fileName);
        void LoginHelper();
    }
}
