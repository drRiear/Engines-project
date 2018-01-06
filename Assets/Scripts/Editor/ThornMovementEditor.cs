using UnityEditor;

[CustomEditor(typeof(ThornMovement))]
[CanEditMultipleObjects]
public class ThornMovementEditor : Editor
{
    ThornMovement _script;
    SerializedProperty visRad;
    SerializedProperty agrRad;
    SerializedProperty type;

    public void OnEnable()
    {
        _script = (ThornMovement)target;
        visRad = serializedObject.FindProperty("visionRaduis");
        agrRad = serializedObject.FindProperty("agrRadius");
        type = serializedObject.FindProperty("type");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Choose type of thorn movement");
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.Separator();
        EditorGUILayout.PropertyField(agrRad);
        if (_script.type == ThornMovement.ThornType.returnable)
            EditorGUILayout.PropertyField(visRad);

        serializedObject.ApplyModifiedProperties();
    }
}
