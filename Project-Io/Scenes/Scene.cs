using Microsoft.Xna.Framework;
using Objects;
using Components;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Scenes
{
    [JsonObject("Scene")]
    internal class Scene
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonIgnore]
        public SceneManager sceneManager { get; set; }
        [JsonProperty("gameObjects")]
        public List<GameObject> gameObjects { get; set; }

        public Scene()
        {
            name = "NewScene";
            id = -1;
        }

        public Scene(string _name, SceneManager _sceneManager, int _id)
        {
            name = _name;
            sceneManager = _sceneManager;
            id = _id;
            gameObjects = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects) 
            {
                gameObject.scene = this;
            }
        }

        public void Update()
        {
            foreach (GameObject gameObject in gameObjects) 
            { 
                gameObject.Update();
            }
        }

        public void DrawSceneContent(SpriteBatch spriteBatch)
        {
            SpriteFont font = sceneManager.game.Content.Load<SpriteFont>("Fonts/Medium Font");

            string[,] strings = new string[
                sceneManager.game.grDeviceManager.PreferredBackBufferHeight / font.LineSpacing,
                sceneManager.game.grDeviceManager.PreferredBackBufferWidth / (int)font.MeasureString("25===+====+====+====+====").X];

            int i = 0, j = 0;
            foreach (GameObject gameObject in gameObjects)
            {
                strings[i, j] = gameObject.name;

                i++;

                if (i > strings.GetLength(0))
                {
                    j++;
                    i = 0;
                }

                if (strings.Length > (i + (j * strings.GetLength(0))))
                {
                    break;
                }
            }

            DrawStrings(spriteBatch, strings);
        }

        public void DrawStrings(SpriteBatch spriteBatch, string[,] strings)
        {
            sceneManager.game.GraphicsDevice.Clear(Color.Black);

            SpriteFont font = sceneManager.game.Content.Load<SpriteFont>("Fonts/Medium Font");

            int i = 0, j = 0;
            while (strings.Length > (i + (j * strings.GetLength(0))))
            {
                spriteBatch.DrawString(
                    font,
                    strings[i, j],
                    new Vector2(j * font.MeasureString("25===+====+====+====+====").X, i * font.LineSpacing),
                    Color.White);
                i++;

                if (i > strings.GetLength(0))
                {
                    j++;
                    i = 0;
                }
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.scene = this;
            gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject) 
        {
            gameObjects.Remove(gameObject);
        }

        public GameObject FindGameObjectWithComponent<T>()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.FindComponent<T>() != null && !gameObject.FindComponent<T>().Equals(default(T)))
                {
                    return gameObject;
                }
            }

            return default;
        }

        public List<GameObject> FindGameObjectsWithComponent<T>()
        {
            List<GameObject> gameObjectsToReturn = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.FindComponent<T>() != null && !gameObject.FindComponent<T>().Equals(default(T)))
                {
                    gameObjectsToReturn.Add(gameObject);
                }
            }

            return gameObjectsToReturn;
        }

        public T FindGameObject<T>()
        {
            return gameObjects.OfType<T>().FirstOrDefault();
        }

        public List<T> FindGameObjects<T>()
        {
            return gameObjects.OfType<T>().ToList();
        }
    }
}
