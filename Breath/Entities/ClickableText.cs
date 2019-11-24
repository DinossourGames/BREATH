using Breath.Abstractions.Interfaces;
using Breath.Systems;
using DinoOtter;
using Ninject;
using Color = System.Drawing.Color;

namespace Breath.Entities
{
    public class ClickableText : Entity
    {
        public ClickableText(float x, float y, string text, int fontSize = 16, Color fillColor = default,
            Color outlineColor = default) 
        {
            X = x;
            Y = y;
            var txt = new Text(text, fontSize);
            txt.CenterOrigin();
            
            if (fillColor != default)
                txt.Color = DinoOtter.Color.FromDraw(fillColor);
            if (outlineColor != default)
                txt.Color = DinoOtter.Color.FromDraw(outlineColor);
            
            AddGraphic(txt);
        }
    }
}