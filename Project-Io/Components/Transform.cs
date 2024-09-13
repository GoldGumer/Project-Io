using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Components
{
    [JsonObject("Transform")]
    internal class Transform : Component
    {
        [JsonProperty("position")]
        public Vector2 position { get; set; }
        [JsonProperty("drawOrder")]
        public int drawOrder { get; set; }

        public Transform()
        {

        }

        public Transform(Vector2 _position = default, int _drawOrder = 0)
        {
            position = _position;
            drawOrder = 0;
        }

        public override void Start()
        {
            
        }

        public override void LateStart()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void LateUpdate()
        {
            
        }
    }
}
