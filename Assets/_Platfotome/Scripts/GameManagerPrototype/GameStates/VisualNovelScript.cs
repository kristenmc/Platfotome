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

			if (GameManager.StateArgs.TryGetValue(VisualNovelState.DialogueKey, out string key)) {
				DialogueManager.LoadDialogue(key);
			}
		}

		private void Loaded() {
			dialogueScript.BeginReveal();
		}

		private void Update() {
			
			if (!GameManager.Paused) {

				if (DialogueManager.DialogueActive) {

					if (Input.GetButtonDown(Constants.Keys.AdvanceDialogue)) { 
						dialogueScript.AdvanceText();
					} else if (Input.GetButtonDown(Constants.Keys.SkipDialogue)) {
						dialogueScript.SkipToEnd();
					}
					return; // Don't allow instant selection / skip of text from immediate transition.

				}

				if (DialogueManager.ChoiceActive) {

					if (Input.GetKeyDown(KeyCode.UpArrow)) {
						choiceScript.ChoiceUp();
					} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
						choiceScript.ChoiceDown();
					} else if (Input.GetButtonDown(Constants.Keys.ChooseDialogueChoice)) {
						choiceScript.SelectCurrent();
					}

				}

			}
		}

		private void OnDestroy() {
			DialogueManager.DialogueScript = null;
			DialogueManager.ChoiceScript = null;
		}

	}

}