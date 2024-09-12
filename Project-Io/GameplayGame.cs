using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Objects;
using Project_Io.Scenes;

namespace Project_Io
{
    public class GameplayGame : Game
    {
        private GraphicsDeviceManager grDeviceManager;
        private SpriteBatch spriteBatch;

        Vector2Int screenSize;

        Scene currentScene;

        SpriteFont mediumFont;

        public GameplayGame()
        {
            grDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screenSize = new Vector2Int(1920, 1080);

            Camera camera = new Camera("MainCamera", Vector2.Zero, grDeviceManager, screenSize);

            camera.backgroundColour = Color.Black;

            currentScene = new Scene("StartScene", 0);

            currentScene.AddGameObject(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Camera camera = null;
            if (currentScene.FindGameObject<Camera>(out camera))
            {
                GraphicsDevice.Clear(camera.backgroundColour);
            }
            else 
            {
                GraphicsDevice.Clear(Color.AntiqueWhite);
            }



            base.Draw(gameTime);
        }
    }
}
