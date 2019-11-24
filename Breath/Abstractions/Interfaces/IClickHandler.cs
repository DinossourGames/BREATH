using System;
using Breath.DataStructs;
using DinoOtter;

namespace Breath.Abstractions.Interfaces
{
    public interface IClickHandler : IDisposable
    {
        StateColors StateColors { get; set; }
        event Action<MouseButton> MouseClick;
        event Action OnHoverStartEvent;
        event Action OnHoverEndEvent;
        void OnClick(MouseButton buttonPressed);
        void OnHoverEnter();
        void OnHoverExit();
    }
}