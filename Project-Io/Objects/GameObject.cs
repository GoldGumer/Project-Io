using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Components;


namespace Objects
{
    internal class GameObject
    {
        public string name { get; set; }
        protected Vector2 position;
        protected List<Component> components;

        public GameObject(string _name = "DefaultGameObject", Vector2 _position = default)
        {
            components = new List<Component>();

            name = _name;
            if (_position == default) 
            { 
                position = Vector2.Zero;
            }
            else
            {
                position = _position;
            }

            if (components.Count > 0)
            {
                foreach (var component in components)
                {
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

        public Vector2 GetPosition() { return position; }
    }
}
