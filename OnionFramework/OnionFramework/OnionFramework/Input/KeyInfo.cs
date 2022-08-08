using System;
using Microsoft.Xna.Framework.Input;

namespace OnionFramework.OnionFramework.Input {
    public struct KeyInfo {
        #region Fields

        private int id, pressTime;
        private string name;
        private bool clicked, pressed, released;

        #region Properties

        public int Id {
            get => id;
            set => id = value;
        }

        public string Name {
            get => name;
            set => name = value;
        }

        public int PressTime {
            get => pressTime;
            set => pressTime = value;
        }

        public bool Clicked {
            get => clicked;
            set => clicked = value;
        }

        public bool Pressed {
            get => pressed;
            set => pressed = value;
        }

        public bool Released {
            get => released;
            set => released = value;
        }

        #endregion

        #endregion


        public KeyInfo(Keys key) {
            id = (int) key;
            name = Enum.GetName(typeof(Keys), key);
            clicked = false;
            pressed = false;
            released = true;
            pressTime = 0;
        }

        public KeyInfo(int id, int pressTime, string name, bool clicked, bool pressed, bool released) {
            this.id = id;
            this.pressTime = pressTime;
            this.name = name;
            this.clicked = clicked;
            this.pressed = pressed;
            this.released = released;
        }
    }
}