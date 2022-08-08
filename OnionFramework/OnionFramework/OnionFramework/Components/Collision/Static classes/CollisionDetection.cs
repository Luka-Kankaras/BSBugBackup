using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OnionFramework.GCC.Collision;
using OnionFramework.OnionFramework.Components.Collision.Enums;

namespace OnionFramework.OnionFramework.Components.Collision.Static_classes {
    public static class CollisionDetection {
        public static List<Vector2> DetectCollision(Collider collider1, Collider collider2) {
            ColliderType type1 = collider1.Type, type2 = collider2.Type;
            if (type1 == ColliderType.QUAD_COLLIDER && type2 == ColliderType.QUAD_COLLIDER)
                return CD_QuadXQuad((QuadCollider) collider1, (QuadCollider) collider2);
            if (type1 == ColliderType.CIRCLE_COLLIDER && type2 == ColliderType.CIRCLE_COLLIDER)
                return CD_CircleXCircle((CircleCollider) collider1, (CircleCollider) collider2);

            if (type1 == ColliderType.CIRCLE_COLLIDER)
                return CD_CircleXQuad((CircleCollider) collider1, (QuadCollider) collider2);
            return CD_CircleXQuad((CircleCollider) collider2, (QuadCollider) collider1);
        }

        #region Collision Detection

        private static List<Vector2> CD_QuadXQuad(QuadCollider collider1, QuadCollider collider2) {
            List<Vector2> crossingPoints = new List<Vector2>();

            if (collider1.Position.X > collider2.Position.X && collider1.Position.X + collider1.Dimensions.X <
                collider2.Position.X + collider2.Dimensions.X &&
                collider1.Position.Y > collider2.Position.Y && collider1.Position.Y + collider1.Dimensions.Y <
                collider2.Position.Y + collider2.Dimensions.Y ||
                collider2.Position.X > collider1.Position.X && collider2.Position.X + collider2.Dimensions.X <
                collider1.Position.X + collider1.Dimensions.X &&
                collider2.Position.Y > collider1.Position.Y && collider2.Position.Y + collider2.Dimensions.Y <
                collider1.Position.Y + collider1.Dimensions.Y) {
                crossingPoints.Add(new Vector2(float.NaN));
                return crossingPoints;
            }

            Vector2[] pts = {
                new Vector2(collider1.Position.X, collider2.Position.Y),
                new Vector2(collider1.Position.X, collider2.Position.Y + collider2.Dimensions.Y),
                new Vector2(collider1.Position.X + collider1.Dimensions.X, collider2.Position.Y),
                new Vector2(collider1.Position.X + collider1.Dimensions.X,
                    collider2.Position.Y + collider2.Dimensions.Y),
                new Vector2(collider2.Position.X, collider1.Position.Y),
                new Vector2(collider2.Position.X + collider2.Dimensions.X, collider1.Position.Y),
                new Vector2(collider2.Position.X, collider1.Position.Y + collider1.Dimensions.Y),
                new Vector2(collider2.Position.X + collider2.Dimensions.X,
                    collider1.Position.Y + collider1.Dimensions.Y),
            };

            for (int i = 0; i < 8; i++) {
                if (IsPointInQuadBorders(collider1.Position, collider1.Dimensions, pts[i]) &&
                    IsPointInQuadBorders(collider2.Position, collider2.Dimensions, pts[i]))
                    crossingPoints.Add(pts[i]);
            }

            return crossingPoints;
        }

        private static List<Vector2> CD_CircleXCircle(CircleCollider collider1, CircleCollider collider2) {
            List<Vector2> crossingPoints = new List<Vector2>();

            float p1 = collider1.Position.X,
                q1 = collider1.Position.Y,
                p2 = collider2.Position.X,
                q2 = collider2.Position.Y;


            float r1 = collider1.Radius, r2 = collider2.Radius;

            float C = p2 * p2 - p1 * p1 + q2 * q2 - q1 * q1 + r1 * r1 - r2 * r2,
                D = C / (2 * (q2 - q1)) - q1,
                E = (p1 - p2) / (q2 - q1);

            float a = E * E + 1, b = 2 * (E * D - p1), c = (p1 * p1 + D * D - r1 * r1);

            float x1 = (float) (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a),
                x2 = (float) (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

            if (x1 != x2) {
                crossingPoints.Add(new Vector2(x1, E * x1 + (D + q1)));
                crossingPoints.Add(new Vector2(x2, E * x2 + (D + q1)));
            }
            else
                crossingPoints.Add(new Vector2(x1, E * x1 + (D + q1)));

            return crossingPoints;
        }

        private static List<Vector2> CD_CircleXQuad(CircleCollider circleCollider, QuadCollider quadCollider) {
            List<Vector2> crossingPoints = new List<Vector2>();

            if (Vector2.Distance(circleCollider.Position, quadCollider.Position) < circleCollider.Radius &&
                Vector2.Distance(circleCollider.Position, quadCollider.Position + quadCollider.Dimensions) <
                circleCollider.Radius) {
                crossingPoints.Add(new Vector2(float.NaN));
                return crossingPoints;
            }

            for (float currX = quadCollider.Position.X;
                 currX <= quadCollider.Position.X + quadCollider.Dimensions.X;
                 currX += quadCollider.Dimensions.X) {
                float b = -2 * circleCollider.Position.Y,
                    c = (float) (Math.Pow(circleCollider.Position.Y, 2) + Math.Pow(currX, 2)
                                 - 2 * circleCollider.Position.X * currX + Math.Pow(circleCollider.Position.X, 2) -
                                 Math.Pow(circleCollider.Radius, 2));

                float discriminant = b * b - 4 * c;
                if (!(discriminant > 0)) continue;
                discriminant = (float) Math.Sqrt(discriminant);

                float currY = (-b + discriminant) / 2;
                if (currY >= quadCollider.Position.Y && currY <= quadCollider.Position.Y + quadCollider.Dimensions.Y)
                    crossingPoints.Add(new Vector2(currX, currY));

                currY = (-b - discriminant) / 2;
                if (currY >= quadCollider.Position.Y && currY <= quadCollider.Position.Y + quadCollider.Dimensions.Y)
                    crossingPoints.Add(new Vector2(currX, currY));
            }

            for (float currY = quadCollider.Position.Y;
                 currY <= quadCollider.Position.Y + quadCollider.Dimensions.Y;
                 currY += quadCollider.Dimensions.Y) {
                float b = -2 * circleCollider.Position.X,
                    c = (float) (Math.Pow(circleCollider.Position.X, 2) + Math.Pow(currY, 2)
                                 - 2 * circleCollider.Position.Y * currY + Math.Pow(circleCollider.Position.Y, 2) -
                                 Math.Pow(circleCollider.Radius, 2));

                float discriminant = b * b - 4 * c;
                if (!(discriminant > 0)) continue;
                discriminant = (float) Math.Sqrt(discriminant);

                float currX = (-b + discriminant) / 2;
                if (currX >= quadCollider.Position.X && currX <= quadCollider.Position.X + quadCollider.Dimensions.X)
                    crossingPoints.Add(new Vector2(currX, currY));

                currX = (-b - discriminant) / 2;
                if (currX >= quadCollider.Position.X && currX <= quadCollider.Position.X + quadCollider.Dimensions.X)
                    crossingPoints.Add(new Vector2(currX, currY));
            }

            if (crossingPoints.Count == 0 &&
                IsPointInQuad(circleCollider.Position, quadCollider.Position, quadCollider.Dimensions))
                crossingPoints.Add(new Vector2(float.NaN));

            return crossingPoints;
        }

        #endregion

        #region Helper methods

        private static bool IsPointInQuadBorders(Vector2 quadPosition, Vector2 quadDimensions, Vector2 pointPosition) {
            if (pointPosition.X >= quadPosition.X && pointPosition.X <= quadPosition.X + quadDimensions.X &&
                (pointPosition.Y == quadPosition.Y || pointPosition.Y == quadPosition.Y + quadDimensions.Y))
                return true;

            return pointPosition.Y >= quadPosition.Y && pointPosition.Y <= quadPosition.Y + quadDimensions.Y &&
                   (pointPosition.X == quadPosition.X || pointPosition.X == quadPosition.X + quadDimensions.X);
        }

        private static bool IsPointInQuad(Vector2 point, Vector2 quadPosition, Vector2 quadDimensions) {
            return point.X > quadPosition.X && point.X < quadPosition.X + quadDimensions.X &&
                   point.Y > quadPosition.Y && point.Y < quadPosition.Y + quadDimensions.Y;
        }

        #endregion
    }
}