using DinoOtter;
using Ninject;

namespace Breath.Systems
{
    public class GameManager
    {
        public static IReadOnlyKernel Kernel { get; private set; }
        
        public GameManager(IReadOnlyKernel kernel)
        {
            Kernel = kernel;
            SceneManager.Initialize(kernel);
        }

        public void Run() => Kernel.Get<Game>().Start();
    }
}