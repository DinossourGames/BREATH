using System;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.Systems;
using DinoOtter;
using Console = Colorful.Console;

namespace Breath.Scenes
{
    public class Options : DinoScene, IControlBinds
    {
        private Game _game;
        private Input _input;
        private InputManager _manager;

        public Options(Game game, Input input, InputManager manager) : base("Options")
        {
            _game = game;
            _input = input;
            _manager = manager;

           BindActions();
        }

        private void BindActions()
        {
            _manager.Pause += Pause;
            _manager.Menu += Menu;
            
            OnEnd += () =>
            {
                _manager.Pause -= Pause;
                _manager.Menu -= Menu;
            };
        }


        public override void Update()
        {
            if (_input.KeyPressed(Key.Space))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        void IControlBinds.Pause() => _game.PauseToggle();
        public void Menu() => SceneManager.LoadScene("Menu");
    }
}