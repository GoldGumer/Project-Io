using Scenes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components;

namespace Objects
{
    class Player : GameObject
    {

        public Player() : base()
        {

        }
        public Player(string _name = default, Scene _scene = default)
        {
            name = _name;
            scene = _scene;
            components = new List<Component>() { new PlayerController(), new Transform(new Vector2(2, 2), new Vector2(1, 1)), new Text("@", "Fonts/Medium Font") };

            Start();
        }
    }
}
