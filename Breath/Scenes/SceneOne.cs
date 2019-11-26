using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.Components;
using Breath.Entities;
using Breath.Systems;
using DinoOtter;
using SFML.Graphics;
using SFML.System;
using Color = DinoOtter.Color;

namespace Breath.Scenes
{
    public class SceneOne : DinoScene, IGameplayBinds
    {
        private Game _game;
        private InputManager _manager; 
        private Player _player;
        private Color _color;
        private SoundSystem _soundSystem;



        public SceneOne(Game game, InputManager manager,SoundSystem soundSystem) : base("SceneOne")
        {
            _game = game;
            _manager = manager;
            _soundSystem = soundSystem;
            _player = new Player(manager,game,game.HalfWidth,game.HalfHeight);
            _color = Color.Black;
           Add(_player);
        
        }

     

        public void Move(Vector2 input)
        {
            _player.X += input.X;
            _player.Y += input.Y;
        }

        public void Breath(Vector2 input)
        {
            _manager.Rumble((byte) input.X, (byte) input.Y);
            _color.R = input.X / 255f;
            _color.B = input.Y / 255f;
            _manager.SetLightbar(System.Drawing.Color.FromArgb(_color.ByteA,_color.ByteR,_color.ByteG,_color.ByteB));
        }

        public void Jump()
        {
           
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
        }
    }
}