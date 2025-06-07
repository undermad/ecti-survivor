#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
using Explorer._Project.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Explorer._Project.Scripts.Editor
{
    [InitializeOnLoad]
    public static class TagValidator
    {
        static TagValidator()
        {
            SyncTagsWithRegistry();
        }

        static void SyncTagsWithRegistry()
        {
            var currentTags = UnityEditorInternal.InternalEditorUtility.tags;

            var tagFields = typeof(TagsRegistry).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

            var registryTags = tagFields.Select(f => (string)f.GetRawConstantValue()).ToList();

            foreach (var tag in registryTags.Where(tag => !currentTags.Contains(tag)))
            {
                AddTag(tag);
            }

            foreach (var tag in currentTags)
            {
                if (tag != "Untagged" && !registryTags.Contains(tag))
                {
                    RemoveTag(tag);
                }
            }
        }

        static void AddTag(string tag)
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var tagsProp = tagManager.FindProperty("tags");

            for (var i = 0; i < tagsProp.arraySize; i++)
            {
                var t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(tag)) return;
            }

            tagsProp.InsertArrayElementAtIndex(0);
            tagsProp.GetArrayElementAtIndex(0).stringValue = tag;

            tagManager.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Added tag: {tag}");
        }

        private static void RemoveTag(string tag)
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var tagsProp = tagManager.FindProperty("tags");

            for (var i = 0; i < tagsProp.arraySize; i++)
            {
                var t = tagsProp.GetArrayElementAtIndex(i);
                if (!t.stringValue.Equals(tag)) continue;
                tagsProp.DeleteArrayElementAtIndex(i);
                tagsProp.DeleteArrayElementAtIndex(i);
                tagManager.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"Removed tag: {tag}");
                return;
            }
        }
    }
}
#endif