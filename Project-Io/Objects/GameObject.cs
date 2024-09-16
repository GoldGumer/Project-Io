using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Components;
using System.Linq;
using Scenes;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Graphics;
using EditModeUI;


namespace Objects
{
    [EditUIDropDown("Create/GameObject")]
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
            name = "NewGameObject";
            components = new List<Component>() { new Transform() };
        }

        public GameObject(Scene _scene)
        {
            name = "NewGameObject";
            components = new List<Component>() { new Transform(), new Text("Default", scene.sceneManager.game.Content.Load<SpriteFont>("Fonts/Medium Font")) };
        }

        public GameObject(string _name, Scene _scene, List<Component> _components)
        {
            components = _components;
            scene = _scene;
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
