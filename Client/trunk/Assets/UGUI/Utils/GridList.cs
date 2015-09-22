using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZGame.UI
{
    /// <summary>
    /// 处理列表项控件
    /// </summary>
    public class GridList : MonoBehaviour
    {
        private GridLayoutGroup widget;

        [ContextMenu("Execute")]
        public void ResizeGrid()
        {
            if (null == widget)
            {
                widget = gameObject.GetComponent<GridLayoutGroup>(); ;
            }

            RectTransform rect = widget.GetComponent<RectTransform>();
            int childCount = widget.transform.childCount;
            int constraintCount = widget.constraintCount;
            Vector2 cellSize = widget.cellSize;
            Vector2 spacing = widget.spacing;
            int count = Mathf.CeilToInt((float)childCount / constraintCount);

            switch (widget.constraint)
            {
                case GridLayoutGroup.Constraint.FixedColumnCount:
                    rect.sizeDelta = new Vector2(constraintCount * (cellSize.x + spacing.x), count * (cellSize.y + spacing.y));
                    break;
                case GridLayoutGroup.Constraint.FixedRowCount:
                    rect.sizeDelta = new Vector2(count * (cellSize.x + spacing.x), constraintCount * (cellSize.y + spacing.y));
                    break;
            }
        }
    }
}
