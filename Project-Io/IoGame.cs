using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Io
{
    struct InputHandler
    {
        public static KeyboardState currentKeyboard;
        static KeyboardState previousKeyboard;

        public static MouseState currentMouse;
        static MouseState previousMouse;

        public static void Start()
        {
            currentKeyboard = Keyboard.GetState();
            previousKeyboard = new KeyboardState();

            currentMouse = Mouse.GetState();
            previousMouse = new MouseState();
        }

        public static void Update()
        {
            previousKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();

            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
        }

        public static bool IsKeyPressed(Keys key)
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

        public static bool IsMouseButtonPressed(int mouseButton)
        {
            switch (mouseButton)
            {
                case 0:
                    if (currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Released)
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (currentMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton != ButtonState.Released)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (currentMouse.MiddleButton == ButtonState.Pressed && previousMouse.MiddleButton != ButtonState.Released)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }

    internal class IoGame : Game
    {
        public GraphicsDeviceManager grDeviceManager { get; protected set; }

        protected SpriteBatch spriteBatch;

        public Point screenSize { get; protected set; }

        protected SceneManager sceneManager;
    }
}
