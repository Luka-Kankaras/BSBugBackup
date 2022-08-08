using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnionFramework.OnionFramework.Components.Collision;
using OnionFramework.OnionFramework.Components.Collision.Enums;
using OnionFramework.OnionFramework.Components.Collision.Static_classes;
using OnionFramework.OnionFramework.Components.Collision.Structs;

namespace OnionFramework.GCC.Collision.Static_classes {
    public static class CollisionManager {

        #region Fields

        private static int[,] layerMatrix = {
            {0, 1, 0},
            {1, 0, 1},
            {0, 1, 0}
        };

        private static List<Collider> colliderList = new List<Collider>();

        #region Properties

        public static int[,] LayerMatrix {
            get => layerMatrix;
            set => layerMatrix = value;
        }

        public static List<Collider> ColliderList {
            get => colliderList;
            set => colliderList = value;
        }

        #endregion

        #endregion

        
        public static void AddCollider(Collider collider) {
            colliderList.Add(collider);
        }

        public static void UpdateAllColliders() {
            for (int i = 0; i < colliderList.Count; i++) {
                for (int j = i + 1; j < colliderList.Count; j++) {
                    Collider collider1 = colliderList[i], collider2 = colliderList[j];
                    if (layerMatrix[(int) collider1.Layer, (int) collider2.Layer] == 0) continue;

                    List<Vector2> crossingPoints = CollisionDetection.DetectCollision(collider1, collider2);
                    if (crossingPoints.Count > 0) {
                        if (collider1.ContainsCollisionInfo(colliderList[j])) {
                            // System.Console.WriteLine("Collision stay");
                            collider1.UpdateCollisionInfo(new CollisionInfo(collider2, crossingPoints,
                                CollisionType.COLLISION_STAY));
                            collider2.UpdateCollisionInfo(new CollisionInfo(collider1, crossingPoints,
                                CollisionType.COLLISION_STAY));
                        }
                        else {
                            // System.Console.WriteLine("Collision enter");
                            collider1.AddCollisionInfo(new CollisionInfo(collider2, crossingPoints,
                                CollisionType.COLLISION_ENTER));
                            collider2.AddCollisionInfo(new CollisionInfo(collider1, crossingPoints,
                                CollisionType.COLLISION_ENTER));
                        }
                    }
                    else if (colliderList[i].ContainsCollisionInfo(colliderList[j])) {
                        // System.Console.WriteLine("Collision exit");
                        collider1.RemoveCollisionInfo(collider2);
                        collider2.RemoveCollisionInfo(collider1);
                    }
                }
            }
        }

        public static void UpdateCollider(Collider collider) {
            for (int i = 0; i < colliderList.Count; i++) {
                Collider other = colliderList[i];
                if (other.Equals(collider) || layerMatrix[(int) collider.Layer, (int) other.Layer] == 0) continue;

                List<Vector2> crossingPoints = CollisionDetection.DetectCollision(collider, other);
                if (crossingPoints.Count > 0) {
                    if (collider.ContainsCollisionInfo(other)) {
                        // System.Console.WriteLine("Collision stay");
                        collider.UpdateCollisionInfo(new CollisionInfo(other, crossingPoints,
                            CollisionType.COLLISION_STAY));
                        other.UpdateCollisionInfo(new CollisionInfo(collider, crossingPoints,
                            CollisionType.COLLISION_STAY));
                    }
                    else {
                        // System.Console.WriteLine("Collision enter");
                        collider.AddCollisionInfo(new CollisionInfo(other, crossingPoints,
                            CollisionType.COLLISION_ENTER));
                        other.AddCollisionInfo(new CollisionInfo(collider, crossingPoints,
                            CollisionType.COLLISION_ENTER));
                    }
                }
                else if (collider.ContainsCollisionInfo(colliderList[i])) {
                    // System.Console.WriteLine("Collision exit");
                    collider.RemoveCollisionInfo(other);
                    other.RemoveCollisionInfo(collider);
                }
            }
        }

        public static void DrawColliders(SpriteBatch spriteBatch) {
            for (int i = 0; i < colliderList.Count; i++)
                colliderList[i].Draw(spriteBatch);
        }
    }
}