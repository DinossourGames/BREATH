using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Breath.Abstractions.Interfaces;
using Breath.Systems;
using Breath.Utils;
using DinoOtter;
using SFML.Graphics.Glsl;

namespace Breath.Entities
{
    enum PlayerAnimation
    {
        Idle,
        Walk,
        Breath,
        Roll,
        Run,
        Jumping,
        Attacking,
        Falling
    }

    enum Tags
    {
        Player,
        Collectable
    }

    public class Player : Entity, IGameplayBinds
    {
        Spritemap<PlayerAnimation> _spritemap = new Spritemap<PlayerAnimation>(BasePath.Images("main.png"), 32, 32);
        BoxCollider _collider = new BoxCollider(32, 32, Tags.Player);

        private InputManager _manager;
        private Game _game;
        private Coroutine _coroutines;

        private bool _isFacingRight = true;
        private bool _isWalking = false;
        private bool _isBreathing = false;
        private bool _isRunning = false;
        private bool _isRolling = false;
        private bool _isJumping = false;
        private bool _isAttacking = false;

        public Player(InputManager manager, Game game, float x, float y, float scale = 10) : base(x, y)
        {
            _manager = manager;
            _game = game;
            _coroutines = game.Coroutine;
        

            _spritemap.Add(PlayerAnimation.Breath, "1,2,3,4,5,5,4,3,2,1", 4);
            _spritemap.Add(PlayerAnimation.Idle, "7,8,9,10,11,12", 4);
            _spritemap.Add(PlayerAnimation.Walk, "14,15,16,17,18,19,20,21,22,23,24", 4);
            _spritemap.Add(PlayerAnimation.Roll, "26,27,28,29", 4);
            _spritemap.Add(PlayerAnimation.Run, "32,33,34,34,33,32", 4);
            _spritemap.Add(PlayerAnimation.Jumping, "36,37,38", 4);
            _spritemap.Add(PlayerAnimation.Falling, "38,37,36", 4);
            _spritemap.Add(PlayerAnimation.Attacking, "40,41,42,43", 4).NoRepeat();

            _spritemap.Play(PlayerAnimation.Idle);
            _spritemap.CenterOrigin();
            _spritemap.Scale = scale;
            AddGraphic(_spritemap);
            AddCollider(_collider);
            _collider.CenterOrigin();
            Bind();
        }

      
        private void Bind()
        {
            _manager.Move += Move;
            _manager.Breath += Breath;
            _manager.Jump += Jump;
            _manager.Interact += Interact;
            _manager.Roll += Roll;
            _manager.Shoot += Shoot;
        }

        private void Roll()
        {
        }

        public void Move(Vector2 input)
        {
            if (Math.Abs(input.X) > .3f)
            {
                if (_isAttacking)
                {
                    _isRunning = false;
                    _isWalking = false;
                }
                else
                {
                    if (Math.Abs(input.X) > .8f)
                    {
                        _isRunning = true;
                        _isWalking = false;
                    }

                    if (Math.Abs(input.X) < .8f)
                    {
                        _isRunning = false;
                        _isWalking = true;
                    }
                }

                if (input.X > 0 && !_isFacingRight)
                {
                    _isFacingRight = !_isFacingRight;
                    _spritemap.FlippedX = false;
                }

                if (input.X < 0 && _isFacingRight)
                {
                    _isFacingRight = !_isFacingRight;
                    _spritemap.FlippedX = true;
                }

                X += input.X;
            }
            else
                _isWalking = false;
        }

        private void PlayAnim(PlayerAnimation anim)
        {
            if (_spritemap.CurrentAnim != anim)
                _spritemap.Play(anim);
        }

        public void Breath(Vector2 input)
        {
        }

        public void Jump()
        {
            _isJumping = true;
        }

        public void Interact()
        {
        }

        private int atackRange = 0;

        public void Shoot()
        {
            if (!_isAttacking)
                _coroutines.Start(Attack());
        }

        IEnumerator Attack()
        {
            _isAttacking = true;
            while (_spritemap.CurrentFrame < 42)
            {
                yield return _coroutines.WaitForFrames(1);
            }

            _isAttacking = false;
        }

        public override void Update()
        {

            if (_isBreathing)
                PlayAnim(PlayerAnimation.Breath);
            if (_isWalking)
                PlayAnim(PlayerAnimation.Walk);
            if (_isRolling)
                PlayAnim(PlayerAnimation.Roll);
            if (_isRunning)
                PlayAnim(PlayerAnimation.Run);
            if (_isAttacking)
                PlayAnim(PlayerAnimation.Attacking);

            if (!_isRolling && !_isRunning && !_isWalking && !_isBreathing && !_isAttacking)
                PlayAnim(PlayerAnimation.Idle);
        }
    }
}