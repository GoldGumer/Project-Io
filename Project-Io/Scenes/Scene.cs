using Microsoft.Xna.Framework;
using Objects;
using Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Io.Scenes
{
    internal class Scene
    {
        protected string name;
        protected int id;
        protected List<GameObject> gameObjects;

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

        public void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in gameObjects) 
            { 
                gameObject.Update(gameTime);
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

        public List<GameObject> GetGameObjects() { return gameObjects; }
    }
}
