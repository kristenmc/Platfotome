using Platfotome.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome {

	public abstract class GameState {

		public string SceneName { get; protected set; }
		public LoadSceneMode LoadMode { get; protected set; }
		public bool LoadAsync { get; protected set; }

		public Transition Transition { get; protected set; }
		public TransitionArguments TransitionArguments { get; protected set; }

		public bool AllowPause { get; protected set; }

		internal KeyWordArgs Args { get; }

		protected GameState(string sceneName, Transition transition = null, TransitionArguments transitionArguments = default) {
			SceneName = sceneName;
			LoadMode = LoadSceneMode.Single;
			LoadAsync = true;

			Transition = transition;
			TransitionArguments = transitionArguments;

			AllowPause = true;

			Args = new KeyWordArgs();
		}

		public override string ToString() {
			return string.Format("GameState('{0}', {1}, {2}, {3}, {4}, {5})",
				SceneName, Transition == null ? "None" : Transition.ToString(), Args, AllowPause ? "Pauseable" : "Not Pausable", LoadMode, LoadAsync ? "Async" : "Immediate");
		}

	}

}