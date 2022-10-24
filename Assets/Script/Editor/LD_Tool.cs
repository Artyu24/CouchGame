using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class LD_Tool : EditorWindow
{
    private Vector3 s = new Vector3(15,0.1f,15);

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

        }

        //Example with Prefab
        //Type probuilderShapeType = _GetProBuilderType();
        //Component proBuilderComponent = prefab.GetComponent(probuilderShapeType);
        //SetProBuilderSize(probuilderShapeType, proBuilderComponent, s);

        //Example ft.Saint Jerome Audo
        //Type probuilderShapeType = _GetProBuilderType();
        //Component[] tabCompo = FindProbuilderPipeInScene();
        //Component proBuilderComponent = tabCompo[0];
        //SetProBuilderSize(probuilderShapeType, proBuilderComponent, s);
    }

    private static Type _GetProBuilderType()
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type proBuilderType = assembly.GetType("UnityEngine.ProBuilder.Shapes.ProBuilderShape");
            if (null != proBuilderType)
            {
                return proBuilderType;
            }
        }

        return null;
    }

    private void SetProBuilderSize(Type proBuilderType, Component component, Vector3 size)
    {
        PropertyInfo sizePropertyInfo = proBuilderType.GetProperty("size");
        MethodInfo[] methInfos = sizePropertyInfo.GetAccessors();
        for (int ctr = 0; ctr < methInfos.Length; ctr++)
        {
            MethodInfo m = methInfos[ctr];
            // Determine if this is the property getter or setter.
            if (m.ReturnType == typeof(void))
            {
                Debug.Log("Setter");
                //  Set the value of the property.
                m.Invoke(component, new object[] { size });
            }
        }
        CallProBuilderRebuildMethod(proBuilderType, component);
    }

    private void CallProBuilderRebuildMethod(Type proBuilder, Component component)
    {
        MethodInfo[] methodInfos = proBuilder.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (methodInfo.Name == "Rebuild" && methodInfo.GetParameters().Length == 0)
            {
                methodInfo.Invoke(component, null);
            }
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

    public static Component[] FindProbuilderPipeInScene()
    {
        List<Component> resultList = new List<Component>();

        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid())
            return resultList.ToArray();

        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootGameObjects)
        {
            if (!obj.activeInHierarchy)
                continue;

            Type typeObject = _GetProBuilderType();
            Component[] componentObject = obj.GetComponentsInChildren(typeObject);
            resultList.AddRange(componentObject);
        }

        return resultList.ToArray();
    }

}
