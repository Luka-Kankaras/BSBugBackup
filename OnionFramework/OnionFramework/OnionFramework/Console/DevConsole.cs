using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnionFramework.OnionFramework.Input;
using OnionFramework.OnionFramework.Utility;

namespace OnionFramework.OnionFramework.Console {
    public static class DevConsole {
        #region Fields

        private static StringBuilder command = new StringBuilder();
        private static List<DevConsoleCommand> commandList = new List<DevConsoleCommand>();
        private static List<string> commandHistory = new List<string>();
        private static bool enabled;

        private static DevConsoleConfig config;

        #region Properties

        public static StringBuilder Command {
            get => command;
            set => command = value;
        }

        public static List<DevConsoleCommand> CommandList {
            get => commandList;
            set => commandList = value;
        }

        public static List<string> CommandHistory {
            get => commandHistory;
            set => commandHistory = value;
        }

        public static bool Enabled {
            get => enabled;
            set => enabled = value;
        }

        public static DevConsoleConfig Config {
            get => config;
            set => config = value;
        }

        #endregion

        #endregion


        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            if (!enabled) return;

            // Draw the background
            Viewport viewPort = graphicsDevice.Viewport;
            float offset = 0.005f * viewPort.Width, // Offset from the edge of the window, both X and Y
                X = viewPort.X + offset,
                Y = viewPort.Y + offset,
                width = viewPort.Width * 0.39f,
                height = viewPort.Height - 2 * offset;

            UtilDraw.DrawQuadFill(spriteBatch, X, Y, width, height, 0.99f, config.BackgroundBoxColor);

            // Draw the input box
            width -= 2 * offset;
            height /= 20;
            offset *= 2;
            X = viewPort.X + offset;
            Y = viewPort.Y + viewPort.Height - offset - height;

            UtilDraw.DrawQuadFill(spriteBatch, X, Y, width, height, 0.991f, config.InputBoxColor);

            // Draw the command
            X += offset / 4;
            Y += offset / 2;

            UtilDraw.DrawString(spriteBatch, "" + config.CommandPrefix + ' ' + command,
                new Vector2(X, Y), 1, 0.992f, config.CommandColor, config.CommandFont);

            // Draw the command history

            Y -= offset / 2 + config.CommandFont.MeasureString("W").Y + 2 * config.VerticalTextPadding;
            foreach (string str in commandHistory) {
                Color drawColor = str[0] == config.NotifPrefix ? config.HistoryNotifColor : config.HistoryWarningColor;

                UtilDraw.DrawString(spriteBatch, str, new Vector2(X, Y), 1, 0.993f, drawColor, config.CommandFont);
                Y -= config.CommandFont.MeasureString(str).Y + config.VerticalTextPadding;
                if (Y < viewPort.Y + offset) break;
            }
        }

        public static void ShowConsole() {
            if (enabled) {
                enabled = false;
                TextInput.textInput -= HandleTextInput;
                TextInput.DisableTextInput();
            }
            else {
                if (config.Equals(default(DevConsoleConfig))) return;

                enabled = true;
                TextInput.EnableTextInput();
                TextInput.textInput += HandleTextInput;
            }
        }

        public static void InitializeByDefault(SpriteFont font) {
            Config = new DevConsoleConfig
            (
                Keys.OemTilde,
                font,
                new Color(Color.Black, 0.5f),
                new Color(Color.Black, 0.7f),
                Color.White,
                Color.Yellow,
                Color.OrangeRed,
                '>', '~', '!',
                2
            );
        }

        private static void AppendCommand() {
            foreach (DevConsoleCommand curr in commandList) {
                string[] wholeCommand = command.ToString().Split(" ");

                if (!curr.CommandName.Equals(wholeCommand[0])) continue;

                string[] args = new string[wholeCommand.Length - 1];

                for (int j = 1; j < wholeCommand.Length; j++)
                    args[j - 1] = wholeCommand[j];

                commandHistory.Insert(0, curr.Method.Invoke(args));
                command.Clear();
            }
        }

        private static void HandleTextInput(char character, Keys key) {
            if (key == Keys.Back) {
                if (KeyboardInput.KeyPressed(Keys.LeftControl) || KeyboardInput.KeyPressed(Keys.RightControl))
                    command.Clear();
                else if (command.Length > 0)
                    command.Remove(command.Length - 1, 1);

                return;
            }

            if (key == Keys.Enter) {
                AppendCommand();
                return;
            }

            if (key == Config.ActivationKey) return;

            command.Append(character);
        }
    }
}