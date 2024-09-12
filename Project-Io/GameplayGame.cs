using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Objects;
using Project_Io.Scenes;
using System.Collections.Generic;

namespace Project_Io
{
    public class GameplayGame : Game
    {
        Point screenSize;

        Scene currentScene;

        public GameplayGame()
        {
            screenSize = new Point(1920, 1080);

            Camera camera = new Camera(this, screenSize, new Vector2(16, 9));
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            camera.backgroundColour = Color.Black;

            Transform transform = new Transform(Vector2.Zero);

            List<Component> cameraComponents = new List<Component>() { camera, transform };
            currentScene = new Scene("StartScene", 0);
            currentScene.AddGameObject(new GameObject("MainCamera", cameraComponents));
        }

        protected override void Initialize()
        {
            currentScene.FindGameObjectWithComponent<Camera>().FindComponent<Camera>().graphicsDevice = GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteFont mediumFont = Content.Load<SpriteFont>("Fonts/Medium Font");

            currentScene.FindGameObjectWithComponent<Camera>().FindComponent<Camera>().spriteBatch = new SpriteBatch(GraphicsDevice);

            currentScene.AddGameObject(new GameObject("TestGameObject", new List<Component>() { new Transform(new Vector2(3, 2)), new Text("Test", mediumFont), new PlayerController() }));
            currentScene.AddGameObject(new GameObject("Test2GameObject", new List<Component>() { new Transform(new Vector2(-3, -2)), new Text("Test2", mediumFont) }));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            currentScene.FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw();

            base.Draw(gameTime);
        }
    }
}
