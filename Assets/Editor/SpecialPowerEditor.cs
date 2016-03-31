using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpecialPower)), CanEditMultipleObjects]
public class SpecialPowerEditor : Editor {

    public SerializedProperty
        duration_Prop,
        animBool_Prop,
		background_Prop,
        type_Prop,
        extraHealth_Prop,
        extraDamage_Prop,
        extraSpeed_Prop,
		fillHealth_Prop,
        maxDamage_Prop, 
        radius_Prop,
        delay_Prop,
        flash_Prop;
            

    void OnEnable()
    {
        // Setup the SerializedProperties
        duration_Prop = serializedObject.FindProperty("duration");
        animBool_Prop = serializedObject.FindProperty("animBool");
		background_Prop = serializedObject.FindProperty("background");
		type_Prop = serializedObject.FindProperty("type");
        extraHealth_Prop = serializedObject.FindProperty("extraHealth");
        extraDamage_Prop = serializedObject.FindProperty("extraDamage");
        extraSpeed_Prop = serializedObject.FindProperty("extraSpeed");
		fillHealth_Prop = serializedObject.FindProperty("fillHealth");
		maxDamage_Prop = serializedObject.FindProperty("maxDamage");
        radius_Prop = serializedObject.FindProperty("radius");
        delay_Prop = serializedObject.FindProperty("delay");
        flash_Prop = serializedObject.FindProperty("flash");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(duration_Prop);
        EditorGUILayout.PropertyField(animBool_Prop);
		EditorGUILayout.PropertyField(background_Prop);
		EditorGUILayout.PropertyField(type_Prop);
        SpecialPower.Type st = (SpecialPower.Type)type_Prop.enumValueIndex;

        switch (st)
        {
            case SpecialPower.Type.Enhance:
                EditorGUILayout.PropertyField(extraDamage_Prop);
                EditorGUILayout.PropertyField(extraHealth_Prop);
                EditorGUILayout.PropertyField(extraSpeed_Prop);
				EditorGUILayout.PropertyField(fillHealth_Prop);
				break;

            case SpecialPower.Type.Bomb:
                EditorGUILayout.PropertyField(maxDamage_Prop);
                EditorGUILayout.PropertyField(radius_Prop);
                EditorGUILayout.PropertyField(delay_Prop);
                EditorGUILayout.PropertyField(flash_Prop);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
