using System.Collections;
using System.Collections.Generic;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.Components;
using Breath.Entities;
using Breath.Enums;
using Breath.Systems;
using Breath.Utils;
using DinoOtter;
using SFML.Graphics;
using SFML.System;
using Color = DinoOtter.Color;
using Image = DinoOtter.Image;

namespace Breath.Scenes
{
    public class SceneOne : DinoScene
    {
        private Game _game;
        private InputManager _manager;
        private Player _player;
        private Color _color;
        private SoundSystem _soundSystem;
        private Coroutine _coroutine;
        private DinoOtter.Color _color1;
        private List<Square> _squares;


        public SceneOne(Game game, InputManager manager, Coroutine coroutine, SoundSystem soundSystem) : base(
            "SceneOne")
        {
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            _soundSystem = soundSystem;
            _player = new Player(manager, game, game.HalfWidth, game.HalfHeight,8);

            CreateSquares();
            
            var dialog = new Dialogue(game.HalfWidth,300,600,300,"YAAAAAAY",Color.White);
            Add(dialog);
            Add(_player);
            
        }

        private void CreateSquares()
        {
            
            _squares = new List<Square>();
            
            for (int i = 0; i < 9; i++)
            {
                var x = 180 + 200 * i;
                
                var e = new Square(x,800,180,180,System.Drawing.Color.White,_game,_manager);
                _squares.Add(e);
                Add(e);
            }
            
        }


        public override void Update()
        {
            
        }

      
        public override void Render()
        {
            
        }
    }
}