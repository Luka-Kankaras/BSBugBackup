using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnionFramework.OnionFramework.Console {
    public struct DevConsoleConfig {
        #region Configuration fields

        public Keys ActivationKey { get; }

        public SpriteFont CommandFont { get; }

        public Color BackgroundBoxColor { get; }
        public Color InputBoxColor { get; }
        public Color CommandColor { get; }
        public Color HistoryNotifColor { get; }
        public Color HistoryWarningColor { get; }

        public char CommandPrefix { get; }
        public char NotifPrefix { get; }
        public char WarningPrefix { get; }

        public int VerticalTextPadding { get; }

        #endregion

        public DevConsoleConfig(Keys activationKey, SpriteFont commandFont, Color backgroundBoxColor,
            Color inputBoxColor, Color commandColor, Color historyNotifColor, Color historyWarningColor,
            char commandPrefix, char notifPrefix, char warningPrefix, int verticalTextPadding) {
            ActivationKey = activationKey;
            CommandFont = commandFont;
            BackgroundBoxColor = backgroundBoxColor;
            InputBoxColor = inputBoxColor;
            CommandColor = commandColor;
            HistoryNotifColor = historyNotifColor;
            HistoryWarningColor = historyWarningColor;
            CommandPrefix = commandPrefix;
            NotifPrefix = notifPrefix;
            WarningPrefix = warningPrefix;
            VerticalTextPadding = verticalTextPadding;
        }
    }
}