using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Objects;
using Scenes;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Project_Io
{
    struct KeyboardHandler
    {
        public static KeyboardState currentKeyboard;
        static KeyboardState previousKeyboard;

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

        EditModeUI.EditUIDropDownCollection editModeUICollection;

        public GameplayGame()
        {
            screenSize = new Point(1920, 1080);

            
            grDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            editModeUICollection = new EditModeUI.EditUIDropDownCollection();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager = new SceneManager(this, 0, new List<Scene>());
            Scene scene = new Scene("StartScene", sceneManager, 0);
            scene.AddGameObject(new GameObject("MainCamera", scene, new List<Component>() { new Camera(new Vector2(16, 9), Color.Black), new Transform(Vector2.Zero) }));

            sceneManager.AddScene(scene);

            sceneManager.LoadScenesFromJSON(Path.Combine(Content.RootDirectory, @"JSON Files\Scenes.json"));

            Camera camera = sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>();

            camera.grDeviceManager = grDeviceManager;
            camera.graphicsDevice = GraphicsDevice;
            camera.spriteBatch = new SpriteBatch(GraphicsDevice);

            camera.UpdateBackBufferSize(screenSize);


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

            sceneManager.GetCurrentScene().Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw();

            base.Draw(gameTime);
        }
    }
}
