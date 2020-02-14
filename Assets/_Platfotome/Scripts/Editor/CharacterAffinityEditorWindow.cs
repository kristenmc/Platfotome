using System;
using UnityEditor;
using UnityEngine;

namespace Platfotome.CustomEditors {

	public class CharacterAffinityEditorWindow : EditorWindow {

		[MenuItem("Platfotome/Character Affinity Window", priority = 0)]
		public static void OpenWindow() {
			CharacterAffinityEditorWindow window = (CharacterAffinityEditorWindow)GetWindow(typeof(CharacterAffinityEditorWindow));
			window.titleContent = new GUIContent("Character Affinity Window");
			window.Show();
		}

		private void OnGUI() {

			EditorGUILayout.LabelField("Character States", EditorStyles.boldLabel);
			EditorGUILayout.Separator();

			var width = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 70;
			Draw(AllCharacterAffinities.HeMan);
			Draw(AllCharacterAffinities.Turtle);
			EditorGUIUtility.labelWidth = width;

		}

		private void Draw<T>(CharacterAffinity<T> affinity) where T : Enum {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(affinity.Name);
			affinity._Editor_SetCurrentState(EditorGUILayout.EnumPopup(affinity.CurrentState, GUILayout.MaxWidth(120)));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
		}

	}

}