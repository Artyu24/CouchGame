using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(TestSpawnPoint))]
public class TestSpawnPointEditor : Editor
{
    private TestSpawnPoint myObject = null;

    private void OnEnable()
    {
        this.myObject = (TestSpawnPoint)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("TestSpawnPointEditor"))
        {
            RaycastHit hit;

            bool isGood = true;
            for (int j = -30; j < 360; j += 30)
            {
                Debug.DrawRay(myObject.transform.position, myObject.transform.up * -1 * 4, Color.green, 5.0f);
                Physics.Raycast(myObject.transform.position, myObject.transform.up * -1, out hit, 4);

                if (hit.transform == null)
                    isGood = false;
                else if (hit.transform.tag != "Platform")
                    isGood = false;

                myObject.transform.eulerAngles = new Vector3(20, j, 0);
            }

            myObject.transform.eulerAngles = Vector3.zero;

            Debug.Log(isGood);
        }
        EditorGUILayout.EndVertical();
    }
}
