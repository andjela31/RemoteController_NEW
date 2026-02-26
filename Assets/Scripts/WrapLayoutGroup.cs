using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrapLayoutGroup : LayoutGroup
{
    public float spacingX = 10f;
    public float spacingY = 10f;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
    }

    public override void CalculateLayoutInputVertical()
    {
        float width = rectTransform.rect.width;

        float x = padding.left;
        float y = padding.top;
        float rowHeight = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            LayoutElement le = child.GetComponent<LayoutElement>();

            float childWidth = le?.preferredWidth ?? child.sizeDelta.x;
            float childHeight = le?.preferredHeight ?? child.sizeDelta.y;

            if (x + childWidth + padding.right > width)
            {
                x = padding.left;
                y += rowHeight + spacingY;
                rowHeight = 0f;
            }

            x += childWidth + spacingX;
            rowHeight = Mathf.Max(rowHeight, childHeight);
        }

        SetLayoutInputForAxis(y + rowHeight + padding.bottom, y + rowHeight + padding.bottom, -1, 1);
    }

    public override void SetLayoutHorizontal()
    {
        SetChildren();
    }

    public override void SetLayoutVertical()
    {
        SetChildren();
    }

    private void SetChildren()
    {
        float panelWidth = rectTransform.rect.width;
        float x = padding.left;
        //float y = padding.top;
        float contentHeight = CalculateTotalHeight(panelWidth);
        float availableHeight = rectTransform.rect.height;

        float yOffset = 0f;

        if (childAlignment == TextAnchor.MiddleCenter ||
            childAlignment == TextAnchor.MiddleLeft ||
            childAlignment == TextAnchor.MiddleRight)
        {
            yOffset = (availableHeight - contentHeight) / 2f;
        }
        else if (childAlignment == TextAnchor.LowerCenter ||
                 childAlignment == TextAnchor.LowerLeft ||
                 childAlignment == TextAnchor.LowerRight)
        {
            yOffset = availableHeight - contentHeight;
        }

        float y = padding.top + yOffset;

        float rowHeight = 0f;

        List<RectTransform> currentRow = new List<RectTransform>();
        float rowWidth = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            LayoutElement le = child.GetComponent<LayoutElement>();
            float cw = le?.preferredWidth ?? child.sizeDelta.x;
            float ch = le?.preferredHeight ?? child.sizeDelta.y;

            float nextRowWidth = (currentRow.Count == 0 ? 0 : spacingX) + cw;

            if (x + nextRowWidth + padding.right > panelWidth && currentRow.Count > 0)
            {
                // Apply alignment
                float offset = GetRowOffset(rowWidth, panelWidth);
                foreach (var c in currentRow)
                {
                    c.anchoredPosition += Vector2.right * offset;
                }


                // Move to next row
                x = padding.left;
                y += rowHeight + spacingY;
                rowHeight = 0f;
                currentRow.Clear();
                rowWidth = 0f;
            }

            // Position element
            SetChildAlongAxis(child, 0, x, cw);
            SetChildAlongAxis(child, 1, y, ch);

            x += cw + spacingX;
            rowWidth += cw + (currentRow.Count == 0 ? 0 : spacingX);
            rowHeight = Mathf.Max(rowHeight, ch);
            currentRow.Add(child);
        }

        // Apply alignment to last row
        if (currentRow.Count > 0)
        {
            float offset = GetRowOffset(rowWidth, panelWidth);
            foreach (var c in currentRow)
            {
                c.anchoredPosition += Vector2.right * offset;
            }
        }
    }

    private float GetRowOffset(float rowWidth, float totalWidth)
    {
        float availableWidth = totalWidth - padding.left - padding.right;

        switch (childAlignment)
        {
            case TextAnchor.UpperCenter:
            case TextAnchor.MiddleCenter:
            case TextAnchor.LowerCenter:
                return (availableWidth - rowWidth) / 2f;

            case TextAnchor.UpperRight:
            case TextAnchor.MiddleRight:
            case TextAnchor.LowerRight:
                return availableWidth - rowWidth;

            default: // Left
                return 0f;
        }
    }

    private float CalculateTotalHeight(float panelWidth)
    {
        float x = padding.left;
        float rowHeight = 0f;
        float totalHeight = padding.top + padding.bottom;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            LayoutElement le = child.GetComponent<LayoutElement>();

            float cw = le?.preferredWidth ?? child.sizeDelta.x;
            float ch = le?.preferredHeight ?? child.sizeDelta.y;

            if (x + cw + padding.right > panelWidth)
            {
                totalHeight += rowHeight + spacingY;
                x = padding.left;
                rowHeight = 0f;
            }

            x += cw + spacingX;
            rowHeight = Mathf.Max(rowHeight, ch);
        }

        totalHeight += rowHeight;
        return totalHeight;
    }

}
