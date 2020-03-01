using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class GameManagerMonoBehavior : MonoBehaviour {

		private void Awake() {
			GameManager.Initialize();
		}

		private void Start() {
			GameManager.LoadOpeningTitles();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Alpha1)) GameManager.RequestStateTransition(new OverworldState());
			if (Input.GetKeyDown(KeyCode.Alpha2)) GameManager.RequestStateTransition(new VisualNovelState(AllCharacterAffinities.HeMan.GetDialogueKey()));
			if (Input.GetKeyDown(KeyCode.Alpha3)) GameManager.RequestStateTransition(new ChoiceWorldState("ChoiceWorld"));
			if (Input.GetKeyDown(KeyCode.Alpha4)) GameManager.RequestStateTransition(new MainMenuState());
			if (Input.GetKeyDown(KeyCode.Alpha5)) GameManager.RequestStateTransition(new ChoiceWorldState("ChoiceWorld_Demo"));
			if (Input.GetKeyDown(KeyCode.Alpha8)) GameManager.RequestStateTransition(new SettingsState());
			if (Input.GetKeyDown(KeyCode.Alpha9)) GameManager.RequestStateTransition(new CreditsState());

			if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.BackQuote)) {
				Debug.Log(GameManager.LogName + " Resetting preferences to factory default.");
				GameConfig.Current = GameConfig.GetDefault();
			}

			GameManager.Update();
		}

	}

}