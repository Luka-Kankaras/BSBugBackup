using System;
using System.Collections.Generic;
using OnionFramework.Development.Temp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnionFramework.OnionFramework.Console;
using OnionFramework.OnionFramework.ECS;
using OnionFramework.OnionFramework.Input;
using OnionFramework.OnionFramework.Renderer;
using OnionFramework.OnionFramework.Utility;

namespace OnionFramework.Development.Main
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region DevConsole commands

        private string ConsoleFullScreen(string[] args)
        {
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();
            return DevConsole.Config.NotifPrefix + " Fullscreen set to " + graphics.IsFullScreen;
        }

        private string ConsoleResizeWindow(string[] args)
        {
            try
            {
                if(args[0] != "w")
                    graphics.PreferredBackBufferWidth = int.Parse(args[0]);
                if(args[1] != "h")
                    graphics.PreferredBackBufferHeight = int.Parse(args[1]);
                graphics.ApplyChanges();
                
                return DevConsole.Config.NotifPrefix + " Resized window to " + 
                       graphics.PreferredBackBufferWidth + "x" + graphics.PreferredBackBufferHeight;
            }
            catch (FormatException)
            {
                return DevConsole.Config.WarningPrefix + " Command requires 2 inputs of type int";
            }
            catch (IndexOutOfRangeException)
            {
                return DevConsole.Config.WarningPrefix + " Command requires 2 inputs";
            }
        }

        private string ConsoleClearHistory(string[] args)
        {
            DevConsole.CommandHistory = new List<string>();
            return "";
        }
        
        private void ConsoleSetCommands()
        {
            DevConsole.CommandList.Add(new DevConsoleCommand("w_fullscreen", ConsoleFullScreen));
            DevConsole.CommandList.Add(new DevConsoleCommand("w_resize", ConsoleResizeWindow));
            DevConsole.CommandList.Add(new DevConsoleCommand("clear", ConsoleClearHistory));
        }
        
        #endregion
        
        #region ILUD

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            PixelPerfectRenderer.AdjustResolution(new Vector2(320, 180), new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            
            IsMouseVisible = true;
            
            KeyboardInput.InitializeKeyboardInput();
            MouseInput.InitializeMouseInput();
            TextInput.InitializeTextInput(Window);
            
            UtilDraw.Initialize(GraphicsDevice, Content);
            
            DevConsole.InitializeByDefault(Content.Load<SpriteFont>("Fonts/DebugFont"));
            ConsoleSetCommands();

            Scene.ActiveScene = new TestScene();
            Scene.ActiveScene.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PixelPerfectRenderer.Initialize(spriteBatch);            
            Scene.ActiveScene.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardInput.UpdateKeyboardInput();
            MouseInput.UpdateMouseInput();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (KeyboardInput.KeyClicked(DevConsole.Config.ActivationKey))
                DevConsole.ShowConsole();
            
            Scene.ActiveScene.Update(gameTime);
            
            base.Update(gameTime);
        }
 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Scene.ActiveScene.Draw();
            base.Draw(gameTime);
        }

        #endregion
    }
}