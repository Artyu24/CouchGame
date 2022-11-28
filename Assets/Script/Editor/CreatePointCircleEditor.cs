using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[CanEditMultipleObjects]
[CustomEditor(typeof(CreatePointCircle))]
public class CreatePointCircleEditor : Editor
{
    private CreatePointCircle myObject = null;

    private void OnEnable()
    {
        this.myObject = (CreatePointCircle) this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("-------Outil création Spawn Point-------");
        EditorGUILayout.HelpBox("Etape à suivre :\nPlacer le point au centre de la surface du cercle SUR X\n Ajouter le cercle et le point juste en dessous dans les cases correspondantes", MessageType.Info);
        EditorGUILayout.EndVertical();

        SerializedObject soCreaterPointCircle = new SerializedObject(myObject);
        soCreaterPointCircle.Update();

        SerializedProperty circle = soCreaterPointCircle.FindProperty("circleParent");
        SerializedProperty firstEmpty = soCreaterPointCircle.FindProperty("firstEmpty");
        SerializedProperty nbrPoint = soCreaterPointCircle.FindProperty("nbrSpawnPoint");
        SerializedProperty spawnPointList = soCreaterPointCircle.FindProperty("spawnPointList");

        circle.objectReferenceValue = EditorGUILayout.ObjectField("Le Cercle :", circle.objectReferenceValue, typeof(Object), true);
        firstEmpty.objectReferenceValue = EditorGUILayout.ObjectField("Premier point de spawn vide :", firstEmpty.objectReferenceValue, typeof(Object), true);
        nbrPoint.intValue = EditorGUILayout.IntField("Nombre de point de spawn souhaité :", nbrPoint.intValue);

        GameObject mySource = null;
        GameObject myCircle = null;
        try
        {
            mySource = (GameObject)firstEmpty.objectReferenceValue;
            myCircle = (GameObject)circle.objectReferenceValue;
        }
        catch (Exception)
        {
            //Do nothing
        }

        if (mySource == null || myCircle == null || nbrPoint.intValue <= 0)
        {
            EditorGUILayout.HelpBox("Attention, il vous manque des choses !", MessageType.Warning);
            soCreaterPointCircle.ApplyModifiedProperties();
            return;
        }

        mySource.transform.position = new Vector3(mySource.transform.position.x, 1, 0);

        if (firstEmpty.objectReferenceValue != null)
        {
            float r = 0;
            if (mySource.transform.position.x != 0)
                r = mySource.transform.position.x;

            if (r == 0)
            {
                EditorGUILayout.HelpBox("Attention, votre point est en 0 sur X !", MessageType.Warning);
                soCreaterPointCircle.ApplyModifiedProperties();
                return;
            }

            PointAreaManager pointAreaManager = FindPointAreaManagerInScene();
            if (pointAreaManager == null)
            {
                EditorGUILayout.HelpBox("Attention, vous n'avez pas de PointAreaManager sur votre scène !", MessageType.Warning);
                soCreaterPointCircle.ApplyModifiedProperties();
                return;
            }

            SerializedObject soPAM = new SerializedObject(pointAreaManager);
            soPAM.Update();
            SerializedProperty spawnManagerList = soPAM.FindProperty("spawnPoint");
            SerializedProperty spawnManagerPlayerList = soPAM.FindProperty("spawnPointPlayer");

            if (GUILayout.Button("Création des points"))
            {
                if (spawnPointList.arraySize != 0)
                {
                    int index = pointAreaManager.SpawnPoint.IndexOf((Transform)spawnPointList.GetArrayElementAtIndex(0).objectReferenceValue);
                    spawnManagerList.DeleteArrayElementAtIndex(index);
                    for (int i = 0; i < spawnPointList.arraySize; i++)
                    {
                        if (i == 0)
                            continue;

                        Transform point = (Transform)spawnPointList.GetArrayElementAtIndex(i).objectReferenceValue;
                        spawnManagerList.DeleteArrayElementAtIndex(index);
                        DestroyImmediate(point.gameObject);
                    }

                    spawnPointList.ClearArray();
                    spawnManagerPlayerList.ClearArray();
                }

                float deg = 0;
                float degSup = 360 / nbrPoint.intValue;
                deg += degSup;
                
                spawnPointList.InsertArrayElementAtIndex(0);
                spawnPointList.GetArrayElementAtIndex(0).objectReferenceValue = mySource.transform;

                spawnManagerList.InsertArrayElementAtIndex(0);
                spawnManagerList.GetArrayElementAtIndex(0).objectReferenceValue = mySource.transform;

                for (int i = 1; i < nbrPoint.intValue; i++)
                {
                    GameObject point = Instantiate(mySource, new Vector3(r * Mathf.Cos(Mathf.Deg2Rad * deg), 1, r * Mathf.Sin(Mathf.Deg2Rad * deg)), Quaternion.identity, myCircle.transform);
                    deg += degSup;
                    spawnPointList.InsertArrayElementAtIndex(i);
                    spawnPointList.GetArrayElementAtIndex(i).objectReferenceValue = point.transform;

                    spawnManagerList.InsertArrayElementAtIndex(i);
                    spawnManagerList.GetArrayElementAtIndex(i).objectReferenceValue = point.transform;
                }
            }

            if (spawnPointList.arraySize != 0)
            {
                if (GUILayout.Button("Destruction des points"))
                {
                    int index = pointAreaManager.SpawnPoint.IndexOf((Transform)spawnPointList.GetArrayElementAtIndex(0).objectReferenceValue);
                    spawnManagerList.DeleteArrayElementAtIndex(index);
                    for (int i = 0; i < spawnPointList.arraySize; i++)
                    {
                        if (i == 0)
                            continue;

                        spawnManagerList.DeleteArrayElementAtIndex(index);
                        Transform point = (Transform)spawnPointList.GetArrayElementAtIndex(i).objectReferenceValue;
                        DestroyImmediate(point.gameObject);
                    }

                    spawnPointList.ClearArray();
                    spawnManagerPlayerList.ClearArray();
                }

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("-------SPAWN POINT PLAYERS-------");
                EditorGUILayout.BeginHorizontal();

                if (!pointAreaManager.SpawnPointPlayer.Contains(mySource.transform))
                {
                    if (GUILayout.Button("AJOUTER"))
                    {
                        for (int i = 0; i < spawnPointList.arraySize; i++)
                        {
                            Transform point = (Transform)spawnPointList.GetArrayElementAtIndex(i).objectReferenceValue;
                            spawnManagerPlayerList.InsertArrayElementAtIndex(i);
                            spawnManagerPlayerList.GetArrayElementAtIndex(i).objectReferenceValue = point.transform;
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button("ENLEVER"))
                        spawnManagerPlayerList.ClearArray();

                    //Pas tout clear mek
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }

            soCreaterPointCircle.ApplyModifiedProperties();
            soPAM.ApplyModifiedProperties();
        }
    }

    public PointAreaManager FindPointAreaManagerInScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid())
            return null;

        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootGameObjects)
        {
            if (!obj.activeInHierarchy)
                continue;

            foreach (Transform child in obj.transform)
            {
                PointAreaManager PAM = child.GetComponent<PointAreaManager>();
                if (PAM != null)
                    return PAM;
            }
        }

        return null;
    }
}
