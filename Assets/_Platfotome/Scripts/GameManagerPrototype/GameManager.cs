using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome {

	public static class GameManager {

		internal const string LogName = "[GameManager]";

		public static GameState CurrentState { get; private set; }

		private static GameObject transitionCanvas;

		private static readonly SimpleTimer transitionTimer = new SimpleTimer(1);
		private static GameState toLoad = null;

		internal static void Initialize() {
			SystemsInitializer.Initialize();
			transitionCanvas = GameObject.FindWithTag("TransitionCanvas");
		}

		internal static void LoadOpeningTitles() {
			Load(new OpeningTitlesState());
		}

		internal static void Update() {
			transitionTimer.Update();

			if (transitionTimer.Done && toLoad != null) {
				LoadInternal(toLoad);
				toLoad = null;
			}
		}

		private static void Load(GameState state) {

			CurrentState = state;

			if (state.Transition != null) {

				Transition transition = UnityEngine.Object.Instantiate(state.Transition, transitionCanvas.transform);
				transition.Config(state.TransitionArguments);

				toLoad = state;
				transitionTimer.Duration = state.TransitionArguments.InTime;
				transitionTimer.Start();

			} else {
				LoadInternal(state);
			}

		}

		private static void LoadInternal(GameState state) {
			Debug.Log($"<b>{LogName} Loading {state}</b>");
			SceneManager.LoadSceneAsync(state.SceneName, state.LoadMode);
		}

		internal static void TransitionComplete() {
			foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects()) {
				item.SendMessage("Loaded", SendMessageOptions.DontRequireReceiver);
			}
		}

		/// <summary>
		/// Request a transition to a new game state.
		/// </summary>
		public static void RequestStateTransition(GameState state) {
			if (GameStateTransitions.IsLegal(CurrentState, state)) {
				Load(state);
			}
		}

	}

}