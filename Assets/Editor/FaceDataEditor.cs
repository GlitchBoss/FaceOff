using UnityEngine;
using UnityEditor;
using System.Collections;
using Data;

[CustomEditor(typeof(FaceData))]
public class FaceDataEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		FaceData faces = (FaceData)target;
		if (GUILayout.Button("Add Face"))
		{
			faces.AddFace();
		}
		if(GUILayout.Button("Remove Last Face"))
		{
			faces.RemoveLastFace();
		}
	}
}
