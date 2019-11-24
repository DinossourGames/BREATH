using System.Collections;
using System.Collections.Generic;
using Breath.Abstractions.Interfaces;
using Breath.Systems;
using DinoOtter;
using Ninject;
using Color = System.Drawing.Color;

namespace Breath.Entities
{
    public class ButtonEntity : Entity
    {
        public string Text
        {
            get => _text.String;
            set => _text.String = value;
        }

        private RichText _text;
        private Coroutine _coroutine;
        public IClickHandler ClickHandler { get; set; }

        public ButtonEntity(int? fontSize = null, string font = null, string text = null)
        {
            ClickHandler = GameManager.Kernel.Get<IClickHandler>();
            _coroutine = GameManager.Kernel.Get<Coroutine>();

            if (text != null)
            {
                _text = new RichText(text, font ?? "Fonts/BACKTO1982.TTF", fontSize ?? 32);
                _text.CenterOrigin();

                _text.TextAlign = TextAlign.Center;
                _text.Color = DinoOtter.Color.White;
                _text.DefaultOutlineColor = DinoOtter.Color.Black;
                _text.DefaultOutlineThickness = 3;
                _coroutine.Start(DefineText());
            }
            
          
        }

        public ButtonEntity(float x, float y,int w,int h, Color color,Image graphic = null) : this()
        {
            Graphic = graphic ?? Image.CreateRectangle(w, h, DinoOtter.Color.FromDraw(color));
            Graphic.CenterOrigin();
            X = x;
            Y = y;
        }

        public ButtonEntity(float x, float y, int? fontSize, string? font, string text,
            Image bgImage = null) : this(fontSize, font, text)
        {
            AddGraphic(bgImage ?? Image.CreateRectangle((int)_text.ScaledWidth + 40, (int)_text.ScaledHeight + 40, DinoOtter.Color.FromDraw(Color.Transparent)));
            Graphic.CenterOrigin();
            X = x;
            Y = y;
            
        }

        IEnumerator DefineText()
        {
            yield return _coroutine.WaitForFrames(10);
            _text.OffsetY = -2;
            AddGraphic(_text);
        }

        public override void Start()
        {
            AddComponent((Component) ClickHandler);
        }
    }
}