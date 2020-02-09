using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome.CustomEditors {

	internal static class EditorCommands {

		[MenuItem("Platfotome/Play from Startup #P", priority = 0)]
		private static void PlayFromStartup() {
			if (!EditorApplication.isPlaying) {
				EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/Startup.unity");
				EditorApplication.EnterPlaymode();
			}
		}

		[MenuItem("Platfotome/Load/Startup #S")]
		private static void LoadStartup() => EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/Startup.unity");
		[MenuItem("Platfotome/Load/Overworld #1")]
		private static void LoadOverworld() => EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/Overworld.unity");
		[MenuItem("Platfotome/Load/Visual Novel #2")]
		private static void LoadVisualNovel() => EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/VisualNovel.unity");
		[MenuItem("Platfotome/Load/Choice World #3")]
		private static void LoadChoiceWorld() => EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/ChoiceWorld.unity");

		[MenuItem("Platfotome/Invoke Loaded() Event #L", priority = 50)]
		private static void InvokeLoadEvent() {
			if (Application.isPlaying) {
				foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects()) {
					item.SendMessage("Loaded", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

	}

}