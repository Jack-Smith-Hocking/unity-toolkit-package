using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LordSheo.Editor.ContextActions
{
    public static class LayoutGroupContextActions
    {
        [MenuItem("GameObject/LordSheo/UI/V-Layout", validate = true)]
        private static bool ValidateCreateVerticalLayoutGroup()
        {
            var selected = Selection.activeGameObject;

            if (selected == null)
            {
                return false;
            }
            
            // Only get a GameObject in an open scene.
            if (selected.scene.IsValid() == false)
            {
                return false;
            }

            return true;
        }
        
        [MenuItem("GameObject/LordSheo/UI/V-Layout", priority = -1)]
        private static void CreateVerticalLayoutGroup()
        {
            var selected = Selection.activeGameObject;

            if (selected == null)
            {
                return;
            }
            
            // Only get a GameObject in an open scene.
            if (selected.scene.IsValid() == false)
            {
                return;
            }

            Undo.RecordObject(selected, "Create Vertical Layout Group");
            
            var layoutGroupObject = new GameObject("V-Layout");
            layoutGroupObject.transform.SetParent(selected.transform);
            
            var layoutGroup = layoutGroupObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            
            CreateLayoutElement("Top", layoutGroupObject.transform);
            CreateLayoutElement("Middle", layoutGroupObject.transform);
            CreateLayoutElement("Bottom", layoutGroupObject.transform);
        }
        
        [MenuItem("GameObject/LordSheo/UI/H-Layout", validate = true)]
        private static bool ValidateCreateHorizontalLayoutGroup()
        {
            var selected = Selection.activeGameObject;

            if (selected == null)
            {
                return false;
            }
            
            // Only get a GameObject in an open scene.
            if (selected.scene.IsValid() == false)
            {
                return false;
            }

            return true;
        }
        
        [MenuItem("GameObject/LordSheo/UI/H-Layout", priority = -1)]
        private static void CreateHorizontalLayoutGroup()
        {
            var selected = Selection.activeGameObject;

            if (selected == null)
            {
                return;
            }
            
            // Only get a GameObject in an open scene.
            if (selected.scene.IsValid() == false)
            {
                return;
            }

            Undo.RecordObject(selected, "Create Horizontal Layout Group");
            
            var layoutGroupObject = new GameObject("H-Layout");
            layoutGroupObject.transform.SetParent(selected.transform);
            
            var layoutGroup = layoutGroupObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            
            CreateLayoutElement("Left", layoutGroupObject.transform);
            CreateLayoutElement("Middle", layoutGroupObject.transform);
            CreateLayoutElement("Right", layoutGroupObject.transform);
        }

        private static LayoutElement CreateLayoutElement(string name, Transform parent)
        {
            var element = new GameObject(name)
                .AddComponent<LayoutElement>();

            element.transform.SetParent(parent);
            
            element.flexibleWidth = 1;
            element.flexibleHeight = 1;
            
            return element;
        }
    }
}
