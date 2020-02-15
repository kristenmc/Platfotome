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
		public static bool InGameState(Type gameStateType) => CurrentState == null ? false : CurrentState.GetType().IsEquivalentTo(gameStateType);
		public static KeyWordArgs StateArgs { get; private set; }

		private static Transform transitionCanvas;
		private static GameObject pauseGUI;

		private static readonly SimpleTimer transitionTimer = new SimpleTimer(1);
		private static GameState toLoad = null;

		private static bool paused_;
		public static bool Paused {
			get => paused_;
			set {
				if (CurrentState != null && CurrentState.AllowPause) {
					paused_ = value;
					Time.timeScale = paused_ ? 0f : 1f;
					pauseGUI.SetActive(paused_);
					Debug.Log(string.Format(LogName + " Game <b>{0}</b>", paused_ ? "Paused" : "Unpaused"));
				} else {
					Debug.Log("Pause blacklist");
				}
			}
		}

		internal static void Initialize() {
			SystemsInitializer.Initialize();
			GamePreferences.Apply();
			transitionCanvas = GameObject.FindWithTag("TransitionCanvas").transform;
			pauseGUI = GameObject.FindWithTag("PauseCanvas").transform.GetChild(0).gameObject;
			pauseGUI.SetActive(false);
			IsFullStartup = true;
		}

		internal static void LoadOpeningTitles() {
			Load(new OpeningTitlesState());
		}

		internal static void Update() {
			transitionTimer.Update();

			if (Input.GetButtonDown(Constants.Keys.Pause)) {
				Paused = !Paused;
			}

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