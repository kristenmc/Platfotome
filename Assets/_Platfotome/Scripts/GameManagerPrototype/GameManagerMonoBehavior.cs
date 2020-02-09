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
				GameManager.RequestStateTransition(new VisualNovelState("test0"));
			}
			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				GameManager.RequestStateTransition(new ChoiceWorldState());
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) {
				GameManager.RequestStateTransition(new MainMenuState());
			}

			GameManager.Update();
		}

	}

}