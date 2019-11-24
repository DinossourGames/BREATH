using System;
using Breath.Systems;
using DinoOtter;
using Color = System.Drawing.Color;
using Console = Colorful.Console;

namespace Breath.Abstractions.Classes
{
    public abstract class DinoScene : Scene, IDisposable
    {
        public string Name { get; set; }
        
        protected DinoScene(string name)
        {
            Console.WriteLine($"Scene {name} Loaded Successfully",Color.Green);
            Name = name.Replace(" ","");
            SceneManager.AddScene(this);
        }

        public virtual void Dispose() => SceneManager.RemoveScene(Name);
        
    }
}