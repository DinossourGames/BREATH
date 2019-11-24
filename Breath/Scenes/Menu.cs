using Breath.Abstractions.Classes;
using Breath.Systems;
using Colorful;
using DinoOtter;

namespace Breath.Scenes
{
    public class Menu : DinoScene
    {
        private Input _input;
        private Game _game;

        public Menu(Input input,Game game) : base("Menu")
        {
            _input = input;
            _game = game;
        }

        public override void Start()
        {
            Console.WriteLine("Menu Loaded",System.Drawing.Color.Green);
            _game.Color = Color.Gray;
        }

        public override void Update()
        {
            if (_input.KeyPressed(Key.Space))
                SceneManager.LoadScene("Options");
        }
    }
}