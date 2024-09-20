using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Objects;
using Scenes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Project_Io
{
    internal class EditModeGame : IoGame
    {
        EditModeUI.EditUIDropDownCollection editModeUICollection;

        public EditModeGame()
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
            camera.UpdateBackBufferSize(screenSize);


            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            if (InputHandler.IsKeyPressed(Keys.Tab) && InputHandler.currentKeyboard.IsKeyDown(Keys.LeftShift))
            {
                sceneManager.IsShowingSceneObjects = !sceneManager.IsShowingSceneObjects;
            }

            sceneManager.GetCurrentScene().Update();

            base.Update(gameTime);
        }

        void DrawObject(object obj)
        {
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Medium Font");

            List<string> strings = new List<string>();

            foreach (var prop in obj.GetType().GetProperties())
            {
                Type testtype = prop.PropertyType;

                if (prop.PropertyType.IsGenericType)
                {
                    strings.Add(prop.Name + " = {");

                    var whatever = prop.PropertyType.GenericTypeArguments;

                    object listObj = prop.GetValue(obj, null);

                    strings.Add(whatever.ToString());

                    strings.Add("}");
                }
                else
                {
                    strings.Add(prop.Name + " = " + prop.GetValue(obj, null));
                }
            }

            DrawStrings(strings);
        }

        void DrawStrings(IEnumerable<string> strings)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteFont font = Content.Load<SpriteFont>("Fonts/Medium Font");

            int i = 0, j = 0;
            while (strings.Count() > (i + (j * strings.Count())))
            {
                spriteBatch.DrawString(
                    font,
                    strings.ToArray()[i + (j * strings.Count())],
                    new Vector2(j * font.MeasureString("50===+====+====+====+====+====+====+====+====+====").X, i * font.LineSpacing),
                    Color.White);
                i++;

                if (i > strings.Count())
                {
                    j++;
                    i = 0;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (sceneManager.IsShowingSceneObjects)
            {
                DrawObject(sceneManager.GetCurrentScene());
            }
            else
            {
                sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
