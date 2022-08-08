using Microsoft.Xna.Framework;

namespace OnionFramework.OnionFramework.Renderer.Camera {
    public class Camera2D {
        #region Fields

        private Vector2 position;
        private float rotation;
        private Matrix view;

        #region Properties

        public Vector2 Position => position;

        public float Rotation => rotation;

        public Matrix View => view;

        #endregion
        
        #endregion

        public Camera2D(Vector2 position) {
            this.position = position;
            rotation = 0;
            view = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        public void UpdateCamera(Vector2 position, float rotation) {
            Vector2 resolution = PixelPerfectRenderer.RealResolution;
            
            this.position = position;
            this.rotation = rotation;
            
            view = Matrix.CreateTranslation(new Vector3(-position - resolution / 2, 0)) *
                     Matrix.CreateRotationZ(rotation) *
                     Matrix.CreateTranslation(new Vector3(resolution / 2, 0));
        }
    }
}