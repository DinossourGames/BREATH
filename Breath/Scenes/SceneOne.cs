using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Breath.Abstractions.Classes;
using Breath.Abstractions.Interfaces;
using Breath.Components;
using Breath.Entities;
using Breath.Enums;
using Breath.Models;
using Breath.Systems;
using Breath.Utils;
using DinoOtter;
using Firebase.Database;
using Firebase.Database.Streaming;
using SFML.Graphics;
using SFML.System;
using Color = DinoOtter.Color;
using Image = DinoOtter.Image;

namespace Breath.Scenes
{
    public class SceneOne : DinoScene
    {
        private Game _game;
        private InputManager _manager;
        private Player _player;
        private SoundSystem _soundSystem;
        private Coroutine _coroutine;
        private List<Square> _squares;
        private Color _corAtual;
        private RichText _text;
        private RichText _score;
        private int score = 0;
        private FirebaseClient _client;
        private List<Cor> _colors;

        public SceneOne(Game game, InputManager manager, Coroutine coroutine, SoundSystem soundSystem) : base(
            "SceneOne")
        {
            

            _client = new FirebaseClient("https://projetobobo-ff345.firebaseio.com/");
            
            GetAllColors();
            
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            _soundSystem = soundSystem;
            _player = new Player(manager, game, game.HalfWidth, game.HalfHeight, 8);

            _text = new RichText("Seja Bem Vindo Ao jogo", 64);
            _score = new RichText("0", 64);
            _text.TextAlign = TextAlign.Center;
            _text.SetPosition(600, 300);

            CreateSquares();


            AddGraphic(_text);
            AddGraphic(_score);
            Add(_player);
            coroutine.Start(MainRoutine());
        }

        private async void  GetAllColors()
        {
            var result = await _client.Child("Colors").OnceAsync<Cor>();
            
            _colors = new List<Cor>();
            
            foreach (var o in result)
            {
                _colors.Add(o.Object);
            }
        }
        
        IEnumerator MainRoutine()
        {
            yield return _coroutine.WaitForSeconds(10);

            while (true)
            {
                Reset();
                _text.String = "{waveAmpY:5}Escolhendo uma cor..";
                yield return _coroutine.WaitForSeconds(1);

                PickRandomColor();

                yield return _coroutine.WaitForSeconds(1);

                UpdateSquares();


                yield return _coroutine.WaitForSeconds(7);

                foreach (var square in _squares)
                {
                    if(square.IsPressed && square.ColorReference == _corAtual)
                        score++;
                    if (square.IsPressed && square.ColorReference != _corAtual)
                       SaveAndReset();
                    
                    _score.String = score.ToString();
                }

                yield return _coroutine.WaitForSeconds(5);

               
            }
        }

        private  void SaveAndReset()
        {
            //Add firebase Logic Here
             _client.Child("Highscore").PutAsync(score.ToString());
            _coroutine.StopAll();
            SceneManager.LoadScene("SceneOne");
        }

        private void UpdateSquares()
        {
            var r = new Random();
            var number = r.Next(0, 8);
            for (int i = 0; i < _squares.Count; i++)
            {
                if (i == number)
                    _squares[i].SetColor(_corAtual);
                else
                    _squares[i].SetColor(Color.Random);
            }
        }

        private void PickRandomColor()
        {
            var cor = _colors.RandomElement();
           _corAtual = Color.FromBytes((byte)cor.R,(byte)cor.G,(byte)cor.B); 

            _text.String = cor.Name;
            _text.Color = Color.Random;
        }

        private void Reset()
        {
            _squares.ForEach(i => i.SetColor(Color.FromDraw(System.Drawing.Color.White)));
            _manager.SetLightbar(System.Drawing.Color.White);
        }

        private void CreateSquares()
        {
            _squares = new List<Square>();

            for (int i = 0; i < 9; i++)
            {
                var x = 180 + 200 * i;

                var e = new Square(x, 800, 180, 180, System.Drawing.Color.White, _game, _manager);
                _squares.Add(e);
                Add(e);
            }
        }


        public override void Update()
        {
        }


        public override void Render()
        {
        }
    }
}