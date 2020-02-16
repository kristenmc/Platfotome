using System.Timers;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome.CustomEditors {

	internal static class EditorCommands {

		[InitializeOnLoadMethod]
		private static void SetStartupScene() {
			EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/_Platfotome/Game States/Startup.unity");
		}

		private static void Load(string name) {
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene($"Assets/_Platfotome/Game States/{name}.unity");
		}

		[MenuItem("Platfotome/Load/Startup #S")]
		private static void LoadStartup() => Load("Startup");
		[MenuItem("Platfotome/Load/Overworld #1")]
		private static void LoadOverworld() => Load("Overworld");
		[MenuItem("Platfotome/Load/Visual Novel #2")]
		private static void LoadVisualNovel() => Load("VisualNovel");
		[MenuItem("Platfotome/Load/Choice World #3")]
		private static void LoadChoiceWorld() => Load("ChoiceWorld");

		[MenuItem("Platfotome/Invoke Loaded() Event #L", priority = 51)]
		private static void InvokeLoadEvent() {
			if (Application.isPlaying) {
				foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects()) {
					item.SendMessage("Loaded", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

	}

}