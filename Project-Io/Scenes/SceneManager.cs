using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scenes
{
    internal class SceneManager
    {
        int currentSceneID;

        List<Scene> scenes;

        public SceneManager(int _currentSceneID = 0, List<Scene> _scenes = default)
        {
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

        public void UpdateCurrentScene()
        {
            GetCurrentScene().Update();
        }

        public void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public void RemoveScene(Scene scene) 
        {
            scenes.Remove(scene);
        }

        public void SaveScenesToJSON(string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                serializer.Serialize(file, scenes);
            }
        }

        public void LoadScenesFromJSON(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                scenes = JsonConvert.DeserializeObject<List<Scene>>(json);
            }
        }
    }
}
