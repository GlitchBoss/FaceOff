using UnityEngine;
using System.Collections;
using UnityEditor;
using Data;

[CustomEditor(typeof(Face))]
public class FaceEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}
}
