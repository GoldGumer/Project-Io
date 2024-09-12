using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    internal abstract class Component
    {
        public abstract void Start();
        public abstract void LateStart();
        public abstract void Update();
        public abstract void LateUpdate();
    }
}
