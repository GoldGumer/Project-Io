﻿using Components;
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
    internal class GameplayGame : IoGame
    {
        public GameplayGame()
        {
            screenSize = new Point(1920, 1080);


            grDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager = new SceneManager(this, 0, new List<Scene>());

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

            sceneManager.GetCurrentScene().Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            sceneManager.GetCurrentScene().FindGameObjectWithComponent<Camera>().FindComponent<Camera>().Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
