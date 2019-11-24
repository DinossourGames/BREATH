using Breath.Scenes;
using Breath.Systems;
using DinoOtter;
using Ninject.Modules;

namespace Breath.Modules
{
    public class Core : NinjectModule
    {
        public override void Load()
        {
            var game = new Game("BREATH",1280,720,60,false)
            {
                Color = Color.FromDraw(System.Drawing.Color.FromArgb(52, 56, 69)),
                MouseVisible = true,
                FirstScene = SceneManager.FirstScene
            };
            
            Bind<Game>().ToConstant(game);
            Bind<Input>().ToConstant(game.Input);
        }
    }
}