using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Objects;
using Project_Io;
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
        [JsonProperty("viewSize")]
        public Vector2 viewSize { get; set; }
        [JsonProperty("backgroundColour")]
        public Color backgroundColour { get; set; }

        public Camera()
        {
            viewSize = new Vector2(1, 1);
            backgroundColour = Color.BlanchedAlmond;
        }

        public Camera(Vector2 _viewSize, Color _backgroundColour)
        {
            viewSize = new Vector2(16, 9);

            backgroundColour = _backgroundColour;
        }


        public override void Start()
        {
            grDeviceManager = gameObject.scene.sceneManager.game.grDeviceManager;
            graphicsDevice = gameObject.scene.sceneManager.game.GraphicsDevice;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(backgroundColour);

            List<GameObject> gameObjects = gameObject.scene.FindGameObjectsWithComponent<Text>();

            if (gameObjects.Count > 0)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    spriteBatch.DrawString(gameObject.FindComponent<Text>().font,
                        gameObject.FindComponent<Text>().text,
                        WorldToScreen(gameObject.FindComponent<Transform>().position),
                        Color.White,
                        gameObject.FindComponent<Transform>().rotation,
                        gameObject.FindComponent<Transform>().pivot,
                        gameObject.FindComponent<Transform>().scale,
                        new SpriteEffects(),
                        gameObject.FindComponent<Transform>().drawOrder);
                }
            }
        }
    }
}
