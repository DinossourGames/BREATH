using System;
using Breath.Abstractions.Classes;
using Breath.Systems;
using DinoOtter;
using Console = Colorful.Console;
using Ninject;
using Color = System.Drawing.Color;

namespace Breath.Scenes
{
    public class DebugScene : DinoScene
    {
        private Game _game;
        private Text _text;

        public DebugScene(Game game,string name) : base("Debug Scene")
        {
            Name = "DEBUG";
            _game = game;
            _text = new Text($"Scene {name} Was not Found on list", 32);
        }

        public override void Start()
        {
            _text.SetPosition(_game.HalfWidth, _game.HalfHeight);
            _text.CenterTextOrigin();
            AddGraphic(_text);
            Console.WriteLine(_text.String, Color.FromArgb(255, 0, 3));
            _game.Color = DinoOtter.Color.Red;

        }
    }
}