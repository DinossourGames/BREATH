using System.Collections.Generic;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.DataStructs;
using Breath.Entities;
using Breath.Systems;
using Colorful;
using DinoOtter;

namespace Breath.Scenes
{
    public class Menu : DinoScene
    {
        private Input _input;
        private Game _game;
        private LoopList<ButtonEntity> _selectables;

        public Menu(Input input, Game game) : base("Menu")
        {
            _input = input;
            _game = game;
        }

        public override void Start()
        {
            _game.Color = Color.FromDraw(System.Drawing.Color.FromArgb(42, 42, 42));
            
            
           var btnStart = new ButtonEntity(_game.HalfWidth, 300,null,null,"Start");
           var btnOptions = new ButtonEntity(_game.HalfWidth, 400,null,null,"Options");
           var btnQuit = new ButtonEntity(_game.HalfWidth, 500,null,null,"Quit");

           btnStart.ClickHandler.MouseClick += button => SceneManager.LoadScene("Scene1"); 
           btnOptions.ClickHandler.MouseClick += button => SceneManager.LoadScene("Options"); 
           btnQuit.ClickHandler.MouseClick += button => _game.Close();
           
           _selectables = new LoopList<ButtonEntity>(new List<ButtonEntity>{btnStart,btnOptions,btnQuit});
           
           AddMultiple(btnStart, btnOptions, btnQuit);
        }

        public override void Update()
        {
            
            if (_input.KeyPressed(Key.Down))
                _selectables.NextItem();
            if (_input.KeyPressed(Key.Up))
                _selectables.PreviousItem();
            if (_input.KeyPressed(Key.Return))
                _selectables.GetItem().ClickHandler.PerformClick();
            
        }
    }
}