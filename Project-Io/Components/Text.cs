using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Components
{
    [JsonObject("Text")]
    internal class Text : Component
    {
        [JsonProperty("text")]
        public string text { get; set; }
        [JsonProperty("font")]
        public SpriteFont font { get; set; }

        public Text()
        {

        }

        public Text(string _text = "DEFAULT", SpriteFont _font = default)
        {
            text = _text;
            font = _font;
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
