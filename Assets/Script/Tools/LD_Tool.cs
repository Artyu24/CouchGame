using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LD_Tool : EditorWindow
{

    [MenuItem("Tools/Level Creator")]
    static void InitWindow()
    {
        LD_Tool window = GetWindow<LD_Tool>();
        window.titleContent = new GUIContent("Level Creator");
        window.Show();
    }

    private void OnGUI()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/CirclePrefab.prefab");
    }
}
