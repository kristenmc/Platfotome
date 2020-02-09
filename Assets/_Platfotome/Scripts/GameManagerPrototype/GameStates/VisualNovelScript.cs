using Platfotome.GUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class VisualNovelScript : MonoBehaviour {

		public VisualNovelDialogueScript dialogueScript;
		public VisualNovelChoiceScript choiceScript;

		private void Awake() {
			DialogueManager.DialogueScript = dialogueScript;
			DialogueManager.ChoiceScript = choiceScript;
			if (GameManager.StateArgs.TryGetValue("key", out string key)) {
				DialogueManager.LoadDialogue(key);
			}
		}

		private void Loaded() {
			DialogueManager.BeginReveal();
		}

		private void Update() {

			// Must update dialogue choice first to prevent instant selection of first choice.
			if (DialogueManager.ChoiceActive) {

				if (Input.GetKeyDown(KeyCode.UpArrow)) {
					DialogueManager.ChoiceUp();
				}
				if (Input.GetKeyDown(KeyCode.DownArrow)) {
					DialogueManager.ChoiceDown();
				}
				if (Input.GetButtonDown(Constants.Keys.ChooseDialogueChoice)) {
					DialogueManager.SelectCurrentChoice();
				}

			}

			if (DialogueManager.DialogueActive) {

				if (Input.GetButtonDown(Constants.Keys.AdvanceDialogue)) { 
					DialogueManager.AdvanceDialogue();
				}
				if (Input.GetButtonDown(Constants.Keys.SkipDialogue)) {
					DialogueManager.SkipToEnd();
				}

			}
		}

		private void OnDestroy() {
			DialogueManager.DialogueScript = null;
		}

	}

}