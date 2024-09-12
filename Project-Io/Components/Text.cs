using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    internal class Text : Component
    {
        public string text { get; set; }
        public SpriteFont font { get; set; }

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
        public override void Update(GameTime gameTime)
        {

        }
        public override void LateUpdate(GameTime gameTime)
        {
            
        }
    }
}
