using DinoOtter;

namespace Breath.Abstractions.Interfaces
{
    public interface IGameplayBinds
    {
        void Move(Vector2 input);
        void Breath(Vector2 input);
        void Jump();
        void Interact();
        void Shoot();
    }
}