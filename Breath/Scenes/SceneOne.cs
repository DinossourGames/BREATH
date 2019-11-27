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


        public SceneOne(Game game, InputManager manager, Coroutine coroutine, SoundSystem soundSystem) : base(
            "SceneOne")
        {
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            _soundSystem = soundSystem;
            _player = new Player(manager, game, 100, game.HalfHeight, 10);
            _color = Color.Black;

            var _ground = new Ground(game.HalfWidth, game.Height - 50, game.Width, 50);
            var trigger = new Square(900,900,50,50,System.Drawing.Color.Transparent,game,First(),manager);
            //var trigger2 = new Square(1900,900,50,50,System.Drawing.Color.Transparent,game,LoadSeccond());

            var bg = new Image(BasePath.Images("trees.png"));
            var bg1 = new Image(BasePath.Images("Sky.png"));
            var land = new Image(BasePath.Images("land1.png"));

            land.Scale = 10;
            land.Y = -500;
            bg1.Scale = 10;
            bg1.Y = -500;
            bg.Scale = 10;
            bg.Y = -600;


            AddGraphic(bg1);
            AddGraphic(bg);
            AddGraphic(land);

            Add(_ground);
            Add(_player);
            Add(trigger);
         //   Add(trigger2);

         CameraFocus = _player;


        }

        IEnumerator LoadSeccond()
        {
            SceneManager.LoadScene("SceneTwo");
            yield return null;
        }
        IEnumerator First()
        {

        yield return _coroutine.WaitForFrames(1);

        }
        public override void Update()
        {
            
        }

      
        public override void Render()
        {
            
        }
    }
}