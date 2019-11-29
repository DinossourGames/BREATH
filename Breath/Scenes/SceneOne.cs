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
        private Cor _cor;

        public SceneOne(Game game, InputManager manager, Coroutine coroutine, SoundSystem soundSystem) : base(
            "SceneOne")
        {
            _client = new FirebaseClient("https://projetobobo-ff345.firebaseio.com/");

            GetAllColors();
            //soundSystem.Play("main");
            _game = game;
            _manager = manager;
            _coroutine = coroutine;
            _soundSystem = soundSystem;
            _player = new Player(manager, game, game.HalfWidth, game.HalfHeight, 8);

            _text = new RichText("{shake:4}Seja Bem Vindo Ao CORES", "Fonts/FONTE.TTF", 64);
            _score = new RichText("Score: 0", "Fonts/BACKTO1982.TTF", 64);
           
            CreateSquares();


            AddGraphic(_text);
            AddGraphic(_score);
            Add(_player);
            coroutine.Start(MainRoutine());
        }

        private async void GetAllColors()
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
            yield return _coroutine.WaitForSeconds(5);

            while (true)
            {
                Reset();
                _text.String = "{waveAmpY:5}Escolhendo uma cor..";
                yield return _coroutine.WaitForSeconds(1);

                PickRandomColor();

                yield return _coroutine.WaitForSeconds(1);

                UpdateSquares();


                yield return _coroutine.WaitForSeconds(7);

                _manager.Rumble(60, 60);
                yield return _coroutine.WaitForSeconds(1);
                _manager.Rumble(0, 0);
                yield return _coroutine.WaitForSeconds(.8f);
                _manager.Rumble(200, 200);
                yield return _coroutine.WaitForSeconds(.8f);
                _manager.Rumble(0, 0);
                yield return _coroutine.WaitForSeconds(1f);
                _manager.Rumble(255, 255);
                yield return _coroutine.WaitForSeconds(.5f);
                _manager.Rumble(0, 0);




                foreach (var square in _squares)
                {
                    if (square.IsPressed && square.ColorReference == _corAtual)
                        score++;
                    if (square.IsPressed && square.ColorReference != _corAtual)
                        yield return SaveAndReset();

                    _score.String = $"SCORE: {score}";
                }

                _manager.Rumble(0, 0);
            }
        }

        private IEnumerator SaveAndReset()
        {
            _manager.Rumble(0, 0);

            _text.String = "VOCE PERDEU!";
            _text.Color = Color.Red;
            yield return _coroutine.WaitForSeconds(3);
            _text.String = _cor.Name;
            _text.Color = Color.FromBytes((byte) _cor.R, (byte) _cor.G, (byte) _cor.B);
            yield return _coroutine.WaitForSeconds(3);


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
                {
                    var cor = _colors.RandomElement();
                    var core = Color.FromBytes((byte) cor.R, (byte) cor.G, (byte) cor.B);
                    while (core == _corAtual || _squares.Any( i => i.ColorReference == core))
                    {
                        cor = _colors.RandomElement();
                        core = Color.FromBytes((byte) cor.R, (byte) cor.G, (byte) cor.B);
                    }

                    _squares[i].SetColor(core);
                }
            }
        }

        private void PickRandomColor()
        {
            _cor = _colors.RandomElement();
            _corAtual = Color.FromBytes((byte) _cor.R, (byte) _cor.G, (byte) _cor.B);

            _text.String = "{outline:3}{shakeX:1}" + _cor.Name;
            _text.Color = Color.White;
        }

        private void Reset()
        {
            _text.Color = Color.White;
            _squares.ForEach(i => i.SetColor(Color.FromDraw(System.Drawing.Color.White)));
            _manager.SetLightbar(System.Drawing.Color.White);
        }

        private void CreateSquares()
        {
            _squares = new List<Square>();

            for (int i = 0; i < 9; i++)
            {
                var x = 160 + 200 * i;

                var e = new Square(x, 800, 180, 180, System.Drawing.Color.White, _game, _manager);
                _squares.Add(e);
                Add(e);
            }
        }


        public override void Update()
        {
            _score.TextAlign = TextAlign.Center;
            _text.TextAlign = TextAlign.Center;
            _text.SetPosition(_game.HalfWidth, 300);
            _score.SetPosition(_game.HalfWidth, 200);
            _score.CenterOrigin();
            _text.CenterOrigin();
            _text.DefaultOutlineColor = Color.White;
            _text.DefaultOutlineThickness = 3;
            _text.Refresh();
            _score.Refresh();
        }


        public override void Render()
        {
        }
    }
}