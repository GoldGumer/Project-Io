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
        int cursorPos = 0;
        object currentDisplayedObject;

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

            currentDisplayedObject = sceneManager.GetCurrentScene();
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

            if (sceneManager.IsShowingSceneObjects)
            {
                cursorPos += (InputHandler.IsKeyPressed(Keys.Down) ? 1 : 0) + ((InputHandler.IsKeyPressed(Keys.Up)) ? -1 : 0);

                cursorPos = Math.Clamp(cursorPos, 0, currentDisplayedObject.GetType().GetProperties().Length - 1);

                if (InputHandler.IsKeyPressed(Keys.Enter))
                {
                    if (currentDisplayedObject.GetType().GetProperties()[cursorPos].GetType().IsGenericType)
                    {
                        currentDisplayedObject = currentDisplayedObject.GetType().GetProperties()[cursorPos];
                    }
                }
            }

            sceneManager.GetCurrentScene().Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (sceneManager.IsShowingSceneObjects)
            {
                DrawObject(currentDisplayedObject);
            }
            else
            {
                sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }


        void DrawObject(object obj)
        {
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Medium Font");

            List<string> strings = new List<string>();

            int i = 0;

            foreach (var prop in obj.GetType().GetProperties())
            {
                string stringToAdd = string.Format("{0}| {1} = {2}", i, prop.Name, prop.GetValue(obj, null));

                if (cursorPos == i)
                {
                    stringToAdd += "<";
                }

                strings.Add(stringToAdd);

                i++;
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
                    new Vector2(j * font.MeasureString("1").X * 50, i * font.LineSpacing),
                    Color.White);
                i++;

                if (i > strings.Count())
                {
                    j++;
                    i = 0;
                }
            }
        }
    }
}
