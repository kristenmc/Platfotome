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

		internal KeyWordArgs Args { get; }

		protected GameState(string sceneName, Transition transition = null, TransitionArguments transitionArguments = default, LoadSceneMode mode = LoadSceneMode.Single, bool loadAsync = false) {
			SceneName = sceneName;
			LoadMode = mode;
			LoadAsync = loadAsync;

			Transition = transition;
			TransitionArguments = transitionArguments;

			Args = new KeyWordArgs();
		}

		public override string ToString() {
			return string.Format("GameState('{0}', {1}, {2}, {3}, {4})",
				SceneName, Transition == null ? "None" : Transition.ToString(), Args, LoadMode, LoadAsync ? "Async" : "Immediate");
		}

	}

}