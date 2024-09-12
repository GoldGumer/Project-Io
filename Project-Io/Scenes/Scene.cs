using Microsoft.Xna.Framework;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject) 
        {
            gameObjects.Remove(gameObject);
        }

        public bool FindGameObject(Vector2 position, out GameObject gameObjectFound)
        {
            gameObjectFound = null;

            foreach (GameObject gameObject in gameObjects) 
            {
                if (gameObject.GetPosition() == position)
                {
                    gameObjectFound = gameObject;

                    return true;
                }
            }

            return false;
        }

        public bool FindGameObject<T>(out T gameObjectFound)
        {
            gameObjectFound = default(T);

            List<T> typeList = FindGameObjects<T>();

            if (typeList.Count > 0)
            {
                gameObjectFound = typeList[0];

                return true;
            }

            return false;
        }

        public List<T> FindGameObjects<T>()
        {
            return gameObjects.OfType<T>().ToList();
        }
    }
}
