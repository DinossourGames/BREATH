using System;
using System.Collections.Generic;
using System.Linq;
using Breath.Abstractions.Classes;
using Breath.Scenes;
using DinoOtter;
using Ninject;
using Color = System.Drawing.Color;
using Console = Colorful.Console;

namespace Breath.Systems
{
    public static class SceneManager
    {
        private static Dictionary<string, DinoScene> _scenes;
        private static Game _game;
        private static IReadOnlyKernel _kernel;

        public static DinoScene FirstScene => new InitialLogoScene();
        private static DinoScene ActiveScene { get; set; }

        private static Dictionary<string, DinoScene> Scenes
        {
            get
            {
                if (_scenes == null)
                    _scenes = new Dictionary<string, DinoScene>();
                return _scenes;
            }
        }

        public static void Initialize(IReadOnlyKernel kernel)
        {
            _game = kernel.Get<Game>();
            _kernel = kernel;
        }


        public static void AddScene(DinoScene scene) => Scenes.TryAdd(scene.Name, scene);

        public static bool RemoveScene(string scene) => Scenes.Remove(scene);

        public static void KeepStateLoadScene(string name)
        {
            _game.RemoveScene();
            if (!Scenes.ContainsKey(name))
            {
                var sc = _kernel.Get<DinoScene>(name);
                AddScene(sc);
            }

            ActiveScene = Scenes.ContainsKey(name) ? Scenes[name] : new DebugScene(_game, name);

            
            if (!_game.Scenes.Contains(ActiveScene))
                _game.AddScene(ActiveScene);
            
            _game.SwitchScene(ActiveScene);
        }

        public static void LoadScene(string name)
        {

            try
            {
                ActiveScene = _kernel.Get<DinoScene>(name);

            }
            catch 
            {
                ActiveScene = new DebugScene(_game,name);
            }

            _game.SwitchScene(ActiveScene);
        }
    }
}