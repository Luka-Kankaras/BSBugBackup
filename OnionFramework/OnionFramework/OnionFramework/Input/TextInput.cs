using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OnionFramework.OnionFramework.Input {
    public static class TextInput {
        #region Fields

        public delegate void TextInputEventHandler(char character, Keys key);

        public static event TextInputEventHandler textInput;

        private static GameWindow gameWindow;

        #endregion

        public static void InitializeTextInput(GameWindow gameWindow) {
            TextInput.gameWindow = gameWindow;
        }

        public static void EnableTextInput() {
            gameWindow.TextInput += HandleTextInput;
        }

        public static void DisableTextInput() {
            gameWindow.TextInput -= HandleTextInput;
        }

        private static void HandleTextInput(Object obj, TextInputEventArgs eventArgs) {
            textInput?.Invoke(eventArgs.Character, eventArgs.Key);
        }
    }
}