using Microsoft.Xna.Framework;

namespace OnionFramework.OnionFramework.ECS {
    public struct Transform {
        #region Fields

        public Vector3 position, rotation, scale;
        public Matrix matrix;

        #region Properties

        public Vector3 Position {
            get => position;
            set {
                position = value;
                UpdateTransform();
            }
        }

        public Vector3 Rotation {
            get => rotation;
            set {
                rotation = value; 
                UpdateTransform();
            }
        }

        public Vector3 Scale {
            get => scale;
            set {
                scale = value; 
                UpdateTransform();
            }
        }

        public Matrix Matrix => matrix;

        #endregion
        
        #endregion

        
        public Transform(Vector3 position, Vector3 rotation, Vector3 scale) : this() {
            UpdateTransform(position, rotation, scale);
        }

        
        public void UpdateTransform(Vector3 position, Vector3 rotation, Vector3 scale) {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            
            matrix = Matrix.CreateTranslation(position) *
                     Matrix.CreateRotationX(rotation.X) *
                     Matrix.CreateRotationY(rotation.Y) *
                     Matrix.CreateRotationZ(rotation.Z) *
                     Matrix.CreateScale(scale);
        }

        private void UpdateTransform() {
            matrix = Matrix.CreateTranslation(position) *
                     Matrix.CreateRotationX(rotation.X) *
                     Matrix.CreateRotationY(rotation.Y) *
                     Matrix.CreateRotationZ(rotation.Z) *
                     Matrix.CreateScale(scale);
        }
    }
}