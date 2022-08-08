using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.OnionFramework.Renderer;

namespace OnionFramework.OnionFramework.Components.Particles {
    public class Particle {
        #region Fields

        private Vector2 startPosition, position, direction, swayComponent, scale;
        private float rotation, speed, lifetime;
        private double birthInMs;
        
        #region Properties

        public Vector2 Position {
            get => position;
            set => position = value;
        }

        public Vector2 Scale {
            get => scale;
            set => scale = value;
        }

        public float Rotation {
            get => rotation;
            set => rotation = value;
        }

        public float Speed {
            get => speed;
            set => speed = value;
        }

        public Vector2 Direction => direction;

        #endregion
        
        #endregion

        public Particle(Vector2 position, Vector2 direction, Vector2 scale, float rotation, float speed, float lifetime, GameTime gameTime) {
            this.position = position;
            this.direction = direction;
            this.scale = scale;
            this.rotation = rotation;
            this.speed = speed;
            this.lifetime = lifetime;
            birthInMs = gameTime.TotalGameTime.TotalMilliseconds;

            startPosition = position;
            Vector3 right = Vector3.Cross(new Vector3(0, 0, 1), new Vector3(direction, 0));
            swayComponent = new Vector2(right.X, right.Y) * 0.0001f;
        }

        public void Draw(Texture2D texture) {
            PixelPerfectRenderer.Draw(texture, new Rectangle((int)position.X, (int)position.Y,
                    (int)scale.X * texture.Width, (int)scale.Y * texture.Height), null, Color.White,
                rotation, Vector2.Zero, SpriteEffects.None, 0);
        }

        public bool Alive(GameTime gameTime) {
            return (gameTime.TotalGameTime.TotalMilliseconds - birthInMs) / 1000f <= lifetime;
        }
    }
}