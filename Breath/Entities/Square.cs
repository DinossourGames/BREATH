using System.Collections;
using System.Collections.Generic;
using Breath.Enums;
using DinoOtter;

namespace Breath.Entities
{
    public class Square : Entity
    {
        private Game _game;
        bool locker;
        private BoxCollider _collider;
        private Coroutine _coroutine;
        private IEnumerator _playable;

        public Square(float x, float y, int w, int h, System.Drawing.Color color,Game game,IEnumerator playable) : base(x, y)
        {
            _game = game;
            _playable = playable;
            _coroutine = _game.Coroutine;
            Graphic = Image.CreateRectangle(w, h, Color.FromDraw(color));
            Graphic.CenterOrigin();
            _collider = new BoxCollider(w, h);
            AddCollider(_collider);
            Collider.CenterOrigin();

        }

        public override void Update()
        {
            if (_collider.Overlap(X, Y, Tags.Player) && !locker)
            {
                locker = true;
                _coroutine.Start(_playable);
            }
        }

        public override void Render()
        {
            Collider.Render();
        }
    }
}