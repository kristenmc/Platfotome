using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome {

	public static class GameManager {

		internal const string LogName = "[GameManager]";

		public static bool IsFullStartup { get; private set; }
		public static GameState CurrentState { get; private set; }
		/// <summary>
		/// Returns true if current game state is specified type.
		/// </summary>
		public static bool InGameState(Type gameStateType) => CurrentState.GetType().IsEquivalentTo(gameStateType);
		public static KeyWordArgs StateArgs { get; private set; }

		private static Transform transitionCanvas;

		private static readonly SimpleTimer transitionTimer = new SimpleTimer(1);
		private static GameState toLoad = null;

		internal static void Initialize() {
			SystemsInitializer.Initialize();
			transitionCanvas = GameObject.FindWithTag("TransitionCanvas").transform;
			IsFullStartup = true;
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
			StateArgs = state.Args; 

			if (state.Transition != null) {

				Util.DestroyChildGameObjects(transitionCanvas);
				Transition transition = UnityEngine.Object.Instantiate(state.Transition, transitionCanvas);
				transition.Config(state.TransitionArguments);

				toLoad = state;
				transitionTimer.Duration = state.TransitionArguments.InTime + 0.02f; // add safety buffer
				transitionTimer.Start();

			} else {
				LoadInternal(state);
			}

		}

		private static void LoadInternal(GameState state) {
			Debug.Log($"{LogName} Loading <b>{state.SceneName}</b>\n{state}");
			SceneManager.LoadSceneAsync(state.SceneName, state.LoadMode);
			CameraController.Instance.ResetProperties();
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