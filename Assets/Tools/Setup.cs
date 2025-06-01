using System.IO;
using UnityEditor;
using UnityEngine;

public static class Setup
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        Folder.CreateDefault("_Project", "Animations", "Arts", "Materials", "Prefabs", "ScriptableObjects",
            "Scripts");
        UnityEditor.AssetDatabase.Refresh();
    }

    static class Folder
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullPath = Path.Combine(Application.dataPath, root);
            foreach (var folder in folders)
            {
                var path = Path.Combine(fullPath, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}