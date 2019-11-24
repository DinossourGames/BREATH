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
            _game.Color = Color.FromDraw(System.Drawing.Color.FromArgb(42, 42, 42));
           var btn = new ButtonEntity(_game.HalfWidth, 150, 300,
               100,System.Drawing.Color.Orchid);
           btn.ClickHandler.MouseClick += button => btn.ClickHandler.Select();
        
            Add(btn);
        }
    }
}