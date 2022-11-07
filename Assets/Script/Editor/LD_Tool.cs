using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class LD_Tool : EditorWindow
{
    private int nbrCircle = 2;
    private float sizeCircle = 12;
    private float heighCircle = 0.1f;
    private float surfaceSize = 2;

    private bool[] canControlTab;
    private float[] surfaceSizeTab;

    private Material[] tabMat = new Material[6];

    private Vector2 scroll = Vector2.zero;

    [MenuItem("Tools/Level Creator")]
    static void InitWindow()
    {
        LD_Tool window = GetWindow<LD_Tool>();
        window.titleContent = new GUIContent("Level Creator");
        window.Show();
    }

    private void Awake()
    {
        if (nbrCircle < 0)
            nbrCircle = 0;

        InitAllTab();
    }

    private void OnGUI()
    {
        #region Search all the files we need
        GameManager gameManager = FindGameManagerInScene();
        if (gameManager == null)
        {
            GameObject prefabGeneral = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/PREFAB_GENERAL.prefab");
            if (prefabGeneral == null)
            {
                EditorGUILayout.HelpBox("PROBLEME AVEC LA PREFAB GENERAL -> VOIR GP", MessageType.Error);
                return;
            }
            
            EditorGUILayout.HelpBox("VOUS DEVEZ INITIALISE LA SCENE", MessageType.Warning);
            
            if (GUILayout.Button("Print LD"))
            {
                GameObject parentPrefab = Instantiate(prefabGeneral);
                parentPrefab.transform.DetachChildren();
            }
            return;
        }

        SerializedObject GM = new SerializedObject(gameManager);
        GM.Update();

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/CirclePrefab.prefab");

        for (int i = 1; i <= 6; i++)
        {
            tabMat[i - 1] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Material/Circle_Mat_Proto/Circle_Mat_" + i.ToString() + ".mat");
        }
        #endregion

        if (prefab != null)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);

            nbrCircle = EditorGUILayout.IntField("Nombre de Cercle :", nbrCircle);
            sizeCircle = EditorGUILayout.FloatField("Taille du premier cercle :", sizeCircle);
            heighCircle = EditorGUILayout.FloatField("Hauteur des cercles :", heighCircle);

            if (nbrCircle <= 0 || sizeCircle <= 0 || heighCircle <= 0)
            {
                EditorGUILayout.EndScrollView();
                return;
            }

            if (canControlTab.Length != nbrCircle || surfaceSizeTab.Length != nbrCircle)
                InitAllTab();

            #region All the list

            #region Can Be possessed List
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("--Can be possessed--");

            for (int i = 0; i < nbrCircle; i++)
            {
                canControlTab[i] = EditorGUILayout.Toggle("Cercle " + (i + 1), canControlTab[i]);
            }

            EditorGUILayout.EndVertical();
            #endregion

            #region Surface List
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("--Surface--");

            for (int i = 0; i < nbrCircle; i++)
            {
                surfaceSizeTab[i] = EditorGUILayout.FloatField("Size " + (i + 1), surfaceSizeTab[i]);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            #endregion

            #endregion

            foreach (float size in surfaceSizeTab)
            {
                if (size <= 0)
                {
                    EditorGUILayout.EndScrollView();
                    return;
                }
            }
            
            if (GUILayout.Button("Print LD"))
            {
                #region Clear Old Circle
                //Reset et destruction de l'ancien terrain

                foreach (GameObject circle in gameManager.TabCircle)
                    DestroyImmediate(circle);

                foreach (GameObject circleBlock in gameManager.CircleBlockList)
                    DestroyImmediate(circleBlock);

                SerializedProperty spTabCircle = FindHiddenPropertyRelative(GM, "tabCircle");
                SerializedProperty spCircleBlockList = FindHiddenPropertyRelative(GM, "circleBlockList");


                spTabCircle.ClearArray();
                spCircleBlockList.ClearArray();
                #endregion

                #region New Circle Generation
                //On créé les nouveaux cercles
                Type probuilderShapeType = _GetProBuilderType();
                float sizeCircleBefore = sizeCircle;
                for (int i = 0; i < nbrCircle; i++)
                {
                    #region Setup Circle
                    GameObject newCircle = Instantiate(prefab);
                    newCircle.GetComponent<MeshRenderer>().material = tabMat[i];

                    Component proBuilderComponent = newCircle.GetComponent(probuilderShapeType);

                    //Surface
                    var so = new SerializedObject(proBuilderComponent);
                    so.FindProperty("m_Shape").FindPropertyRelative("m_Thickness").floatValue = surfaceSizeTab[i];
                    so.ApplyModifiedProperties();

                    //Taille
                    if (i == 0)
                        SetProBuilderSize(probuilderShapeType, proBuilderComponent, new Vector3(sizeCircle, heighCircle, sizeCircle));
                    else
                    {
                        SetProBuilderSize(probuilderShapeType, proBuilderComponent, new Vector3(sizeCircleBefore + surfaceSizeTab[i] * 2, heighCircle, sizeCircleBefore + surfaceSizeTab[i] * 2));
                        sizeCircleBefore += surfaceSizeTab[i] * 2;
                    }
                    #endregion

                    #region Add To System

                    if (canControlTab[i])
                    {
                        int arraySize = spTabCircle.arraySize;
                        spTabCircle.InsertArrayElementAtIndex(arraySize);
                        spTabCircle.GetArrayElementAtIndex(arraySize).objectReferenceValue = newCircle;
                    }
                    else
                    {
                        int arraySize = spCircleBlockList.arraySize;
                        spCircleBlockList.InsertArrayElementAtIndex(arraySize);
                        spCircleBlockList.GetArrayElementAtIndex(arraySize).objectReferenceValue = newCircle;
                    }
                    #endregion
                }
                #endregion
                
                GM.ApplyModifiedProperties();
            }

            EditorGUILayout.EndScrollView();
        }

        //Example de récupération d'une property provenant d'un script héritant d'un autre
        //En gros j'ai chopper la variable de type Shape dans le ProBuilderShape et étant donné que c'est une classe Pipe qui héite de Shape, je peux accéder par property relative à la classe Pipe et donc à sa thickness
        //Type probuilderShapeType = _GetProBuilderType();
        //Component proBuilderComponent = prefab.GetComponent(probuilderShapeType);
        //var so = new SerializedObject(proBuilderComponent);
        //so.FindProperty("m_Shape").FindPropertyRelative("m_Thickness").floatValue = 1.5f;
        //so.ApplyModifiedProperties();

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

    private void InitAllTab()
    {
        InitCanControlTab();
        InitSurfaceSizeTab();
    }

    private void InitCanControlTab()
    {
        canControlTab = new bool[nbrCircle];
        for (int i = 0; i < canControlTab.Length; i++)
            canControlTab[i] = true;
    }

    private void InitSurfaceSizeTab()
    {
        surfaceSizeTab = new float[nbrCircle];
        for (int i = 0; i < surfaceSizeTab.Length; i++)
            surfaceSizeTab[i] = surfaceSize;
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
                //Debug.Log("Setter");
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

    public SerializedProperty FindHiddenPropertyRelative(SerializedObject obj, string propertyName)
    {
        var property = obj.GetIterator();
        var enumerator = property.GetEnumerator();
        while (enumerator.MoveNext())
        {
            SerializedProperty spChild = enumerator.Current as SerializedProperty;
            if (spChild.name == propertyName)
                return spChild;
        }
        return null;
    }

}
