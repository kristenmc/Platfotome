using Platfotome.GUI;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

	[CustomPropertyDrawer(typeof(MetaLoadEntry))]
	public class MetaLoadEntryPropertyDrawer : PropertyDrawer {

		private bool open;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginProperty(position, label, property);

			float width = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 80f;

			open = EditorGUILayout.BeginFoldoutHeaderGroup(open, new GUIContent("Meta Load Entry"), EditorStyles.foldout);

			if (open) {

				var typeProp = property.FindPropertyRelative("type");

				EditorGUILayout.PropertyField(typeProp, new GUIContent("Load Type"));

				switch (typeProp.enumValueIndex) {
					case (int)MetaLoadType.None:
						break;
					case (int)MetaLoadType.Level:
						EditorGUILayout.ObjectField(property.FindPropertyRelative("level"));
						break;
					case (int)MetaLoadType.Dialogue:
						EditorGUILayout.ObjectField(property.FindPropertyRelative("dialogue"));
						break;
					case (int)MetaLoadType.Choice:
						EditorGUILayout.ObjectField(property.FindPropertyRelative("choice"));
						break;
				}

			}

			EditorGUILayout.EndFoldoutHeaderGroup();

			EditorGUIUtility.labelWidth = width;

			EditorGUI.EndProperty();

		}

	}



}