using DinoOtter;

namespace Breath.Entities
{
    public class Square : Entity
    {
        public Square(float x, float y, int w, int h, System.Drawing.Color color,Game game) : base(x, y)
        {
            Graphic = Image.CreateRectangle(w, h, Color.FromDraw(color));
            Graphic.CenterOrigin();
            Collider = new BoxCollider(w, h);
            Collider.CenterOrigin();

        }
    }
}