using UnityEngine;

namespace vietlabs
{
    public static class vlbEvent
    {
        public static bool HasRightMouseDown(this Rect r) {
            var evt = Event.current;
            return evt.type == EventType.mouseDown && evt.button == 1 && r.Contains(evt.mousePosition);
        }
        public static bool HasLeftMouseDown(this Rect r) {
            var evt = Event.current;
            return evt.type == EventType.mouseDown && evt.button == 0 && r.Contains(evt.mousePosition);
        }
        public static bool HasRightMouseUp(this Rect r) {
            var evt = Event.current;
            return evt.type == EventType.mouseUp && evt.button == 1 && r.Contains(evt.mousePosition);
        }
        public static bool HasLeftMouseUp(this Rect r) {
            var evt = Event.current;
            return evt.type == EventType.mouseUp && evt.button == 0 && r.Contains(evt.mousePosition);
        }

        public static bool IsDown(this KeyCode c) {
            var evt = Event.current;
            return evt.type == EventType.keyDown && evt.keyCode == c;
        }
        public static bool IsUp(this KeyCode c) {
            var evt = Event.current;
            return evt.type == EventType.keyUp && evt.keyCode == c;
        }

        public static bool NoModifier(bool autoUseEvent = false) {
            var evt = Event.current;
            var result = !evt.control && !evt.alt && !evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool Alt(bool autoUseEvent = false) {
            var evt = Event.current;
            var result = !evt.control && evt.alt && !evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool Ctrl(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = evt.control && !evt.alt && !evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool Shift(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = !evt.control && !evt.alt && evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool CtrlAlt(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = evt.control && evt.alt && !evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool AltShift(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = !evt.control && evt.alt && evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool CtrlShift(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = evt.control && !evt.alt && evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }
        public static bool CtrlAltShift(bool autoUseEvent = false)
        {
            var evt = Event.current;
            var result = evt.control && evt.alt && evt.shift;
            if (result && autoUseEvent) evt.Use();
            return result;
        }

        public static bool IsLayout(this Event evt) {
            return evt.type == EventType.layout;
        }
        public static bool IsNotLayout(this Event evt) {
            return evt.type != EventType.layout;
        }
        public static bool IsNotUsed(this Event evt) {
            return evt.type != EventType.used;
        }
    }
}

