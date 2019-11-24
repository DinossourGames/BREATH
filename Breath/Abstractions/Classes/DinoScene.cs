using System;
using Breath.Systems;
using DinoOtter;

namespace Breath.Abstractions.Classes
{
    public abstract class DinoScene : Scene, IDisposable
    {
        public string Name { get; set; }
        
        protected DinoScene(string name)
        {
            Name = name.Replace(" ","");
            SceneManager.AddScene(this);
        }

        public virtual void Dispose() => SceneManager.RemoveScene(Name);
        
    }
}