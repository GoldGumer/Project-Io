using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Objects;
using Project_Io.Scenes;
using System;
using System.Collections.Generic;

namespace Components
{
    internal class Camera : Component
    {
        public GraphicsDeviceManager grDeviceManager;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;
        public Vector2 viewSize;
        public Color backgroundColour;

        public Camera(Game game = default, Point screenSize = default, Vector2 _viewSize = default)
        {
            grDeviceManager = new GraphicsDeviceManager(game);

            viewSize = new Vector2(16, 9);

            UpdateBackBufferSize(screenSize);
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

        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            Vector2 topLeft = new Vector2(
                gameObject.FindComponent<Transform>().position.X - (viewSize.X / 2),
                gameObject.FindComponent<Transform>().position.Y + (viewSize.Y / 2)
                );

            Vector2 vector = new Vector2(
                (worldPos.X - topLeft.X) * (grDeviceManager.PreferredBackBufferWidth / viewSize.X),
                (- (worldPos.Y - topLeft.Y)) * (grDeviceManager.PreferredBackBufferHeight / viewSize.Y)
                );

            return vector;
        }

        public void UpdateBackBufferSize(Point screenSize)
        {
            grDeviceManager.PreferredBackBufferWidth = screenSize.X;
            grDeviceManager.PreferredBackBufferHeight = screenSize.Y;

            grDeviceManager.ApplyChanges();
        }

        public void Draw()
        {
            graphicsDevice.Clear(backgroundColour);

            spriteBatch.Begin();

            List<GameObject> gameObjects = gameObject.scene.FindGameObjectsWithComponent<Text>();

            if (gameObjects.Count > 0)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    spriteBatch.DrawString(gameObject.FindComponent<Text>().font, gameObject.FindComponent<Text>().text, WorldToScreen(gameObject.FindComponent<Transform>().position), Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}
