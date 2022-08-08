namespace OnionFramework.OnionFramework.Console {
    public class DevConsoleCommand {
        #region Fields

        public delegate string CommandMethod(string[] args);
        private CommandMethod method;
        private string commandName;

        #region Properties

        public string CommandName => commandName;

        public CommandMethod Method => method;

        #endregion
        
        #endregion

        public DevConsoleCommand(string commandName, CommandMethod method) {
            this.commandName = commandName;
            this.method = method;
        }
    }
}