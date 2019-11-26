using Breath.Abstractions.Classes;
using Breath.Systems;
using DinoOtter;

namespace Breath.Scenes
{
    public class InitialLogoScene : DinoScene
    {
        private float _timer = 0;
        private Image _logo;
        private float scale = .3f;

        public InitialLogoScene() : base("START")
        {
            
        }

        public override void Start()
        {
            Game.Instance.Color = Color.Black;
            _logo = new Image("Assets/Images/logo.png");
            _logo.SetPosition(Game.HalfWidth,Game.HalfHeight);
            _logo.Scale = .6f;
            _logo.Alpha = 0;
            _logo.CenterOrigin();
            AddGraphic(_logo);
        }

        public override void Update()
        {
            
            _timer += Game.Instance.RealDeltaTime;

            if (_timer > 1000)
            {
                if (scale < .6f)
                    _logo.Alpha += Game.RealDeltaTime / 1000;
                scale += scale < .6f ? Game.RealDeltaTime / 10000 : 0;
                if (scale >= .6f)
                    _logo.Alpha -= Game.RealDeltaTime / 1000;
            }

            if(_timer > 300) //TODO: 5000
                SceneManager.LoadScene("Menu");
        }
    }
}