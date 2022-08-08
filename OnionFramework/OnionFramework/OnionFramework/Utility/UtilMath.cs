using System;
using Microsoft.Xna.Framework;

namespace OnionFramework.OnionFramework.Utility {
    public static class UtilMath {
        public static float Angle(Vector2 w, Vector2 v) {
            return (float) Math.Atan((w.Y * v.X - w.X * v.Y) / (w.X * v.X + w.Y * v.Y));
        }
    }
}