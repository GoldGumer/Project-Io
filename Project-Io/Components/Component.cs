using Microsoft.Xna.Framework;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    internal abstract class Component
    {
        public GameObject gameObject { get; set; }

        public abstract void Start();
        public abstract void LateStart();
        public abstract void Update(GameTime gameTime);
        public abstract void LateUpdate(GameTime gameTime);
    }
}
