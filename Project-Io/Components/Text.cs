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
        [JsonProperty("directory")]
        public string fontDirectory { get; set; }
        [JsonIgnore]
        public SpriteFont font { get; set; }

        public Text()
        {
            text = "Default";
        }

        public Text(string _text = default, string _fontDirectory = default)
        {
            text = _text;
            fontDirectory = _fontDirectory;
        }

        public override void Start()
        {
            font = gameObject.scene.sceneManager.game.Content.Load<SpriteFont>(fontDirectory);
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
