using System;
using System.Collections;
using System.Collections.Generic;
using Breath.Enums;
using Breath.Systems;
using DinoOtter;

namespace Breath.Entities
{
    public class Square : Entity
    {
  
        private readonly BoxCollider _collider;
        private Coroutine _coroutine;
        private InputManager _manager;

        public bool IsPressed;
        
        public Square(float x, float y, int w, int h, System.Drawing.Color color,Game game,InputManager manager) : base(x, y)
        {
            _manager = manager;
            _coroutine = game.Coroutine;
            Graphic = Image.CreateRectangle(w, h, Color.FromDraw(color));
            Graphic.CenterOrigin();
            _collider = new BoxCollider(w, h);
            AddCollider(_collider);
            Collider.CenterOrigin();

        }

        public void SetColor(Color color)
        {
            Graphic.Color = color;
            ColorReference = color;
        }

        public Color ColorReference { get; set; }

        public override void Update()
        {
            if (_collider.Overlap(X, Y, Tags.Player))
            {
                IsPressed = true;
                _manager.SetLightbar(System.Drawing.Color.FromArgb(Graphic.Color.ByteA,Graphic.Color.ByteR,Graphic.Color.ByteG,Graphic.Color.ByteB));
            }
            else
            {
                IsPressed = false;
            }
        }

    
    }
}