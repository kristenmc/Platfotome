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
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				GameManager.RequestStateTransition(new OverworldState());
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				GameManager.RequestStateTransition(new VisualNovelState(AllCharacterAffinities.HeMan.GetDialogueKey()));
			}
			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				GameManager.RequestStateTransition(new ChoiceWorldState("test0"));
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) {
				GameManager.RequestStateTransition(new MainMenuState());
			}
			if (Input.GetKeyDown(KeyCode.Alpha6)) {
				GameManager.RequestStateTransition(new CreditsState());
			}
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                GameManager.RequestStateTransition(new ChoiceWorldState("A"));
            }

            if (Input.GetKeyDown(KeyCode.Keypad0)) {
                AllCharacterAffinities.HeMan.RequestTransition(HeManAffinity.State.Angry);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1)) {
                AllCharacterAffinities.HeMan.RequestTransition(HeManAffinity.State.Happy);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2)) {
                AllCharacterAffinities.HeMan.RequestTransition(HeManAffinity.State.Sad);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3)) {
                AllCharacterAffinities.HeMan.RequestTransition(HeManAffinity.State.Reconcile);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4)) {
                AllCharacterAffinities.HeMan.RequestTransition(HeManAffinity.State.End);
            }

            GameManager.Update();
		}

	}

}