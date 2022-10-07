using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ChangeSpriteColor))]
public class ChangeSpriteColorEditor : Editor
{
    private ChangeSpriteColor myObject = null;

    private void OnEnable()
    {
        this.myObject = (ChangeSpriteColor) this.target;
    }

    public override void OnInspectorGUI()
    {
        //Custom Inspector content here
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("My Label");
        EditorGUILayout.HelpBox("Attention, vous êtes beau", MessageType.Warning);
        EditorGUILayout.EndHorizontal();

        this.myObject.myText = EditorGUILayout.TextField("My Text", this.myObject.myText);
    }
}
