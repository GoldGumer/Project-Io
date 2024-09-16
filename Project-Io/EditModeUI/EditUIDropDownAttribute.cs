using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditModeUI
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class EditUIDropDownAttribute : Attribute
    {
        string directory;

        public EditUIDropDownAttribute(string _directory)
        {
            directory = _directory;
        }

        public string[] GetMenuDirectory()
        {
            return directory.Split('/');
        }
    }
}
