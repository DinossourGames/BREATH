using System;
using Breath.Abstractions.Interfaces;
using Breath.DataStructs;
using DinoOtter;

namespace Breath.Components
{
    public class ClickHandler : Component, IClickHandler
    {
        private readonly Input _input;
        private bool _isPressed;
        private bool _isPressedLastFrame;
        private bool _isHovered;
        public StateColors StateColors { get; set; }
        private Graphic _sprite;

        public event Action<MouseButton> MouseClick = delegate { };
        public event Action OnHoverStartEvent = delegate { };
        public event Action OnHoverEndEvent = delegate { };

        public ClickHandler(Input input)
        {
            _input = input;
            MouseClick += OnClick;
            OnHoverStartEvent += OnHoverEnter;
            OnHoverEndEvent += OnHoverExit;
        }


         public virtual void OnClick(MouseButton buttonPressed)
        {
        }

        public virtual void OnHoverEnter()
        {
        }

        public virtual void OnHoverExit()
        {
        }

        public override void UpdateFirst()
        {
            _sprite = Entity.Graphic;
            _isPressedLastFrame = _isPressed;
        }

        public override void Update()
        {
            UpdateState();

            if (_isPressed)
            {
                _sprite.Color = StateColors.ClickedColor ?? _sprite.Color;
                if (!_isPressedLastFrame)
                    MouseClick?.Invoke(_input.LastMouseButton);
            }
            else if (_isHovered)
                _sprite.Color = StateColors.HoveredColor ?? _sprite.Color;
            else
                _sprite.Color = StateColors.DefaultColor ?? _sprite.Color;
        }


        private void UpdateState()
        {
            if(Entity.Graphic == null)
                return;
            
            if (_input.MouseScreenX > Entity.X - _sprite.HalfWidth &&
                _input.MouseScreenX < Entity.X + _sprite.HalfWidth &&
                _input.MouseScreenY > Entity.Y - _sprite.HalfHeight &&
                _input.MouseScreenY < Entity.Y + _sprite.HalfHeight)
            {
                if (!_isHovered)
                    OnHoverStartEvent?.Invoke();
                _isHovered = true;
                _isPressed = _input.MouseButtonDown(MouseButton.Any);
            }
            else
            {
                if (_isHovered)
                    OnHoverEndEvent?.Invoke();
                _isHovered = false;
            }
        }

        public void Dispose()
        {
            MouseClick -= OnClick;
            OnHoverStartEvent -= OnHoverEnter;
            OnHoverEndEvent -= OnHoverExit;
        }
    }
}