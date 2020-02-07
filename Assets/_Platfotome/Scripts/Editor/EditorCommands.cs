using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome.Editor {

	internal static class EditorCommands {

		[MenuItem("Platfotome/Play from Startup #P", priority = 0)]
		private static void PlayFromStartup() {
			if (!EditorApplication.isPlaying) {
				EditorSceneManager.OpenScene("Assets/_Platfotome/Game States/Startup.unity");
				EditorApplication.EnterPlaymode();
			}
		}

		[MenuItem("Platfotome/Invoke Loaded() Event #L", priority = 1)]
		private static void InvokeLoadEvent() {
			if (Application.isPlaying) {
				foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects()) {
					item.SendMessage("Loaded", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

	}

}