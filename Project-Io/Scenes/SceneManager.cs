using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scenes
{
    internal class SceneManager
    {
        int currentSceneID;

        List<Scene> scenes;

        public Game game { get; }

        public SceneManager(Game _game, int _currentSceneID = 0, List<Scene> _scenes = default)
        {
            game = _game;
            currentSceneID = _currentSceneID;
            scenes = _scenes;
        }

        public Scene GetCurrentScene() 
        {
            return scenes.Find(scene => scene.id == currentSceneID);
        }

        public void LoadNextScene()
        {
            currentSceneID++;
        }

        public void LoadPreviousScene()
        {
            currentSceneID--;
        }

        public void LoadScene(int sceneID)
        {
            currentSceneID = scenes.Find(scene => scene.id == sceneID).id;
        }

        public void AddScene(Scene scene)
        {
            scene.sceneManager = this;
            scenes.Add(scene);
        }

        public void RemoveScene(Scene scene) 
        {
            scenes.Remove(scene);
        }

        public void SaveScenesToJSON(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                string JSONToWrite = JsonConvert.SerializeObject(scenes, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
                sw.WriteLine(JSONToWrite);
            }
        }

        public void LoadScenesFromJSON(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                scenes = JsonConvert.DeserializeObject<List<Scene>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            }

            foreach (Scene scene in scenes) { scene.sceneManager = this; }
        }
    }
}
