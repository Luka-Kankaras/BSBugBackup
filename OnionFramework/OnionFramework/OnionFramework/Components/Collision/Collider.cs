using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.GCC.Collision.Static_classes;
using OnionFramework.OnionFramework.Components.Collision.Enums;
using OnionFramework.OnionFramework.Components.Collision.Structs;
using OnionFramework.OnionFramework.ECS;

namespace OnionFramework.OnionFramework.Components.Collision {
    public abstract class Collider : Component {
        #region Fields

        public delegate void CollisionEventHandler(CollisionInfo collisionInfo);

        public event CollisionEventHandler onCollision;

        protected Vector2 position;
        protected CollisionLayer layer;
        protected List<CollisionInfo> collisionInfoList;

        protected bool enabled;
        protected Color color;
        protected ColliderType type;

        #region Properties

        public Vector2 Position {
            get => position;
            set {
                position = value;
                CollisionManager.UpdateCollider(this);
            }
        }

        public CollisionLayer Layer {
            get => layer;
            set => layer = value;
        }

        public List<CollisionInfo> CollisionInfoList {
            get => collisionInfoList;
            set => collisionInfoList = value;
        }

        public bool Enabled {
            get => enabled;
            set => enabled = value;
        }

        public Color Color {
            get => color;
            set => color = value;
        }

        public ColliderType Type {
            get => type;
            set => type = value;
        }

        #endregion

        #endregion


        #region Constructors

        protected Collider(Entity parent, Vector2 position, CollisionLayer layer) : base(parent) {
            this.position = position;
            this.layer = layer;
            color = Color.Red;
            collisionInfoList = new List<CollisionInfo>();
            CollisionManager.AddCollider(this);
        }
        
        protected Collider(Entity parent, Vector2 position, CollisionLayer layer, Color color) : base(parent) {
            this.position = position;
            this.layer = layer;
            this.color = color;
            collisionInfoList = new List<CollisionInfo>();
            CollisionManager.AddCollider(this);
        }

        #endregion

        public void AddCollisionInfo(CollisionInfo collisionInfo) {
            collisionInfoList.Add(collisionInfo);
            OnCollision(collisionInfo);
        }

        public void UpdateCollisionInfo(CollisionInfo collisionInfo) {
            for (int i = 0; i < collisionInfoList.Count; i++) {
                if (collisionInfoList[i].Other.Equals(collisionInfo.Other)) {
                    collisionInfoList[i] = collisionInfo;
                    break;
                }
            }

            OnCollision(collisionInfo);
        }

        public void RemoveCollisionInfo(Collider other) {
            int i;
            for (i = 0; i < collisionInfoList.Count; i++)
                if (collisionInfoList[i].Other.Equals(other))
                    break;

            collisionInfoList.RemoveAt(i);
            OnCollision(new CollisionInfo(other, new List<Vector2>(), CollisionType.COLLISION_EXIT));
        }

        public bool ContainsCollisionInfo(Collider other) {
            for (int i = 0; i < collisionInfoList.Count; i++)
                if (collisionInfoList[i].Other.Equals(other))
                    return true;

            return false;
        }

        public abstract void Draw(SpriteBatch spriteBatch); 
        
        private void OnCollision(CollisionInfo collisionInfo) {
            onCollision?.Invoke(collisionInfo);
        }
        
        
    }
}