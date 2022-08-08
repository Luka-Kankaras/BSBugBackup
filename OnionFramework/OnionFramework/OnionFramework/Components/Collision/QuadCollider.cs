using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.OnionFramework.Components.Collision.Enums;
using OnionFramework.OnionFramework.ECS;
using OnionFramework.OnionFramework.Utility;

namespace OnionFramework.OnionFramework.Components.Collision {
    public class QuadCollider : Collider {
        #region Fields

        private Vector2 dimensions;

        #region Properties

        public Vector2 Dimensions {
            get => dimensions;
            set => dimensions = value;
        }

        #endregion

        #endregion


        #region Constructors

        public QuadCollider(Entity parentEntity, Vector2 position, Vector2 dimensions, CollisionLayer layer) : base(
            parentEntity, position, layer) {
            type = ColliderType.QUAD_COLLIDER;
            this.dimensions = dimensions;
        }

        public QuadCollider(Entity parentEntity, Vector2 position, Vector2 dimensions, CollisionLayer layer,
            Color color) : base(parentEntity, position, layer, color) {
            type = ColliderType.QUAD_COLLIDER;
            this.dimensions = dimensions;
        }

        public QuadCollider(Entity parentEntity, Vector2 position, CollisionLayer layer, Color color) : base(
            parentEntity, position, layer, color) {
            type = ColliderType.QUAD_COLLIDER;
            dimensions = Vector2.Zero;
        }

        public QuadCollider(Entity parentEntity, Vector2 position, CollisionLayer layer) : base(parentEntity, position,
            layer) {
            type = ColliderType.QUAD_COLLIDER;
            dimensions = Vector2.Zero;
        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch) {
            UtilDraw.DrawQuad(spriteBatch, position.X - 1, position.Y - 1, dimensions.X + 1, dimensions.Y + 1, color,
                1);
        }
    }
}