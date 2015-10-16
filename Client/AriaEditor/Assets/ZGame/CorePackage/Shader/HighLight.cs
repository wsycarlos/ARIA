using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZGame.UI.Effect
{
    [AddComponentMenu("UI/Effects/HighLight", 14)]
    public class HighLight : BaseVertexEffect
    {
        [SerializeField]
        private Color m_EffectColor = new Color(0f, 0f, 0f, 0f);

        //[SerializeField]
        //private Vector2 m_EffectDistance = new Vector2(1f, -1f);

        //[SerializeField]
        //private bool m_UseGraphicAlpha = true;

        protected HighLight()
        { }

        //#if UNITY_EDITOR
        //        protected override void OnValidate()
        //        {
        //            effectDistance = m_EffectDistance;
        //            base.OnValidate();
        //        }

        //#endif

        public Color effectColor
        {
            get { return m_EffectColor; }
            set
            {
                m_EffectColor = value;
                if (graphic != null)
                    graphic.SetVerticesDirty();
            }
        }

        //public Vector2 effectDistance
        //{
        //    get { return m_EffectDistance; }
        //    set
        //    {
        //        if (value.x > 600)
        //            value.x = 600;
        //        if (value.x < -600)
        //            value.x = -600;

        //        if (value.y > 600)
        //            value.y = 600;
        //        if (value.y < -600)
        //            value.y = -600;

        //        if (m_EffectDistance == value)
        //            return;

        //        m_EffectDistance = value;

        //        if (graphic != null)
        //            graphic.SetVerticesDirty();
        //    }
        //}

        //public bool useGraphicAlpha
        //{
        //    get { return m_UseGraphicAlpha; }
        //    set
        //    {
        //        m_UseGraphicAlpha = value;
        //        if (graphic != null)
        //            graphic.SetVerticesDirty();
        //    }
        //}

        protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end)
        {
            UIVertex vt;

            var neededCpacity = verts.Count * 2;
            if (verts.Capacity < neededCpacity)
                verts.Capacity = neededCpacity;

            for (int i = start; i < end; ++i)
            {
                vt = verts[i];
                //var newColor = vt.color;
                //newColor.r = (byte)(newColor.r + (color.r / 255));//color.r);//
                //newColor.g = (byte)(newColor.g + (color.g / 255));//color.g);//
                //newColor.b = (byte)(newColor.b + (color.b / 255));//color.b);//
                //vt.color = newColor;
                vt.color = color;
                verts[i] = vt;
            }
        }

        public override void ModifyVertices(List<UIVertex> verts)
        {
            if (!IsActive())
                return;

            ApplyShadow(verts, effectColor, 0, verts.Count);
        }
    }
}