using System;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.Entities;
using Breath.Enums;
using Breath.Systems;
using DinoOtter;
using Ninject;

namespace Breath.Components
{
    public class Ds4Movment : IDinoComponent, IGameplayBinds
    {
        public float Speed { get; set; } = 10;


        private Animator<PlayerAnimations> _animator;
        public DinoEntity Entity { get; set; }
        [Inject] public IJump JumpComponent { get; set; }

        private float _velocityX = 0;
        private Vector2 _breath;

        public Ds4Movment( InputManager manager, Animator<PlayerAnimations> animator)
        {
            
            _animator = animator;

            Bind(manager);
        }


        private void Bind(InputManager manager)
        {
            manager.Move += Move;
            manager.MoveRelease += () => _velocityX = 0;

            manager.Breath += Breath;
            manager.BreathRelease += () => _breath = Vector2.Zero;

            manager.Jump += Jump;
            manager.JumpRelease += () => { };

            manager.Interact += Interact;
            manager.Roll += Roll;
            manager.Attack += Attack;
        }


        public void Start()
        {
            JumpComponent.Start();
        }

        public void Update()
        {
            JumpComponent.Update();
            JumpComponent.Entity = Entity;
            if (Entity != null)
                Entity.X += _velocityX;
            
            
        }

        public void Draw()
        {
        }


        public void Move(Vector2 input)
        {
            _velocityX = input.X * Speed;
        }

        public void Breath(Vector2 input)
        {
            _animator.Play(PlayerAnimations.Breath);
            _breath = input;
        }

        public void Jump() => JumpComponent.Jump();

        public void Interact()
        {
        }

        public void Attack()
        {
        }

        public void Roll()
        {
        }
    }
}