﻿using DinoOtter;

namespace Breath.Entities
{
    public class Dialogue : Entity
    {
        public Dialogue(float x, float y, int w,int h,string text,Color color) : base(x,y)
        {
            Graphic = Image.CreateRectangle(w,h,color);
            Graphic.CenterOrigin();
            var texto = new Text(text,32);
            texto.CenterTextOrigin();
            

            
            AddGraphic(texto);
        }
        
    }
}