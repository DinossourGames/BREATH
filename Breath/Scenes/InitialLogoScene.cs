using Breath.Abstractions.Classes;
using Breath.Systems;
using DinoOtter;

namespace Breath.Scenes
{
    public class InitialLogoScene : DinoScene
    {
        private float _timer = 0;

        public InitialLogoScene() : base("START")
        {
        }

        public override void Start()
        {
            Game.Instance.Color = Color.Black;
           
        }

        public override void Update()
        {
            
            _timer += Game.Instance.RealDeltaTime;
            
            if(_timer > 300)
                SceneManager.LoadScene("Menu");
        }
    }
}