using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DropDownMenu
{
    internal class DropDownEditModeContainer
    {
        Type[] elements;

        private DropDownEditModeContainer()
        {
            elements = GetElements().ToArray();
        }

        private IEnumerable<Type> GetElements() 
        {
            Assembly assembly = typeof(DropDownEditModeContainer).Assembly;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(DropDownEditModeElement), true).Length > 0)
                {
                    yield return type;
                }
            }
        }
    }
}
