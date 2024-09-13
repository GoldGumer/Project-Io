using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Components
{
    internal class PlayerController : Component
    {

        public override void Start()
        {

        }
        public override void LateStart()
        {
            
        }
        public override void Update()
        {
            Vector2 inputDirection = new Vector2
                (
                (Project_Io.KeyboardHandler.IsKeyPressed(Keys.Right) ? 1 : 0) -
                (Project_Io.KeyboardHandler.IsKeyPressed(Keys.Left) ? 1 : 0),
                (Project_Io.KeyboardHandler.IsKeyPressed(Keys.Up) ? 1 : 0) -
                (Project_Io.KeyboardHandler.IsKeyPressed(Keys.Down) ? 1 : 0)
                );

            Vector2 newPosition = gameObject.FindComponent<Transform>().position + inputDirection;

            gameObject.FindComponent<Transform>().position = newPosition;
        }
        public override void LateUpdate()
        {
            
        }
    }
}
