using Breath.Abstractions.Interfaces;
using Breath.Systems;
using DinoOtter;
using Ninject;
using Color = System.Drawing.Color;

namespace Breath.Entities
{
    public class ButtonEntity : Entity
    {
       public IClickHandler ClickHandler { get; set; }

        public ButtonEntity() => ClickHandler = GameManager.Kernel.Get<IClickHandler>();

        public ButtonEntity(float x, float y, Image graphic = null) : this()
        {
            Graphic = graphic ?? Image.CreateRectangle(300, 100);
            Graphic.CenterOrigin();
            X = x;
            Y = y;
        }

        public ButtonEntity(float x, float y, int w, int h, Image graphic = null) : this()
        {
            Graphic = graphic ?? Image.CreateRectangle(w, h);
            Graphic.CenterOrigin();
            X = x;
            Y = y;
        }

        public override void Start()
        {
            AddComponent((Component) ClickHandler);
        }
        
    }
}