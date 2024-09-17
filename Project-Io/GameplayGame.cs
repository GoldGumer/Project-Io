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


            InputHandler.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Update();

            if (InputHandler.IsKeyPressed(Keys.S) && InputHandler.currentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                sceneManager.SaveScenesToJSON(Path.Combine(Content.RootDirectory, @"JSON Files\Scenes.json"));
            }

            if (InputHandler.IsKeyPressed(Keys.O))
            {
                object obj = editModeUICollection.GetFirstGameObject().GetConstructor(new System.Type[] { }).Invoke(new object[] { });

                if (obj.GetType() == typeof(GameObject))
                {
                    sceneManager.GetCurrentScene().AddGameObject((GameObject)obj);
                }
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
