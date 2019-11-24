using System;
using DinoOtter;

namespace Breath.Abstractions.Interfaces
{
    public interface IClickHandler : IDisposable, ISelectable
    {
        event Action<MouseButton> MouseClick;
        event Action OnHoverStartEvent;
        event Action OnHoverEndEvent;
        void OnClick(MouseButton buttonPressed);
        void OnHoverEnter();
        void OnHoverExit();
        void PerformClick();
    }
}