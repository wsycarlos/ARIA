using System;
using UnityEngine;

namespace vietlabs
{
    public static class vlbMath {
        public static Vector3 FixNaN(this Vector3 v) {
            v.x = Single.IsNaN(v.x) ? 0 : v.x;
            v.y = Single.IsNaN(v.y) ? 0 : v.y;
            v.z = Single.IsNaN(v.z) ? 0 : v.z;
            return v;
        }

        internal static float LerpTo(this float from, float to, float minDelta = 0.5f, float frac = 0.1f)
        {
            var d = to - from;
            if (d >= -minDelta && d <= minDelta) return to;
            return Mathf.Lerp(from, to, frac);
        }
    }
}