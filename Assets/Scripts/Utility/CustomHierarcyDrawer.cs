#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarcyDrawer
{
    //static Color selectionColor = new Color(61 / 255f, 96 / 255f, 145 / 255f) * 0.6f;
    static Color selectionColor = Color.white * 0.6f;
    
    static CustomHierarcyDrawer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOmGUI;
    }

    static void HandleHierarchyWindowItemOmGUI(int inSelectionID, Rect inSelectionRect)
    {
        EditorApplication.RepaintHierarchyWindow();

        GameObject obj = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

        if (obj != null)
        {
            var customItems = obj.GetComponents<CustomHierarchyItem>();

            if (customItems != null && customItems.Length > 0)
            {
                bool isSelected = Selection.instanceIDs.Contains(inSelectionID);

                foreach (var item in customItems)
                {
                    paintObject(item, inSelectionRect);
                }

                drawSelection(inSelectionRect, isSelected);
            }
        }

        EditorApplication.RepaintHierarchyWindow();
    }

    static void drawSelection(Rect inSelectionRect, bool isSelected)
    {
        if (isSelected)
        {
            Rect backgroundRect = inSelectionRect;
            backgroundRect.x = 0;
            backgroundRect.xMax *= 1.5f;

            //EditorGUI.DrawRect(BackgroundOffset, bgCol * 1.5f);
            //EditorGUI.DrawRect(BackgroundOffset, Color.white * 0.5f);
            EditorGUI.DrawRect(backgroundRect, selectionColor);
        }
    }

    static void paintObject(CustomHierarchyItem label, Rect inSelectionRect)
    {

        if (label != null && Event.current.type == EventType.Repaint)
        {
            #region Determine Styling

            Color bgCol = label.BackgroundColor;
            Color textCol = label.TextColor;
            FontStyle fontStyle = label.FontStyle;

            #endregion

            //Draw selected background color on to hierachy object
            #region Draw Background
            //Only draw background if background color is not completely transparent
            if (bgCol.a > 0f)
            {
                Rect backgroundRect = inSelectionRect;
                backgroundRect.x = 0;
                backgroundRect.xMax *= 1.5f;

                EditorGUI.DrawRect(backgroundRect, bgCol);
            }


            #endregion
            //Draw custom label on to hierachy object
            #region Draw Label
            if (bgCol.a > 0.8f && textCol.a > 0)
            {
                Rect textRect = inSelectionRect;
                textRect.x = textRect.x + 18.25f + label.TextOffsetPixel + inSelectionRect.width * label.TextOffsetPerct;
                textRect.y = textRect.y + 1.25f;

                EditorGUI.LabelField(textRect, label.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = textCol },
                    fontStyle = fontStyle
                });
            }
            #endregion


            //Draw selected icon on to hierachy object
            #region Draw Icon
            if (label.Texture != null)
                DrawIcon(inSelectionRect, label.Texture, label.IconSize, label.IconOffsetPerct, label.IconOffsetPixel);
            #endregion

            EditorApplication.RepaintHierarchyWindow();
        }
    }

    private static void DrawIcon(Rect rect, Texture tex, Vector2 iconSize, float offsetX, float pixelOffset)
    {
        Rect r;
        float weight = 0, height = 0;
        float w = iconSize.x;
        float h = iconSize.y;

        if (w <= 0) weight = 16; else weight = w;
        if (h <= 0) height = 16; else height = h;

        if (offsetX < 0)
            r = new Rect(rect.x + rect.width + pixelOffset, rect.y, weight, height);
        else
            r = new Rect((rect.x + rect.width) * offsetX + pixelOffset, rect.y, weight, height);

        GUI.DrawTexture(r, tex);
    }
}

#endif
