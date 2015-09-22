using UnityEngine;
namespace vietlabs
{
    public static class vlbRect {
        public static float dx(this Rect rect1, Rect rect2) { return rect2.x - rect1.x; }
        public static float dy(this Rect rect1, Rect rect2) { return rect2.y - rect1.y; }
        public static float dw(this Rect rect1, Rect rect2) { return rect2.width - rect1.width; }
        public static float dh(this Rect rect1, Rect rect2) { return rect2.height - rect1.height; }

        public static float dx_abs(this Rect rect1, Rect rect2) { return Mathf.Abs(rect2.x - rect1.x); }
        public static float dy_abs(this Rect rect1, Rect rect2) { return Mathf.Abs(rect2.y - rect1.y); }
        public static float dw_abs(this Rect rect1, Rect rect2) { return Mathf.Abs(rect2.width - rect1.width); }
        public static float dh_abs(this Rect rect1, Rect rect2) { return Mathf.Abs(rect2.height - rect1.height); }




        public static bool IsDifferent(this Rect rect1, Rect rect2, float tollerant = 0.5f) {
            return (rect1.dx_abs(rect2) + rect1.dy_abs(rect2) + rect1.dw_abs(rect2) + rect1.dh_abs(rect2)) > tollerant;
        }

        public static Rect Lerp(this Rect rect1, Rect rect2, float snap = 0.5f)
        {
            rect1.x         = rect1.x.LerpTo(rect2.x, snap);
            rect1.y         = rect1.y.LerpTo(rect2.y, snap);
            rect1.width     = rect1.width.LerpTo(rect2.width, snap);
            rect1.height    = rect1.height.LerpTo(rect2.height, snap);
            return rect1;
        }
        public static Rect Set(this Rect rect, float? x = null, float? y = null, float? w = null, float? h = null) {
            if (x!= null) rect.x        = x.Value;
            if (y!= null) rect.y        = y.Value;
            if (w!= null) rect.width    = w.Value;
            if (h!= null) rect.height   = h.Value;
            return rect;
        }

        public static Rect dx(this Rect rect, float val)
        {
            rect.x += val;
            return rect;
        }
        public static Rect dy(this Rect rect, float val)
        {
            rect.y += val;
            return rect;
        }
        public static Rect dw(this Rect rect, float val)
        {
            rect.width += val;
            return rect;
        }
        public static Rect dh(this Rect rect, float val)
        {
            rect.height += val;
            return rect;
        }

        public static Rect x(this Rect rect, float val)
        {
            rect.x = val;
            return rect;
        }
        public static Rect y(this Rect rect, float val)
        {
            rect.y = val;
            return rect;
        }
        public static Rect w(this Rect rect, float val) {
            rect.width = val;
            return rect;
        }
        public static Rect h(this Rect rect, float val) {
            rect.height = val;
            return rect;
        }

        public static Rect dl(this Rect rect, float val)
        {
            rect.xMin += val;
            return rect;
        }
        public static Rect dr(this Rect rect, float val)
        {
            rect.xMax += val;
            return rect;
        }
        public static Rect dt(this Rect rect, float val)
        {
            rect.yMin += val;
            return rect;
        }
        public static Rect db(this Rect rect, float val)
        {
            rect.yMax += val;
            return rect;
        }

        public static Rect l(this Rect rect, float val)
        {
            rect.xMin = val;
            return rect;
        }
        public static Rect r(this Rect rect, float val)
        {
            rect.xMax = val;
            return rect;
        }
        public static Rect t(this Rect rect, float val)
        {
            rect.yMin = val;
            return rect;
        }
        public static Rect b(this Rect rect, float val)
        {
            rect.yMax = val;
            return rect;
        }

        public static Vector2 XY_AsVector2(this Rect rect) { return new Vector2(rect.x, rect.y); }
        public static Vector2 WH_AsVector2(this Rect rect) { return new Vector2(rect.width, rect.height); }

        public static Rect Move(this Rect rect, float? l = null, float? r = null, float? t = null, float? b = null) {
            if (r != null) rect.xMax += r.Value;
            if (l != null) rect.xMin += l.Value;
            if (t != null) rect.yMin += t.Value;
            if (b != null) rect.yMax += b.Value;
            return rect;
        }
        public static Rect MoveUntilSize(this Rect rect, float? w = null, float? h = null) {
            if (w != null) rect.x = rect.width - w.Value;
            if (h != null) rect.y = rect.height - h.Value;
            return rect;
        }
        public static Rect Slide(this Rect rect, float pctX = 0, float pctY = 0) {
            rect.x += pctX * rect.width;
            rect.y += pctY * rect.height;
            return rect;
        }
        public static Rect Expand(this Rect rect, int px, int py) {
            rect.xMin -= px;
            rect.yMin -= py;
            rect.xMax += px;
            rect.yMax += py;
            return rect;
        }

        public static Rect GetFit(this Rect rect, float w, float h, float x = 0, float y = 0) {
            if (w==0 || h== 0) return new Rect();
            var s = Mathf.Min(h / rect.height, w / rect.width);
            var nw = s * rect.width;
            var nh = s * rect.height;
            return new Rect((w - nw) / 2f, (h - nh) / 2f, nw, nh);
        }
        public static Rect GetFill(this Rect rect, float w, float h, float x = 0, float y = 0) {
            if (w == 0 || h == 0) return new Rect();
            var s = Mathf.Max(h / rect.height, w / rect.width);
            var nw = s * rect.width;
            var nh = s * rect.height;
            return new Rect((w - nw) / 2f, (h - nh) / 2f, nw, nh);
        }
    }    
}