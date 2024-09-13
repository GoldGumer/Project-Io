using Microsoft.Xna.Framework;
using Objects;
using Components;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scenes
{
    [JsonObject("Scene")]
    internal class Scene
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("gameObjects")]
        public List<GameObject> gameObjects { get; set; }

        public Scene()
        {

        }

        public Scene(string _name = "DefaultScene", int _id = -1)
        {
            name = _name;
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
