using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Objects;
using Scenes;
using System.Collections.Generic;
using System.IO;

namespace Project_Io
{
    struct KeyboardHandler
    {
        public static KeyboardState currentKeyboard;
        public static KeyboardState previousKeyboard;

        public static void Start()
        {
            currentKeyboard = Keyboard.GetState();
            previousKeyboard = new KeyboardState();
        }

        public static void Update()
        {
            previousKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
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
    }

    public class GameplayGame : Game
    {
        public GraphicsDeviceManager grDeviceManager { get; set; }

        Point screenSize;

        SceneManager sceneManager;

        public GameplayGame()
        {
            screenSize = new Point(1920, 1080);
            grDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            sceneManager = new SceneManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager.LoadScenesFromJSON(Path.Combine(Content.RootDirectory, @"JSON Files\Scenes.json"));

            Camera camera = sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>();

            camera.grDeviceManager = grDeviceManager;
            camera.graphicsDevice = GraphicsDevice;


            KeyboardHandler.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardHandler.Update();

            if (KeyboardHandler.IsKeyPressed(Keys.S) && KeyboardHandler.currentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                sceneManager.SaveScenesToJSON(Path.Combine(Content.RootDirectory, @"JSON Files\Scenes.json"));
            }

            sceneManager.UpdateCurrentScene();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw();

            base.Draw(gameTime);
        }
    }
}
