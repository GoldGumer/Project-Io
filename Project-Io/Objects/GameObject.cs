using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Components;
using System.Linq;
using Scenes;
using Newtonsoft.Json;


namespace Objects
{
    [JsonObject("GameObject")]
    internal class GameObject
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("scene")]
        public Scene scene { get; set; }
        [JsonProperty("components")]
        protected List<Component> components { get; set; }

        public GameObject()
        {

        }

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

        public void Update()
        {
            if (components.Count > 0)
            {
                foreach (var component in components)
                {
                    component.Update();
                }

                foreach (var component in components)
                {
                    component.LateUpdate();
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
