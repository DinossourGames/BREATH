using System.Collections;
using Breath.Abstractions.Classes;
using Breath.Entities;
using Breath.Systems;
using Breath.Utils;
using DinoOtter;

namespace Breath.Scenes
{
    public class SceneTwo : DinoScene
    {
        private InputManager _manager;
        private Game _game;
        private Coroutine _coroutine;
        private SoundSystem _soundSystem;
        private Player _player;
        private Color _color;

        public SceneTwo(Game game, InputManager manager, Coroutine coroutine, SoundSystem soundSystem) : base("SceneTwo")
        {
            
            
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            _soundSystem = soundSystem;
            _player = new Player(manager, game, 100, game.HalfHeight, 10);
            _color = Color.Black;

            var _ground = new Ground(game.HalfWidth, game.Height - 50, game.Width, 50);
//            var trigger = new Square(900,900,50,50,System.Drawing.Color.Transparent,game,First());
//            var trigger2 = new Square(1900,900,50,50,System.Drawing.Color.Transparent,game,LoadSeccond());

            var bg = new Image(BasePath.Images("trees.png"));
            var bg1 = new Image(BasePath.Images("bgCave.jpg"));
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
//            Add(_player);
//            Add(trigger);
//            Add(trigger2);
        }
        
        IEnumerator LoadSeccond()
        {
            SceneManager.LoadScene("SceneTwo");
            yield return null;
        }
        IEnumerator First()
        {
           // var dialog = new Dialogue(_game.HalfWidth, 50, 500, 200, "Respira");
       //     Add(dialog);

            _player._isBreathing = true;
            _player.canMove = false;
            
            for (int i = 0; i < 16; i++)
            {
                CameraZoom += .001f;
                yield return _coroutine.WaitForFrames(2);
            }
            
            yield return _coroutine.WaitForSeconds(3);
         //   Remove(dialog);
            _player._isBreathing = false;
            _player.canMove = true;

        }
        
    }
}