using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

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
        GameManager gameManager = FindGameManagerInScene();
        if (gameManager == null)
            return;

        int nbrCircle = EditorGUILayout.IntField("Nombre de Cercle : ", gameManager.TabCircle.Length);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/CirclePrefab.prefab");

        if (prefab != null)
        {
            //float patate = EditorGUILayout.FloatField("Taille ", prefab.GetComponent<ProBuilderShape>().size.x);
        }
    }

    public static GameManager FindGameManagerInScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid())
            return null;

        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootGameObjects)
        {
            if(!obj.activeInHierarchy)
                continue;

            GameManager GM = obj.GetComponent<GameManager>();
            if (GM != null)
                return GM;
        }

        return null;
    }
}
