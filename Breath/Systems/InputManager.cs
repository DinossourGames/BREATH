using System;
using System.Linq;
using System.Threading;
using Breath.Enums;
using DinoOtter;
using DS4Windows;
using Color = System.Drawing.Color;
using Console = Colorful.Console;

namespace Breath.Systems
{
    public class InputManager
    {
        public event Action Next = delegate { };
        public event Action Previous = delegate { };
        public event Action Select = delegate { };

        public event Action Menu = delegate { };
        public event Action Pause = delegate { };

        public event Action<Vector2> Move = delegate { };
        public event Action MoveRelease = delegate { };
        public event Action<Vector2> Breath = delegate { };
        public event Action BreathRelease = delegate { };

        public event Action Jump = delegate { };
        public event Action JumpRelease = delegate { };
        public event Action Interact = delegate { };
        public event Action Attack = delegate { };
        public event Action ShootRelease = delegate { };

        public event Action Roll = delegate { };
        public event Action RollRelease = delegate { };


        private DS4Device device = null;

        private Game _game;
        private bool _isAlive;
        private Input _input;
        private string _lastCall;
        public bool CanReceiveInput { get; set; }

        public InputManager(Game game, Input input)
        {
            _game = game;
            _input = input;
            _game.OnUpdate += Update;
            FindController();
        }

        private void FindController()
        {
            new Thread(() =>
            {
                var canRun = true;
                _game.OnEnd += () => canRun = false;
                while (canRun)
                {
                    if (device == null)
                    {
                        DS4Devices.findControllers();
                        device = DS4Devices.getDS4Controllers().First();
                        device.Removal += (sender, args) =>
                        {
                            device = null;
                            DS4Devices.stopControllers();
                        };
                        device.Report += DeviceOnReport;
                        device.StartUpdate();
                        device.LightBarColor = new DS4Color(Color.DeepPink);
                        PrintInfo();
                    }

                    if (device != null && !_isAlive)
                        device = null;

                    Thread.Sleep(16);
                }
            }).Start();
        }


        private void DeviceOnReport(DS4Device sender, EventArgs args)
        {
            _isAlive = !sender.IsRemoved;
            var state = sender.getCurrentState();
            var previousState = sender.getPreviousState();


            var current = state.GetType().GetFields().Where(field => field.FieldType == typeof(bool))
                .ToDictionary(field => field.Name, field => (bool) field.GetValue(state));

            var previous = state.GetType().GetFields().Where(field => field.FieldType == typeof(bool))
                .ToDictionary(field => field.Name, field => (bool) field.GetValue(previousState));

            var activeCurrent = current.Where(val => val.Value).ToDictionary(i => i.Key, i => i.Value);
            var activePrevious = previous.Where(val => val.Value).ToDictionary(i => i.Key, i => i.Value);

            var inativeCurrent = current.Where(val => !val.Value).ToDictionary(i => i.Key, i => i.Value);
            var inactivePrevious = previous.Where(val => !val.Value).ToDictionary(i => i.Key, i => i.Value);

            var l3 = GetNormalized(state.LX, state.LY);
            var l3P = GetNormalized(previousState.LX, previousState.LY);

            if (l3 == l3P)
                MoveRelease?.Invoke();
            else
                Move?.Invoke(l3);


            if (state.L2Btn || state.R2Btn) Breath?.Invoke(new Vector2(state.L2, state.R2));
            else BreathRelease?.Invoke();

            //Move Up
            if (l3.Y > .5f)
                if (l3P.Y < .5 && l3P.Y > 0)
                    Next?.Invoke();
            //Move Down
            if (l3.Y < -.5)
                if (l3P.Y > -.5)
                    Previous?.Invoke();

            OnButtonPressedEvent(sender, state);

            foreach (var c in activeCurrent.Where(c => !activePrevious.ContainsKey(c.Key)))
                if (CanReceiveInput)
                {
                    OnOnButtonClickEvent(sender, state, c.Key);
                }

            foreach (var c in inativeCurrent.Where(c => activePrevious.ContainsKey(c.Key)))
                if (CanReceiveInput)
                {
                    OnReleaseClickEvent(sender, state, c.Key);
                }
        }


        private void OnReleaseClickEvent(DS4Device sender, DS4State state, string cKey)
        {
            var chave = Enum.TryParse(cKey, out DS4Button button);
            if (!chave) return;

            switch (button)
            {
                case DS4Button.Cross:
                    break;
                case DS4Button.Square:
                    break;
                case DS4Button.Circle:
                    break;
                case DS4Button.Triangle:
                    break;
                case DS4Button.Options:
                    break;
                case DS4Button.Share:
                    break;
                case DS4Button.L1:
                    break;
                case DS4Button.L3:
                    break;
                case DS4Button.R1:
                    break;
                case DS4Button.R3:
                    break;
                case DS4Button.DpadLeft:
                    break;
                case DS4Button.DpadUp:
                    break;
                case DS4Button.DpadRight:
                    break;
                case DS4Button.DpadDown:
                    break;
            }
            
        }

        private void OnButtonPressedEvent(DS4Device sender, DS4State state)
        {
            if (state.Cross)
                Jump?.Invoke();
            
            if (!state.Cross)
                JumpRelease?.Invoke();
        }


        private void OnOnButtonClickEvent(DS4Device sender, DS4State state, string cKey)
        {
            var chave = Enum.TryParse(cKey, out DS4Button button);
            if (!chave) return;
            switch (button)
            {
                case DS4Button.Cross:
                    CallAction(Select, "Select");
                    break;
                case DS4Button.Square:
                    Attack?.Invoke();
                    break;
                case DS4Button.Circle:
                    break;
                case DS4Button.Triangle:
                    break;
                case DS4Button.Options:
                    CallAction(Menu, "Menu");
                    break;
                case DS4Button.Share:
                    break;
                case DS4Button.L1:
                    break;
                case DS4Button.L3:
                    break;
                case DS4Button.R1:
                    Roll?.Invoke();
                    break;
                case DS4Button.R3:
                    break;
                case DS4Button.DpadLeft:
                    break;
                case DS4Button.DpadUp:
                    Next?.Invoke();
                    break;
                case DS4Button.DpadRight:
                    break;
                case DS4Button.DpadDown:
                    Previous?.Invoke();
                    break;
            }
        }

        void Update()
        {
            if (_isAlive) return;

            var move = new Vector2(0, 0);
            if (_input.KeyPressed(Key.W))
                move.Y = 1;
            if (_input.KeyPressed(Key.S))
                move.Y = -1;
            if (_input.KeyPressed(Key.D))
                move.X = 1;
            if (_input.KeyPressed(Key.A))
                move.X = -1;
            Move?.Invoke(move);


            var breath = new Vector2(0, 0);
            if (_input.KeyPressed(Key.LShift))
                breath.X = 1;
            if (_input.KeyPressed(Key.RShift))
                breath.Y = 1;
            Breath?.Invoke(breath);

            if (_input.KeyPressed(Key.Space))
                Jump?.Invoke();
            if (_input.KeyDown(Key.J))
                Attack?.Invoke();
            if (_input.KeyPressed(Key.F))
                Interact?.Invoke();

            if (_input.KeyPressed(Key.Down))
                Previous?.Invoke();
            if (_input.KeyPressed(Key.Up))
                Next?.Invoke();

            if (_input.KeyPressed(Key.Escape))
                CallAction(Menu, "Menu");
            if (_input.KeyPressed(Key.P))
                CallAction(Pause, "Pause");
            if (_input.KeyPressed(Key.Return))
                CallAction(Select, "Select");
        }

        private void CallAction(Action action, string name)
        {
            if (_lastCall == name) return;
            action?.Invoke();
            _lastCall = name;
        }

        private void PrintInfo()
        {
            Console.WriteLine("DS4 CONTROLLER FOUND", Color.GreenYellow);
            Console.WriteLine($"Baterry Percentage -> {device.Battery}");
            Console.WriteLine($"Connection Type -> {device.ConnectionType}");
        }


        private static Vector2 GetNormalized(byte x, byte y, float deadZone = .3f)
        {
            var array = new Vector2(0f, 0f);
            if (x < 125f)
                array.X = (1f - x / 125f) * -1 < -deadZone ? (1f - x / 125f) * -1 : 0;

            if (x > 125f)
                array.X = (x - 125) / (255f - 125f) > deadZone ? (x - 125) / (255f - 125f) : 0;

            if (y < 125f)
                array.Y = 1f - y / 125f > deadZone ? 1f - y / 125f : 0;

            if (y > 125f)
                array.Y = (y - 125) / (255f - 125f) * -1f < -deadZone ? (y - 125) / (255f - 125f) * -1f : 0;

            return array;
        }


        public void Rumble(byte left, byte right)
        {
            device.setRumble(right, left);
        }

        public void StopRumble()
        {
            device.setRumble(0, 0);
        }

        public void SetLightbar(Color color)
        {
            device.LightBarColor = new DS4Color(color);
        }
    }
}