using Breath.Abstractions.Classes;
using Breath.Entities;
using Colorful;
using DinoOtter;

namespace Breath.Scenes
{
    public class Menu : DinoScene
    {
        private Input _input;
        private Game _game;

        public Menu(Input input, Game game) : base("Menu")
        {
            _input = input;
            _game = game;
        }

        public override void Start()
        {
            Console.WriteLine("Menu Loaded", System.Drawing.Color.Green);
            _game.Color = Color.Gray;

            var btn = new ButtonEntity(100, 100, 100, 100);
            btn.ClickHandler.MouseClick +=
                button => _game.Color = Color.Random;

            Add(btn);
        }
    }
}