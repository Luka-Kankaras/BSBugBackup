using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.GCC.Collision;
using OnionFramework.OnionFramework.Components.Collision.Enums;
using OnionFramework.OnionFramework.ECS;
using OnionFramework.OnionFramework.Utility;

namespace OnionFramework.OnionFramework.Components.Collision {
    public class CircleCollider : Collider {
        public float Radius { get; set; }


        #region Constructors

        public CircleCollider(Entity parentEntity, Vector2 position, float radius, CollisionLayer layer) : base(
            parentEntity, position, layer) {
            type = ColliderType.CIRCLE_COLLIDER;
            this.Radius = radius;
        }

        public CircleCollider(Entity parentEntity, Vector2 position, float radius, CollisionLayer layer, Color color) :
            base(parentEntity, position, layer, color) {
            type = ColliderType.CIRCLE_COLLIDER;
            this.Radius = radius;
        }

        public CircleCollider(Entity parentEntity, Vector2 position, CollisionLayer layer) : base(parentEntity,
            position, layer) {
            type = ColliderType.CIRCLE_COLLIDER;
            Radius = 0;
        }

        public CircleCollider(Entity parentEntity, Vector2 position, CollisionLayer layer, Color color) : base(
            parentEntity, position, layer, color) {
            type = ColliderType.CIRCLE_COLLIDER;
            Radius = 0;
        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch) {
            UtilDraw.DrawCircle(spriteBatch, position, Radius, color);
        }
    }
}