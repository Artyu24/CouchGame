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
        EditorGUILayout.HelpBox("Etape à suivre : \n Créé un empty object \n Le mettre en enfant du cercle choisis \n Le placer au centre de la surface du cercle avec une hauteur de 1 sur Y \n Ajouter le cercle et le point juste en dessous dans les cases correspondantes", MessageType.Info);
        EditorGUILayout.EndVertical();

        myObject.CircleParent = EditorGUILayout.ObjectField("Le Cercle :", myObject.CircleParent, typeof(Object), true);
        myObject.FirstEmpty = EditorGUILayout.ObjectField("Premier point de spawn vide :", myObject.FirstEmpty, typeof(Object), true);
        myObject.NbrSpawnPoint = EditorGUILayout.IntField("Nombre de point de spawn souhaité :", myObject.NbrSpawnPoint);


        GameObject mySource = null;
        GameObject myCircle = null;
        try
        {
            mySource = (GameObject)myObject.FirstEmpty;
            myCircle = (GameObject)myObject.CircleParent;
        }
        catch (Exception)
        {
            //Do nothing
        }

        if (mySource == null || myCircle == null)
        {
            EditorGUILayout.HelpBox("Attention, il vous manque des choses !", MessageType.Warning);
            return;
        }

        if (myObject.FirstEmpty != null)
        {
            float r = 0;
            if (mySource.transform.position.x != 0)
                r = mySource.transform.position.x;
            else if (mySource.transform.position.z != 0)
                r = mySource.transform.position.z;

            if (r == 0)
            {
                EditorGUILayout.HelpBox("Attention, votre point est en 0 0 !", MessageType.Warning);
                return;
            }

            PointAreaManager pointAreaManager = FindPointAreaManagerInScene();
            if (pointAreaManager == null)
            {
                EditorGUILayout.HelpBox("Attention, vous n'avez pas de PointAreaManager sur votre scène !", MessageType.Warning);
                return;
            }

            if (GUILayout.Button("Création des points"))
            {
                if (myObject.SpawnPointList.Count != 0)
                {
                    foreach (Transform point in myObject.SpawnPointList)
                    {
                        if (point == myObject.SpawnPointList[0])
                            continue;

                        pointAreaManager.SpawnPoint.Remove(point);
                        DestroyImmediate(point.gameObject);
                    }

                    myObject.SpawnPointList.Clear();
                }

                float deg = 0;
                float degSup = 360 / myObject.NbrSpawnPoint;
                deg += degSup;

                if(!myObject.SpawnPointList.Contains(mySource.transform))
                    myObject.SpawnPointList.Add(mySource.transform);

                if(!pointAreaManager.SpawnPoint.Contains(mySource.transform))
                    pointAreaManager.SpawnPoint.Add(mySource.transform);

                for (int i = 1; i < myObject.NbrSpawnPoint; i++)
                {
                    GameObject point = Instantiate(mySource, new Vector3(r * Mathf.Cos(Mathf.Deg2Rad * deg), 1, r * Mathf.Sin(Mathf.Deg2Rad * deg)), Quaternion.identity, myCircle.transform);
                    deg += degSup;
                    myObject.SpawnPointList.Add(point.transform);
                    pointAreaManager.SpawnPoint.Add(point.transform);
                }
            }

            if (myObject.SpawnPointList.Count != 0)
            {
                if (GUILayout.Button("Destruction des points"))
                {
                    foreach (Transform point in myObject.SpawnPointList)
                    { 
                        if(point == myObject.SpawnPointList[0])
                            continue;

                        pointAreaManager.SpawnPoint.Remove(point);
                        DestroyImmediate(point.gameObject);
                    }

                    myObject.SpawnPointList.Clear();
                }
            }
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

            PointAreaManager PAM = obj.GetComponent<PointAreaManager>();
            if (PAM != null)
                return PAM;
        }

        return null;
    }
}
