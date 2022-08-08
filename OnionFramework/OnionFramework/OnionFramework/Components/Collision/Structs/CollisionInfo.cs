using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OnionFramework.GCC.Collision;
using OnionFramework.OnionFramework.Components.Collision.Enums;

namespace OnionFramework.OnionFramework.Components.Collision.Structs {
    public struct CollisionInfo {
        #region Fields

        private Collider other;
        private List<Vector2> crossingPoints;
        private CollisionType collisionType;

        #region Properties

        public Collider Other => other;

        public List<Vector2> CrossingPoints => crossingPoints;

        public CollisionType CollisionType => collisionType;

        #endregion

        #endregion


        #region Constructors

        public CollisionInfo(Collider other, List<Vector2> crossingPoints, CollisionType collisionType) {
            this.other = other;
            this.crossingPoints = crossingPoints;
            this.collisionType = collisionType;
        }

        #endregion
    }
}