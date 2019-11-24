using DinoOtter;

namespace Breath.DataStructs
{
    public struct StateColors
    {
        public Color DefaultColor { get; set; }
        public Color HoveredColor { get; set; }
        public Color ClickedColor { get; set; }
        public Color DesactivateColor { get; set; }

        public StateColors(Color defaultColor, Color hoveredColor, Color clickedColor, Color desactivateColor = null)
        {
            DefaultColor = defaultColor;
            HoveredColor = hoveredColor;
            ClickedColor = clickedColor;
            DesactivateColor = desactivateColor ?? Color.Gray;
        }

        public StateColors(System.Drawing.Color defaultColor, System.Drawing.Color hoveredColor,
            System.Drawing.Color clickedColor, System.Drawing.Color desactivateColor)
        {
            DefaultColor = Color.FromDraw(defaultColor);
            HoveredColor = Color.FromDraw(hoveredColor);
            ClickedColor = Color.FromDraw(clickedColor);
            DesactivateColor = Color.FromDraw(desactivateColor);
        }

        public StateColors(System.Drawing.Color defaultColor, System.Drawing.Color hoveredColor,
            System.Drawing.Color clickedColor)
        {
            DefaultColor = Color.FromDraw(defaultColor);
            HoveredColor = Color.FromDraw(hoveredColor);
            ClickedColor = Color.FromDraw(clickedColor);
            DesactivateColor = Color.Gray;
        }
    }
}