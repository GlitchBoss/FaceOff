using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpecialPower)), CanEditMultipleObjects]
public class SpecialPowerEditor : Editor {

    public SerializedProperty
        duration_Prop,
        animBool_Prop,
        type_Prop,
        extraHealth_Prop,
        extraDamage_Prop,
        extraSpeed_Prop,
        maxDamage_Prop, 
        radius_Prop,
        delay_Prop;
            

    void OnEnable()
    {
        // Setup the SerializedProperties
        duration_Prop = serializedObject.FindProperty("duration");
        animBool_Prop = serializedObject.FindProperty("animBool");
        type_Prop = serializedObject.FindProperty("type");
        extraHealth_Prop = serializedObject.FindProperty("extraHealth");
        extraDamage_Prop = serializedObject.FindProperty("extraDamage");
        extraSpeed_Prop = serializedObject.FindProperty("extraSpeed");
        maxDamage_Prop = serializedObject.FindProperty("maxDamage");
        radius_Prop = serializedObject.FindProperty("radius");
        delay_Prop = serializedObject.FindProperty("delay");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(duration_Prop);
        EditorGUILayout.PropertyField(animBool_Prop);
        EditorGUILayout.PropertyField(type_Prop);
        SpecialPower.Type st = (SpecialPower.Type)type_Prop.enumValueIndex;

        switch (st)
        {
            case SpecialPower.Type.Enhance:
                EditorGUILayout.PropertyField(extraDamage_Prop);
                EditorGUILayout.PropertyField(extraHealth_Prop);
                EditorGUILayout.PropertyField(extraSpeed_Prop);
                break;

            case SpecialPower.Type.Bomb:
                EditorGUILayout.PropertyField(maxDamage_Prop);
                EditorGUILayout.PropertyField(radius_Prop);
                EditorGUILayout.PropertyField(delay_Prop);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
