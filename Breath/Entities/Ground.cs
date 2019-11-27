using System.Drawing;
using Breath.Enums;
using DinoOtter;
using Color = DinoOtter.Color;

namespace Breath.Entities
{
    public class Ground : Entity
    {
        public Ground(float x, float y, int w, int h) : base(x,y)
        {
            Collider = new BoxCollider(w,h,Tags.Ground);
            Collider.CenterOrigin();
            Graphic = Image.CreateRectangle(w,h,Color.Black);
            Graphic.CenterOrigin();
        }

        public override void Render()
        {
            Collider.Render();
        }
    }
}