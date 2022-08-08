using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OnionFramework.OnionFramework.Input {
    public static class MouseInput {
        public enum MouseKeys {
            LeftMouseButton = 0,
            RightMouseButton = 1,
            XButton1 = 2,
            XButton2 = 3,
            MiddleMouseButton = 4
        }

        #region Fields

        private static KeyInfo[] mouseKeys;
        private static MouseState mouseState;
        private const int clickTimeRange = 9;

        #endregion


        public static void InitializeMouseInput() {
            mouseKeys = new[] {
                new KeyInfo(0, 0, "LMB", false, false, true),
                new KeyInfo(1, 0, "RMB", false, false, true),
                new KeyInfo(2, 0, "XButton-1", false, false, true),
                new KeyInfo(3, 0, "XButton-2", false, false, true),
                new KeyInfo(4, 0, "MMB", false, false, true)
            };
        }

        public static void UpdateMouseInput() {
            mouseState = Mouse.GetState();
            ButtonState[] buttonStates = {
                mouseState.LeftButton,
                mouseState.RightButton,
                mouseState.XButton1,
                mouseState.XButton2,
                mouseState.MiddleButton
            };

            for (int i = 0; i < buttonStates.Length; i++) {
                KeyInfo currKey = mouseKeys[i];
                if (currKey.Clicked)
                    currKey.Clicked = false;

                if (buttonStates[i] == ButtonState.Pressed) {
                    currKey.Released = false;
                    currKey.Pressed = true;
                    currKey.PressTime++;
                }
                else {
                    currKey.Released = true;

                    if (currKey.PressTime < clickTimeRange && currKey.Pressed)
                        currKey.Clicked = true;

                    currKey.PressTime = 0;
                    currKey.Pressed = false;
                }

                mouseKeys[i] = currKey;
            }
        }

        public static bool KeyPressed(MouseKeys key) {
            return mouseKeys[(int) key].Pressed;
        }

        public static bool KeyClicked(MouseKeys key) {
            return mouseKeys[(int) key].Clicked;
        }

        public static bool KeyReleased(MouseKeys key) {
            return mouseKeys[(int) key].Released;
        }

        public static Vector2 GetCursorPosition() {
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }
}