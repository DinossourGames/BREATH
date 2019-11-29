using DinoOtter;

namespace Breath.Entities
{
    public class Dialogue : Entity
    {
        private RichText _texto;
        private float xpos;
        private float ypos;

        public Dialogue(float x, float y, int w,int h,string text,Color color) : base(x,y)
        {
            xpos = x;
            ypos = y;
            //Graphic = Image.CreateRectangle(w,h,color);
            //Graphic.CenterOrigin();
            _texto = new RichText(text, 48) {Color = Color.White};
            _texto.SetPosition(x,y);
            AddGraphic(_texto);
        }

        public void SetText(string text, Color color = null)
        {
            _texto.String = text;
            _texto.Color = color ?? Color.White;
            _texto.TextAlign = TextAlign.Center;
            _texto.SetPosition(xpos,ypos);

        }
        
    }
}