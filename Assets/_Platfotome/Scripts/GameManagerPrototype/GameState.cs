using Platfotome.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome {

	public abstract class GameState {

		public string SceneName { get; }
		public LoadSceneMode LoadMode { get; }
		public bool LoadAsync { get; }

		public Transition Transition { get; }
		public TransitionArguments TransitionArguments { get; }

		protected GameState(string sceneName, Transition transition = null, TransitionArguments transitionArguments = default, LoadSceneMode mode = LoadSceneMode.Single, bool loadAsync = false) {
			SceneName = sceneName;
			LoadMode = mode;
			LoadAsync = loadAsync;

			Transition = transition;
			TransitionArguments = transitionArguments;
		}

		public override string ToString() {
			return string.Format("GameState('{0}', {1}, {2}, {3})",
				SceneName, Transition == null ? "None" : Transition.ToString(), LoadMode, LoadAsync ? "Async" : "Immediate");
		}

	}

}