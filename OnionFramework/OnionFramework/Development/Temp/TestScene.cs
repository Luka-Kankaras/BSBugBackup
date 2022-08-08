using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnionFramework.OnionFramework.Components.Particles;
using OnionFramework.OnionFramework.ECS;
using OnionFramework.OnionFramework.Input;
using OnionFramework.OnionFramework.Renderer;
using OnionFramework.OnionFramework.Renderer.Camera;

namespace OnionFramework.Development.Temp {
    public class TestScene : Scene {
        #region Fields

        private Camera2D camera;
        private ParticleGenerator particleGenerator;
        
        #endregion
        
        
        public TestScene() {
            camera = new Camera2D(new Vector2(0, 0));
            PixelPerfectRenderer.Camera = camera;
        }

        public override void LoadContent(ContentManager contentManager) {
            Texture2D tex = contentManager.Load<Texture2D>("Textures/test");
            particleGenerator = new ParticleGenerator(tex, Vector2.Zero, Vector2.One, new Vector2(4f), 
                Vector2.One, Vector2.One, Vector2.Zero, 0.2f);

            // particleGenerator.AutoUpdate = false;
            
            base.LoadContent(contentManager);
        }

        public override void Update(GameTime gameTime) {
            particleGenerator.Update(gameTime);
            // if(KeyboardInput.KeyClicked(Keys.Space))
                // particleGenerator.GenerateParticles(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw() {
            particleGenerator.Render();
            base.Draw();
        }
    }
}