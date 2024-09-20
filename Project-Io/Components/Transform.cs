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
        [JsonProperty("scale")]
        public Vector2 scale { get; set; }
        [JsonProperty("rotation")]
        public float rotation { get; set; }
        [JsonProperty("pivot")]
        public Vector2 pivot { get; set; }
        [JsonProperty("drawOrder")]
        public int drawOrder { get; set; }

        public Transform()
        {
            position = Vector2.Zero;
            drawOrder = 0;
        }

        public Transform(Vector2 _position = default, Vector2 _scale = default, float _rotation = default, Vector2 _pivot = default, int _drawOrder = 0)
        {
            position = _position;
            scale = _scale;
            rotation = _rotation;
            pivot = _pivot;
            drawOrder = _drawOrder;
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
