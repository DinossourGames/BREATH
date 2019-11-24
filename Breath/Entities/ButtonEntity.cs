using Breath.Abstractions.Interfaces;
using Breath.DataStructs;
using Breath.Systems;
using DinoOtter;
using Ninject;
using Color = System.Drawing.Color;

namespace Breath.Entities
{
    public class ButtonEntity : Entity
    {
        private StateColors? _stateColors;

        public IClickHandler ClickHandler { get; set; }

        public ButtonEntity() => ClickHandler = GameManager.Kernel.Get<IClickHandler>();

        public ButtonEntity(float x, float y, StateColors? colors = null, Image graphic = null) : this()
        {
            Graphic = graphic ?? Image.CreateRectangle(300, 100);
            Graphic.CenterOrigin();
            X = x;
            Y = y;
            _stateColors = colors;
        }

        public ButtonEntity(float x, float y, int w, int h, StateColors? colors = null, Image graphic = null) : this()
        {
            Graphic = graphic ?? Image.CreateRectangle(w, h);
            Graphic.CenterOrigin();
            X = x;
            Y = y;
            _stateColors = colors;
        }

        public override void Start()
        {
            ClickHandler.StateColors = _stateColors ?? new StateColors(
                                           Color.FromArgb(255, 129, 0),
                                           Color.FromArgb(255, 206, 0),
                                           Color.FromArgb(182, 159, 0));

            AddComponent((Component) ClickHandler);
        }
        
    }
}