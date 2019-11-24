using System;
using Breath.Abstractions.Classes;
using Breath.Systems;
using DinoOtter;
using Console = Colorful.Console;
namespace Breath.Scenes
{
    public class Options : DinoScene
    {
        private Game _game;
        private Input _input;

        public Options(Game game,Input input) : base("Options")
        {
            _game = game;
            _input = input;
        }

        public override void Start()
        {
            _game.Color = Color.Magenta;
            Console.WriteLine("Options Loaded",System.Drawing.Color.Green);
        }

        public override void Update()
        {
            if(_input.KeyPressed(Key.Space))
                 SceneManager.LoadScene("Menu");
        }
    }
}