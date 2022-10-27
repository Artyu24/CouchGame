using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(CreatePointCircle))]
public class CreatePointCircleEditor : Editor
{
    private CreatePointCircle myObject = null;

    private Object source;
    private Object circle;

    private void OnEnable()
    {
        this.myObject = (CreatePointCircle) this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Outil création Spawn Point");
        EditorGUILayout.HelpBox("Attention, il vous manque des choses !", MessageType.Info);

        source = EditorGUILayout.ObjectField("Premier point de spawn vide :",source, typeof(Object), true);
        circle = EditorGUILayout.ObjectField("Le Cercle :", circle, typeof(Object), true);

        GameObject mySource = (GameObject) source;
        GameObject myCircle = (GameObject) circle;

        if (mySource == null || myCircle == null)
        {
            EditorGUILayout.HelpBox("Attention, il vous manque des choses !", MessageType.Warning);
            return;
        }


        myObject.FirstEmpty = mySource.transform;

        if (myObject.FirstEmpty != null)
        {
            if (GUILayout.Button("Création des points"))
            {
                if (myObject.FirstEmpty.position.x != 0)
                {
                    float r = myObject.FirstEmpty.position.x;
                    float deg = 0;
                    float degSup = 360 / 16;
                    for (int i = 0; i < 16; i++)
                    {
                        //Debug.Log(r * Mathf.Cos(Mathf.Deg2Rad * deg) + " // " + r * Mathf.Sin(Mathf.Deg2Rad * deg));
                        Instantiate(mySource, new Vector3(r * Mathf.Cos(Mathf.Deg2Rad * deg), 1, r * Mathf.Sin(Mathf.Deg2Rad * deg)), Quaternion.identity, myCircle.transform);
                        deg += degSup;
                    }
                }
            }
        }

        EditorGUILayout.EndHorizontal();
    }
}
