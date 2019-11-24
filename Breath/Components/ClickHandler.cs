using System;
using Breath.Abstractions.Interfaces;
using Breath.Entities;
using DinoOtter;
using Ninject;

namespace Breath.Components
{
    public class ClickHandler : Component, IClickHandler
    {
        private readonly Input _input;
        private bool _isPressed;
        private bool _isPressedLastFrame;
        private bool _isHovered;
        private Graphic _sprite;
        private Graphic _shaderSprite = null;
        private Shader _shader;
        private bool _isSelected;

        public event Action<MouseButton> MouseClick = delegate { };
        public event Action OnHoverStartEvent = delegate { };
        public event Action OnHoverEndEvent = delegate { };

        public ClickHandler(Input input)
        {
            _input = input;
            MouseClick += OnClick;
            OnHoverStartEvent += OnHoverEnter;
            OnHoverEndEvent += OnHoverExit;
            _shader = new Shader("Shaders/Frags/hover.frag");
        }


        public void Select()
        {
            _isSelected = !_isSelected;
        }

        public virtual void OnClick(MouseButton buttonPressed)
        {
            Select();
        }

        public void PerformClick() => MouseClick?.Invoke(MouseButton.Any);

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
                _shader.SetParameter("alpha", .5f);

                if (!_isPressedLastFrame)
                    MouseClick?.Invoke(_input.LastMouseButton);
            }
            else if (_isHovered)
                _shader.SetParameter("alpha", .3f);
            else if (_isSelected)
                _shader.SetParameter("alpha", .1f);
            else
                _shader.SetParameter("alpha", 0f);
        }

        private void UpdateState()
        {
            if (Entity.Graphic == null)
                return;

            _shaderSprite ??= Image.CreateRectangle(_sprite.Width , _sprite.Height , Color.White);
            _shaderSprite.CenterOrigin();

            if (!Entity.Graphics.Contains(_shaderSprite))
            {
                _shaderSprite.Shader = _shader;
                Entity.AddGraphic(_shaderSprite);
            }

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