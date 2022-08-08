using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OnionFramework.OnionFramework.Utility {
    public static class UtilDraw {
        #region Fields

        private static SpriteFont debugFont;
        private static GraphicsDevice graphicsDevice;
        private static Texture2D pixelTexture;
        private static BasicEffect basicEffect;

        #endregion


        public static void Initialize(GraphicsDevice graphicsDevice, ContentManager contentManager) {
            UtilDraw.graphicsDevice = graphicsDevice;
            debugFont = contentManager.Load<SpriteFont>("Fonts/DebugFont");
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] {Color.White});

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);
        }

        public static void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, float scale, float depth,
            Color color, SpriteFont spriteFont) {
            spriteBatch.DrawString(spriteFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None,
                depth);
        }

        public static void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, float scale, float depth,
            Color color) {
            spriteBatch.DrawString(debugFont, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        public static void DrawQuadFill(SpriteBatch spriteBatch, float X, float Y, float width, float height,
            float depth, Color color) {
            spriteBatch.Draw(pixelTexture, new Rectangle((int) X, (int) Y, (int) width, (int) height), null, color, 0,
                Vector2.Zero, SpriteEffects.None, depth);
        }

        public static void DrawQuad(SpriteBatch spriteBatch, float X, float Y, float width, float height, Color color,
            int thickness) {
            while (true) {
                spriteBatch.Draw(pixelTexture, new Rectangle((int) X, (int) Y, (int) width, 1), color);
                spriteBatch.Draw(pixelTexture, new Rectangle((int) (X + width), (int) Y, 1, (int) height), color);
                spriteBatch.Draw(pixelTexture, new Rectangle((int) X, (int) (Y + height), (int) width, 1), color);
                spriteBatch.Draw(pixelTexture, new Rectangle((int) X, (int) Y, 1, (int) height), color);

                if (thickness > 1) {
                    X += 1;
                    Y += 1;
                    width -= 2;
                    height -= 2;
                    thickness--;
                }
                else
                    return;
            }
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float radius, Color color,
            int thickness = 1) {
            while (true) {
                if (radius <= 0) return;

                Vector2 curr = new Vector2(center.X, center.Y - radius);
                do {
                    float changeX, changeY;

                    spriteBatch.Draw(pixelTexture, curr, color);

                    if (curr.Y <= center.Y)
                        changeX = 1;
                    else
                        changeX = -1;

                    if (curr.X >= center.X)
                        changeY = 1;
                    else
                        changeY = -1;

                    float minDiff = float.MaxValue;
                    Vector2 bestChange = new Vector2(0);

                    float currDiff = Math.Abs(Vector2.Distance(center, curr + new Vector2(changeX, 0)) - radius);
                    if (currDiff < minDiff) {
                        minDiff = currDiff;
                        bestChange = new Vector2(changeX, 0);
                    }

                    currDiff = Math.Abs(Vector2.Distance(center, curr + new Vector2(0, changeY)) - radius);
                    if (currDiff < minDiff) {
                        minDiff = currDiff;
                        bestChange = new Vector2(0, changeY);
                    }

                    currDiff = Math.Abs(Vector2.Distance(center, curr + new Vector2(changeX, changeY)) - radius);
                    if (currDiff < minDiff) bestChange = new Vector2(changeX, changeY);

                    curr += bestChange;
                } while (curr != new Vector2(center.X, center.Y - radius));

                if (thickness > 1) {
                    radius -= 1;
                    thickness -= 1;
                    continue;
                }

                break;
            }
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Color color) {
            float width = Vector2.Distance(p1, p2);
            float angle = (float) Math.Atan((double) (p2.Y - p1.Y) / (p2.X - p1.X));

            if (p2.X < p1.X) angle += MathHelper.Pi;

            spriteBatch.Draw(pixelTexture, new Rectangle((int) p1.X, (int) p1.Y, (int) width, 1),
                null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}