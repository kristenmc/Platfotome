using Platfotome.ScriptableDictInternal;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

    public class KeyValuePairRenderer : PropertyDrawer {

        private const float Pad = 3;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float keyWidth = position.width / 3;

            // Calculate rects
            var keyRect = new Rect(position.x, position.y, keyWidth, position.height);
            var valueRect = new Rect(position.x + keyWidth + Pad, position.y, position.width - keyWidth - Pad, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(keyRect, property.FindPropertyRelative("key"), GUIContent.none);
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }

    }

    [CustomPropertyDrawer(typeof(ObjectDictionary.KVPair))]
    public class ObjectDictionaryKVPairPropertyDrawer : KeyValuePairRenderer { }

    [CustomPropertyDrawer(typeof(ValueDictionary.KVPair))]
    public class ValueDictionaryKVPairPropertyDrawer : KeyValuePairRenderer { }

}
