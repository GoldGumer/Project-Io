using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Components;
using System.Linq;
using Project_Io.Scenes;


namespace Objects
{
    internal class GameObject
    {
        public string name { get; set; }
        public Scene scene { get; set; }
        protected List<Component> components;

        public GameObject(string _name = "DefaultGameObject", List<Component> _components = default)
        {
            components = _components;

            name = _name;

            if (components.Count > 0)
            {
                foreach (var component in components)
                {
                    component.gameObject = this;

                    component.Start();
                }

                foreach (var component in components)
                {
                    component.LateStart();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (components.Count > 0)
            {
                foreach (var component in components)
                {
                    component.Update(gameTime);
                }

                foreach (var component in components)
                {
                    component.LateUpdate(gameTime);
                }
            }
        }

        public void AddComponent(Component component)
        {
            component.Start();
            component.LateStart();

            components.Add(component);
        }

        public void RemoveComponent(Component component) { components.Remove(component); }

        public T FindComponent<T>()
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public List<T> FindComponents<T>()
        {
            return components.OfType<T>().ToList();
        }
    }
}
