using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    struct Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    internal class Camera : GameObject
    {
        protected GraphicsDeviceManager grDeviceManager;
        protected Vector2 viewSize;
        public Color backgroundColour;

        public Camera(string _name = "DefaultCameraObject", Vector2 _position = default, GraphicsDeviceManager graphics = default, Vector2Int screenSize = default) : base(_name, _position)
        {
            grDeviceManager = graphics;

            viewSize = new Vector2(screenSize.x, screenSize.y);

            UpdateBackBufferSize(screenSize);
        }

        public void UpdateBackBufferSize(Vector2Int screenSize)
        {
            grDeviceManager.PreferredBackBufferWidth = screenSize.x;
            grDeviceManager.PreferredBackBufferHeight = screenSize.y;

            grDeviceManager.ApplyChanges();
        }

        public void Draw()
        {

        }
    }
}
