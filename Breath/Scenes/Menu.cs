using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.DataStructs;
using Breath.Entities;
using Breath.Systems;
using Colorful;
using DinoOtter;
using Console = System.Console;

namespace Breath.Scenes
{
    public class Menu : DinoScene, IMenuBindings
    {
        private Input _input;
        private Game _game;
        private LoopList<ButtonEntity> _selectables;
        private Key _selectKey;
        private InputManager _manager;
        private Coroutine _coroutine;
        private SoundSystem _soundSystem;

        public Menu(Input input, Game game,InputManager manager,Coroutine coroutine,SoundSystem soundSystem) : base("Menu")
        {
            _soundSystem = soundSystem;
            _input = input;
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            BindEvents();
            _coroutine.Start(ActivateInput());
        }

        IEnumerator ActivateInput()
        {
            yield return _coroutine.WaitForFrames(60);
            _manager.CanReceiveInput = true;
        }
        
        private void BindEvents()
        {
            _manager.Next += OnNext;
            _manager.Previous += OnPrevious;
            _manager.Select += OnSelect;

            OnEnd += () =>
            {
                _manager.Next -= OnNext;
                _manager.Previous -= OnPrevious;
                _manager.Select -= OnSelect;
            };
        }

        public override void Start()
        {
            _game.Color = Color.FromDraw(System.Drawing.Color.FromArgb(42, 42, 42));
            _selectKey = Key.Return;
            
           var btnStart = new ButtonEntity(_game.HalfWidth, _game.HalfHeight,null,null,"Start");
          var btnOptions = new ButtonEntity(_game.HalfWidth, _game.HalfHeight,null,null,"Options");
           var btnQuit = new ButtonEntity(_game.HalfWidth, _game.HalfHeight + 100,null,null,"Quit");

           btnStart.ClickHandler.MouseClick += button => SceneManager.LoadScene("SceneOne");
           
           btnQuit.ClickHandler.MouseClick += button => _game.Close();
           
           _selectables = new LoopList<ButtonEntity>(new List<ButtonEntity>{btnStart,btnOptions,btnQuit});
           
           AddMultiple(btnStart, btnQuit);
           
        }
        
        public void OnNext() => _selectables.NextItem();

        public void OnPrevious() => _selectables.PreviousItem();

        public void OnSelect() => _selectables.GetItem().ClickHandler.PerformClick();
    }
}