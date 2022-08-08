using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.OnionFramework.Renderer.Camera;

namespace OnionFramework.OnionFramework.Renderer {
    public static class PixelPerfectRenderer {
        #region Feild

        private static Vector2 nativeResolution, realResolution, pixelScale;

        private static SpriteBatch spriteBatch;
        private static SpriteSortMode spriteSortMode;
        private static BlendState blendState;
        private static SamplerState samplerState;
        private static DepthStencilState depthStencilState;
        private static RasterizerState rasterizerState;
        private static Effect effect;
        private static Camera2D camera;

        #region Properties

        public static SpriteBatch SpriteBatch {
            get => spriteBatch;
            set => spriteBatch = value;
        }

        public static SpriteSortMode SpriteSortMode {
            get => spriteSortMode;
            set => spriteSortMode = value;
        }

        public static BlendState BlendState {
            get => blendState;
            set => blendState = value;
        }

        public static SamplerState SamplerState {
            get => samplerState;
            set => samplerState = value;
        }

        public static DepthStencilState DepthStencilState {
            get => depthStencilState;
            set => depthStencilState = value;
        }

        public static RasterizerState RasterizerState {
            get => rasterizerState;
            set => rasterizerState = value;
        }

        public static Effect Effect {
            get => effect;
            set => effect = value;
        }

        public static Camera2D Camera {
            get => camera;
            set => camera = value;
        }

        public static Vector2 NativeResolution => nativeResolution;

        public static Vector2 RealResolution => realResolution;

        public static Vector2 PixelScale => pixelScale;

        #endregion
        
        #endregion
        
        public static void AdjustResolution(Vector2 nativeResolution, Vector2 realResolution) {
            PixelPerfectRenderer.nativeResolution = nativeResolution;
            PixelPerfectRenderer.realResolution = realResolution;

            pixelScale = realResolution / nativeResolution;
        }

        public static void Initialize(SpriteBatch spriteBatch) {
            PixelPerfectRenderer.spriteBatch = spriteBatch;
            spriteSortMode = SpriteSortMode.Deferred;
            blendState = BlendState.AlphaBlend;
            samplerState = SamplerState.PointClamp;
            DepthStencilState = DepthStencilState.None;
            RasterizerState = RasterizerState.CullCounterClockwise;
            effect = null;
        }
        
        public static void Start() {
            Matrix view = camera?.View ?? Matrix.Identity;
            spriteBatch.Begin(spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, view);
        }

        public static void Draw(Texture2D texture, Vector2 position, Color color) {
            spriteBatch.Draw(texture, new Rectangle((int)(position.X * pixelScale.X), (int)(position.Y * pixelScale.Y),
                (int)(texture.Width * pixelScale.X), (int)(texture.Height * pixelScale.Y)), color);
        }

        public static void Draw(Texture2D texture, Rectangle rectangle, Rectangle? sourceRectangle, Color color,
            float rotation, Vector2 origin, SpriteEffects spriteEffects, float layerDepth) {

            spriteBatch.Draw(texture, new Rectangle((int) pixelScale.X * rectangle.X, (int) pixelScale.Y * rectangle.Y,
                    (int) pixelScale.X * rectangle.Width, (int) pixelScale.Y * rectangle.Height), sourceRectangle,
                color, rotation, origin, spriteEffects, layerDepth);    
        }
        
        public static void End() {
            spriteBatch.End();
        }
    }
}