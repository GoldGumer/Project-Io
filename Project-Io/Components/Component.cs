using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    [JsonObject("Component")]
    internal abstract class Component
    {
        [JsonProperty("gameObject")]
        public GameObject gameObject { get; set; }

        public abstract void Start();
        public abstract void LateStart();
        public abstract void Update();
        public abstract void LateUpdate();
    }
}
