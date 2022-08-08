using System;
using Microsoft.Xna.Framework.Input;

namespace OnionFramework.OnionFramework.Input {
    public static class KeyboardInput {
        #region Fields

        private static KeyInfo[] keyInfos;
        private static KeyboardState keyboardState;
        private const int maxTimePassedForClick = 9;

        #endregion


        public static void InitializeKeyboardInput() {
            keyInfos = new KeyInfo[Enum.GetNames(typeof(Keys)).Length];

            int index = 0;
            foreach (Keys key in Enum.GetValues(typeof(Keys))) {
                keyInfos[index] = new KeyInfo(key);
                index++;
            }
        }

        public static void UpdateKeyboardInput() {
            keyboardState = Keyboard.GetState();
            for (int i = 0; i < keyInfos.Length; i++) {
                KeyInfo currKey = keyInfos[i];

                if (currKey.Clicked)
                    currKey.Clicked = false;

                if (keyboardState.IsKeyDown((Keys) currKey.Id)) {
                    currKey.Released = false;
                    currKey.Pressed = true;
                    currKey.PressTime++;
                }
                else {
                    currKey.Released = true;

                    if (currKey.PressTime < maxTimePassedForClick && currKey.Pressed)
                        currKey.Clicked = true;

                    currKey.PressTime = 0;
                    currKey.Pressed = false;
                }

                keyInfos[i] = currKey;
            }
        }

        public static bool KeyPressed(Keys key) {
            foreach (KeyInfo curr in keyInfos)
                if (curr.Id == (int) key && curr.Pressed)
                    return true;

            return false;
        }

        public static bool KeyClicked(Keys key) {
            foreach (var curr in keyInfos)
                if (curr.Id == (int) key && curr.Clicked)
                    return true;

            return false;
        }

        public static bool KeyReleased(Keys key) {
            foreach (var curr in keyInfos)
                if (curr.Id == (int) key && curr.Released)
                    return true;

            return false;
        }
    }
}