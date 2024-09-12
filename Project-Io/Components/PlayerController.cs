using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Components
{
    internal class PlayerController : Component
    {
        KeyboardState currentKeyboard;
        KeyboardState previousKeyboard;


        public override void Start()
        {
            currentKeyboard = Keyboard.GetState();
            previousKeyboard = new KeyboardState();
        }
        public override void LateStart()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            previousKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();

            Vector2 inputDirection = new Vector2
                (
                (IsKeyPressed(Keys.Right) ? 1 : 0) -
                (IsKeyPressed(Keys.Left) ? 1 : 0),
                (IsKeyPressed(Keys.Up) ? 1 : 0) -
                (IsKeyPressed(Keys.Down) ? 1 : 0)
                );

            Vector2 newPosition = gameObject.FindComponent<Transform>().position + inputDirection;

            gameObject.FindComponent<Transform>().position = newPosition;
        }
        public override void LateUpdate(GameTime gameTime)
        {
            
        }

        private bool IsKeyPressed(Keys key)
        {
            if (currentKeyboard.IsKeyDown(key) && !previousKeyboard.IsKeyDown(key)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
