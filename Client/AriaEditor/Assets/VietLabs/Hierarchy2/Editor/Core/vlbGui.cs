using System;
using UnityEngine;

namespace vietlabs
{
    public struct vlbVtHzGL : IDisposable /* Using struct to prevent gc */
    {
        private readonly bool isHorz;

        public vlbVtHzGL(bool hz, params GUILayoutOption[] options) {
            isHorz = hz;
            if (isHorz) {
                GUILayout.BeginHorizontal(options);
            } else {
                GUILayout.BeginVertical(options);
            }
        }
        public vlbVtHzGL(bool hz, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            isHorz = hz;
            if (isHorz) {
                GUILayout.BeginHorizontal(content, style, options);
            } else {
                GUILayout.BeginVertical(content, style, options);
            }
        }
        public vlbVtHzGL(bool hz, Texture image, GUIStyle style, params GUILayoutOption[] options)
        {
            isHorz = hz;
            if (isHorz) {
                GUILayout.BeginHorizontal(image, style, options);
            } else {
                GUILayout.BeginVertical(image, style, options);
            }
        }
        public vlbVtHzGL(bool hz, string text, GUIStyle style, params GUILayoutOption[] options)
        {
            isHorz = hz;
            if (isHorz) {
                GUILayout.BeginHorizontal(text, style, options);
            } else {
                GUILayout.BeginVertical(text, style, options);
            }
        }
        public vlbVtHzGL(bool hz, GUIStyle style, params GUILayoutOption[] options) {
            isHorz = hz;
            if (isHorz) {
                GUILayout.BeginHorizontal(style, options);
            } else {
                GUILayout.BeginVertical(style, options);
            }
        }

        public void Dispose() {
            if (isHorz) {
                GUILayout.EndHorizontal();
            } else {
                GUILayout.EndVertical();
            }
        }
    }
    public struct vlbScrollG : IDisposable {
        private readonly bool scroll;

        public vlbScrollG(Rect clipRect, ref Rect contentRect, ref Vector2 scrollPosition) {
            var horz = contentRect.width > clipRect.width;
            var vert = contentRect.height > clipRect.height;
            scroll = horz || vert;

            if (!scroll) return;

            if (vert) contentRect.width  -= 18;
            if (horz) contentRect.height -= 18;
            scrollPosition = GUI.BeginScrollView(clipRect, scrollPosition, contentRect, horz, vert);
        }

        public void Dispose() {
            if (scroll) GUI.EndScrollView();
        }
    }

    public static class vlbGui {
        public static vlbVtHzGL HzLayout { get { return new vlbVtHzGL(true); } }
        public static vlbVtHzGL VtLayout { get { return new vlbVtHzGL(false); } }

        public static vlbVtHzGL HzLayout2(params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, options);
        }
        public static vlbVtHzGL VtLayout2(params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, options);
        }
                                       
        public static vlbVtHzGL HzLayout2(GUIContent content, GUIStyle style, params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, content, style, options);
        }
        public static vlbVtHzGL VtLayout2(GUIContent content, GUIStyle style, params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, content, style, options);
        }
                                       
        public static vlbVtHzGL HzLayout2(Texture image, GUIStyle style, params GUILayoutOption[] options){
            return new vlbVtHzGL(true, image, style, options);
        }
        public static vlbVtHzGL VtLayout2(Texture image, GUIStyle style, params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, image, style, options);
        }
                                        
        public static vlbVtHzGL HzLayout2(string text, GUIStyle style, params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, text, style, options);
        }
        public static vlbVtHzGL VtLayout2(string text, GUIStyle style, params GUILayoutOption[] options) {
            return new vlbVtHzGL(true, text, style, options);
        }
                                        
        public static vlbVtHzGL HzLayout2(GUIStyle style, params GUILayoutOption[] options)
        {
            return new vlbVtHzGL(true, style, options);
        }
        public static vlbVtHzGL VtLayout2(GUIStyle style, params GUILayoutOption[] options)
        {
            return new vlbVtHzGL(true, style, options);
        }

        public static vlbScrollG ScrollView(this Rect clipRect, ref Rect contentRect, ref Vector2 scrollPosition) {
            return new vlbScrollG(clipRect, ref contentRect, ref scrollPosition);
        }

        static internal GUIContent ToGUIContent(this string value) { return new GUIContent(value); }
        static internal GUIContent[] ToGUIContent(this string[] list)
        {
            var result = new GUIContent[list.Length];
            for (var i = 0; i < list.Length; i++)
            {
                result[i] = new GUIContent(list[i]);
            }
            return result;
        }
    }

    public static class vlbGuiHelper {
        public static bool ClickTexture(this Rect r, Texture2D tex) {
            GUI.DrawTexture(r, tex);
            return r.HasLeftMouseDown();
        }
        public static bool ClickLabel(this Rect r, ref string text) {
            GUI.Label(r, text);
            return r.HasLeftMouseDown();
        }
        public static bool ToggleTexture(this Rect r, bool isOn, Texture2D on, Texture2D off) {
            GUI.DrawTexture(r, isOn ? on : off);
            if (r.HasLeftMouseDown() && vlbEvent.NoModifier(true)) isOn = !isOn;
            return isOn;
        }
        public static bool Button_RMB(this Rect r, string text) {
            GUI.Button(r, text);
            return r.HasRightMouseDown();
        } 
    }
}

