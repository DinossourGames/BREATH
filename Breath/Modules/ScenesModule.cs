using Breath.Abstractions.Classes;
using Breath.Scenes;
using Ninject.Modules;

namespace Breath.Modules
{
    public class ScenesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DinoScene>().To<Menu>().Named("Menu");
            Bind<DinoScene>().To<SceneOne>().Named("SceneOne");
        }
    }
}