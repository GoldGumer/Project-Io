using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditModeUI
{
    internal class EditUIDropDownCollection
    {
        Type[] gameObjects;

        public EditUIDropDownCollection()
        {
            gameObjects = GetTypes(Assembly.GetAssembly(this.GetType())).ToArray();

            
        }

        IEnumerable<Type> GetTypes(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(EditUIDropDownAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }
    }
}
