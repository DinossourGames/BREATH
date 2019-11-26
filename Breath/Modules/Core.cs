using Breath.Abstractions.Interfaces;
using Breath.Components;
using Breath.Scenes;
using Breath.Systems;
using DinoOtter;
using Ninject;
using Ninject.Modules;

namespace Breath.Modules
{
    public class Core : NinjectModule
    {
        public override void Load()
        {
            var game = new Game("BREATH",1920,1080,60,false)
            {
                Color = Color.FromDraw(System.Drawing.Color.FromArgb(52, 56, 69)),
                MouseVisible = true,
                FirstScene = SceneManager.FirstScene,
                //EnableQuitButton = false
            };
            
            Bind<Game>().ToConstant(game);
            Bind<Input>().ToConstant(game.Input);
            Bind<Coroutine>().ToConstant(game.Coroutine);
            Bind<InputManager>().ToConstant(new InputManager(game, game.Input));
            Bind<SoundSystem>().ToConstant(new SoundSystem(game));
            
            Bind<IClickHandler>().To<ClickHandler>();
        }
    }
}