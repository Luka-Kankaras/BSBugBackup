using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.OnionFramework.Renderer;

namespace OnionFramework.OnionFramework.Components.Particles {
    public class ParticleGenerator {
        #region Fields

        private delegate void UpdateParticle(Particle particle);
        
        private Vector2 position, direction, lifetimeRange, speedRange, scaleRange, rotationRange;
        private float particlesPerSecond;
        private double lastGenInMs;
        private bool active, autoUpdate, randomSway;
        private List<Particle> particles;
        private Texture2D texture;
        private Effect shader;
        private UpdateParticle updateParticle;
        private Random random;

        #region Properties

        public Vector2 Position {
            get => position;
            set => position = value;
        }

        public Vector2 Direction {
            get => direction;
            set => direction = value;
        }

        public Vector2 LifetimeRange {
            get => lifetimeRange;
            set => lifetimeRange = value;
        }

        public Vector2 SpeedRange {
            get => speedRange;
            set => speedRange = value;
        }

        public Vector2 ScaleRange {
            get => scaleRange;
            set => scaleRange = value;
        }

        public Vector2 RotationRange {
            get => rotationRange;
            set => rotationRange = value;
        }

        public float ParticlesPerSecond {
            get => particlesPerSecond;
            set => particlesPerSecond = value;
        }

        public bool Active {
            get => active;
            set => active = value;
        }

        public bool AutoUpdate {
            get => autoUpdate;
            set => autoUpdate = value;
        }

        public Texture2D Texture {
            get => texture;
            set => texture = value;
        }

        public Effect Shader {
            get => shader;
            set => shader = value;
        }

        #endregion
        
        #endregion


        public ParticleGenerator(Texture2D texture, Vector2 position, Vector2 direction, Vector2 lifetimeRange, Vector2 speedRange, Vector2 scaleRange, Vector2 rotationRange, float particlesPerSecond) {
            this.position = position;
            this.direction = direction;
            this.direction.Normalize();
            this.lifetimeRange = lifetimeRange;
            this.speedRange = speedRange;
            this.scaleRange = scaleRange;
            this.rotationRange = rotationRange;
            this.particlesPerSecond = particlesPerSecond;
            this.texture = texture;
            
            active = true;
            autoUpdate = true;
            randomSway = false;
            
            particles = new List<Particle>();
            shader = null;
            random = new Random();
        }

        public void Update(GameTime gameTime) {
            KillDeadParticles(gameTime);

            UpdateParticles();
            
            if (!autoUpdate) return;
            GenerateParticles(gameTime);
        } 
        
        public void Render() {
            PixelPerfectRenderer.Effect = shader;
            PixelPerfectRenderer.Start();
            foreach (Particle particle in particles)
                particle.Draw(texture);
            PixelPerfectRenderer.End();
        }

        private void KillDeadParticles(GameTime gameTime) {
            List<Particle> alivePtls = new List<Particle>();

            foreach (Particle particle in particles)
                if(particle.Alive(gameTime)) alivePtls.Add(particle);

            particles = alivePtls;
        }

        private void UpdateParticles() {
            foreach (Particle particle in particles) {
                particle.Position += particle.Speed * particle.Direction;
            }
        }

        public void GenerateParticles(GameTime gameTime) {
            double currTime = gameTime.TotalGameTime.TotalMilliseconds;
            
            if (autoUpdate && currTime - lastGenInMs < 1 / particlesPerSecond * 1000f) return;

            particles.Add(new Particle(position, direction, Vector2.One, 0, 1.0f, lifetimeRange.X, gameTime));
            lastGenInMs = currTime;
        }
    }
}