using Breath.Abstractions.Classes;
using Breath.Systems;
using DinoOtter;

namespace Breath.Scenes
{
    public class InitialLogoScene : DinoScene
    {
        private float timer = 0;

        public InitialLogoScene() : base("START")
        {
        }

        public override void Start()
        {
            Game.Instance.Color = Color.Black;
        }

        public override void Update()
        {
            timer += Game.Instance.RealDeltaTime;
            
            if(timer > 5000)
                SceneManager.LoadScene("Menu");
        }
    }
}