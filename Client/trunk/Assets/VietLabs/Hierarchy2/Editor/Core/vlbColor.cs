using System.Globalization;
using UnityEngine;

namespace vietlabs
{
    public static class vlbColor {

        public static string ColorToHex(this Color color) {
            var c = (Color32)color;
            return c.r.ToString("X2") + c.g.ToString("X2") + c.b.ToString("X2");
        }
        public static Color HexToColor(this string hex) {
            var r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        public static Color ToColor(this int colorValue) {
            var a = (colorValue >> 24);
            var r = (colorValue >> 16) & 255;
            var g = (colorValue >> 8) & 255;
            var b = colorValue & 255;
            return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
        }
        public static int ToInt(this Color c) {
            Color32 c32 = c;
            return (c32.a << 24) | (c32.r << 16) | (c32.g << 8) | c32.b;
        }

        public static Color Alpha(this Color c, float alpha) {
            c.a = alpha;
            return c;
        }
        public static Color Adjust(this Color c, float pctAmount) {
            c.r *= 1 - pctAmount;
            c.g *= 1 - pctAmount;
            c.b *= 1 - pctAmount;
            return c;
        }
    }
}

