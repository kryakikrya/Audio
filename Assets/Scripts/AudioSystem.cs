using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioDataSO : ScriptableObject
{
    [field: SerializeField] public DataTypes ShowListType = DataTypes.Agressive;

    [field: SerializeField] public string ID;

    [Space]
    [field: SerializeField] public DataTypes Type1 = DataTypes.Agressive;
    [field: SerializeField] public List<AudioData> AudioDatas1;

    [Space]
    [field: SerializeField] public DataTypes Type2 = DataTypes.Neutral;
    [field: SerializeField] public List<AudioData> AudioDatas2;

    [Space]
    [field: SerializeField] public DataTypes Type3 = DataTypes.Positive;
    [field: SerializeField] public List<AudioData> AudioDatas3;

    [TextAreaAttribute(5, 20)]
    [field: SerializeField] public string Description;
}

[Serializable]
public class AudioData
{
    [field: SerializeField] public AudioClip AudioClip;

    [field: Range(0, 1)]
    [field: SerializeField] public float Volume;
}

[CustomEditor(typeof(AudioDataSO))]
public class AudioDataCustomEditor : Editor
{
    private SerializedProperty _type;
    private SerializedProperty _idProperty;
    private SerializedProperty _list1Property;
    private SerializedProperty _list2Property;
    private SerializedProperty _list3Property;
    private SerializedProperty _descriptionProperty;

    private bool _showDescription;
    private bool _showList;
    private void OnEnable()
    {
        _type = serializedObject.FindProperty("ShowListType");
        _idProperty = serializedObject.FindProperty("ID");
        _list1Property = serializedObject.FindProperty("AudioDatas1");
        _list2Property = serializedObject.FindProperty("AudioDatas2");
        _list3Property = serializedObject.FindProperty("AudioDatas3");
        _descriptionProperty = serializedObject.FindProperty("Description");
    }

    public override void OnInspectorGUI()
    {
        AudioDataSO obj = (AudioDataSO)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(_type);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Show List"))
        {
            _showList = true;
        }
        if (GUILayout.Button("Hide List"))
        {
            _showList = false;
        }

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Show Description"))
        {
            _showDescription = true;
        }

        if (GUILayout.Button("Hide Description"))
        {
            _showDescription = false;
        }

        GUILayout.EndHorizontal();


        if (GUILayout.Button("Hide All"))
        {
            _showDescription = false;
            _showList = false;
        }

        EditorGUILayout.PropertyField(_idProperty);

        if (_showList)
        {
            if (obj.Type1 == obj.ShowListType)
            {
                EditorGUILayout.PropertyField(_list1Property);
            }
            if (obj.Type2 == obj.ShowListType)
            {
                EditorGUILayout.PropertyField(_list2Property);
            }
            if (obj.Type3 == obj.ShowListType)
            {
                EditorGUILayout.PropertyField(_list3Property);
            }
        }

        if (_showDescription)
        {
            EditorGUILayout.LabelField("Description:");
            EditorGUILayout.PropertyField(_descriptionProperty, new GUIContent());
        }

        serializedObject.ApplyModifiedProperties();
    }
}

public enum DataTypes
{
    Agressive,
    Neutral,
    Positive
}