using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Breath.Abstractions.Interfaces;
using Breath.Enums;
using Breath.Systems;
using Breath.Utils;
using DinoOtter;
using SFML.Graphics.Glsl;

namespace Breath.Entities
{
    public class Player : Entity, IGameplayBinds
    {
        public Spritemap<PlayerAnimation> _spritemap = new Spritemap<PlayerAnimation>(BasePath.Images("main.png"), 32, 32);


        private InputManager _manager;
        private Game _game;
        private Coroutine _coroutines;

        public bool _isFacingRight = true;
        public bool _isWalking = false;
        public bool _isBreathing = false;
        public bool _isRunning = false;
        public bool _isRolling = false;
        public bool _isJumping = false;

        private bool _isAttacking = false;
        public bool canMove = true;

        private bool canApply;
        //  private float gravity = 10f;


        public Player(InputManager manager, Game game, float x, float y, float scale = 10) : base(x, y)
        {
            _manager = manager;
            _game = game;
            _coroutines = game.Coroutine;
            Initialize(scale);
            Collider = new BoxCollider((int) (_spritemap.HalfWidth * scale), (int) (_spritemap.Height * scale) - 8,
                Tags.Player);
            Collider.CenterOrigin();
            Bind();
        }

        private void Initialize(float scale)
        {
            _spritemap.Add(PlayerAnimation.Breath, "1,2,3,4,5,5,4,3,2,1", 4);
            _spritemap.Add(PlayerAnimation.Idle, "7,8,9,10,11,12", 4);
            _spritemap.Add(PlayerAnimation.Walk, "14,15,16,17,18,19,20,21,22,23,24", 4);
            _spritemap.Add(PlayerAnimation.Roll, "26,27,28,29", 4).NoRepeat();
            _spritemap.Add(PlayerAnimation.Run, "32,33,34,34,33,32", 4);
            _spritemap.Add(PlayerAnimation.Jumping, "36,37,38", 4).NoRepeat();
            _spritemap.Add(PlayerAnimation.Falling, "38,37,36", 4);
            _spritemap.Add(PlayerAnimation.Attacking, "40,41,42,43", 4).NoRepeat();

            _spritemap.Play(PlayerAnimation.Idle);
            _spritemap.CenterOrigin();
            _spritemap.Scale = scale;
            AddGraphic(_spritemap);
        }

        private void Bind()
        {
            _manager.Move += Move;
            _manager.Breath += Breath;
            _manager.Jump += Jump;
            _manager.Interact += Interact;
            _manager.Roll += Roll;
            _manager.Attack += Shoot;
            _manager.JumpRelease += () => JumpPressed = false;
        }

        private void Roll()
        {
            if (!_isRolling)
            {
                _coroutines.Start(RollAnim());
            }
        }

        IEnumerator RollAnim()
        {
            _isRolling = true;
            while (_spritemap.CurrentFrameIndex != _spritemap.Anims[PlayerAnimation.Roll].FrameCount - 1)
            {
                X += 25 * (_isFacingRight ? 1 : -1);
                yield return _coroutines.WaitForFrames(1);
            }

            _isRolling = false;
        }

        public void Move(Vector2 input)
        {
            if (Math.Abs(input.X) > .3f)
            {
                if (_isAttacking || _isRolling || _isJumping)
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

                Flip(input);

                if (!_isRolling && canMove)
                    X += input.X;
            }
            else
            {
                _isWalking = false;
                _isRunning = false;
            }
        }

        private void Flip(Vector2 input)
        {
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
        }

        public float Speed { get; set; } = 30;

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
            JumpPressed = true;
//            if (!_isJumping)
//                _coroutines.Start(Jumpe());
        }

        IEnumerator Jumpe()
        {
            _isJumping = true;

            canApply = false;
            var max = Y - 320;
            while (Y > max)
            {
                Y -= MathHelper.Lerp(Y, JumpForce, 1f);
                yield return _coroutines.WaitForFrames(1);
            }

            _isJumping = false;
            canApply = true;
        }

        public void Interact()
        {
        }

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


        private bool IsGrounded { get; set; }
        private bool JumpPressed { get; set; }

        private float VelocityY { get; set; }
        public float JumpForce { get; set; } = 100f;


        public override void Render()
        {
            Collider.Render();
        }

        private bool locker;
        public override void UpdateFirst()
        {
            if (Collider.Overlap(X, Y +10f, Tags.Ground) && !locker)
                IsGrounded = true;
        }

        public override void Update()
        {
            Gravity();

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
            if (_isJumping)
                PlayAnim(PlayerAnimation.Jumping);

            if (!_isRolling && !_isRunning && !_isWalking && !_isBreathing && !_isAttacking && !_isJumping)
                PlayAnim(PlayerAnimation.Idle);
        }

        private void Gravity()
        {
            Y += VelocityY;

            if (IsGrounded)
                locker = true;
            
            if (JumpPressed && IsGrounded)
            {
                JumpPressed = false;
                _isJumping = true;
                PlayAnim(PlayerAnimation.Jumping);
                IsGrounded = false;
                Y -= 5;
                VelocityY = -20;
            }


            if (!IsGrounded)
            {
                locker = false;
                VelocityY += .65f * _game.DeltaTime;
            }
            else
            {
                VelocityY = 0f;
                _isJumping = false;
                locker = true;
            }
        }
    }
}