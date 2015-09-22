using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// 自动下滑到最新文本
/// </summary>
[RequireComponent(typeof(Text))]
public class ScrollText : MonoBehaviour
{
    public ScrollRect scroll;
    public Text content;

    StringBuilder sb;
    int maxLineCount = 20;

    List<string> records;

    void Awake()
    {
        scroll = gameObject.GetComponentInParent<ScrollRect>();
        content = gameObject.GetComponent<Text>();
        records = new List<string>();
    }

    public void AppendLine(string line)
    {
        if (records.Count >= maxLineCount)
        {
            records.RemoveAt(0);
        }
        records.Add(line);
        ScrollToEnd();
    }

    private void ScrollToEnd()
    {
        sb = new StringBuilder();
        foreach (string str in records)
        {
            sb.AppendLine(str);
        }
        content.text = sb.ToString();
        scroll.verticalNormalizedPosition = 0;
    }
}
