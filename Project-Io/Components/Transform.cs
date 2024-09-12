using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Components
{
    internal class Transform : Component
    {
        public Vector2 position { get; set; }
        public int drawOrder { get; set; }

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

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void LateUpdate(GameTime gameTime)
        {
            
        }
    }
}
