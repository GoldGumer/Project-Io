using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Objects;
using Scenes;
using System;
using System.Collections.Generic;

namespace Components
{
    [JsonObject("Camera")]
    internal class Camera : Component
    {
        [JsonIgnore]
        public GraphicsDeviceManager grDeviceManager { get; set; }
        [JsonIgnore]
        public GraphicsDevice graphicsDevice { get; set; }
        [JsonIgnore]
        public SpriteBatch spriteBatch { get; set; }
        [JsonProperty("viewSize")]
        public Vector2 viewSize { get; set; }
        [JsonProperty("backgroundColour")]
        public Color backgroundColour { get; set; }

        public Camera(Point screenSize = default, Vector2 _viewSize = default)
        {
            viewSize = new Vector2(16, 9);

            UpdateBackBufferSize(screenSize);
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
