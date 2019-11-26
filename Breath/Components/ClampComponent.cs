using DinoOtter;

namespace Breath.Components
{
    public class ClampComponent : Component
    {
        private readonly Game _game;

        public ClampComponent(Game game) => _game = game;

        public override void UpdateFirst()
        {
            if (Entity.Graphic == null)
                return;

            Entity.X = MathHelper.Clamp(Entity.X, 0, _game.Width - Entity.Graphic.HalfWidth);
            Entity.Y = MathHelper.Clamp(Entity.Y, 0, _game.Height - Entity.Graphic.HalfHeight);
        }
    }
}