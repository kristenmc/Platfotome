using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class VisualNovelChoiceScript : MonoBehaviour {

		private const string Prefix = "[DialogueChoiceScript]";

		public RectTransform[] buttons = new RectTransform[0];
		private DialogueChoiceButtonReference[] buttonRefs;
		public RectTransform background = null;
		public RectTransform portrait = null;

		public GameObject buttonPrefab = null;

		public Color tmpHighlightColor;
		public Color tmpSelectColor;
		public float delayAfterSelect = 0.3f;

		public int selectionIndex;
		private int choiceCount;

		private DialogueChoice choice;

		internal void ClearAll() {
			Array.ForEach(buttons, x => Util.DestroyChildGameObjects(x));
			Util.DestroyChildGameObjects(portrait);
			Util.DestroyChildGameObjects(background);
		}

		internal void LoadChoice(string key, DialogueChoice choice) {
			ClearAll();

			this.choice = choice;

			choice.style.Resolve(out var np, out var fr, out var pt, out var bg);
			LoadPrefab(portrait, pt, "portrait", key);
			LoadPrefab(background, bg, "background", key);

			choiceCount = choice.Count;
			buttonRefs = new DialogueChoiceButtonReference[choiceCount];
			tmpColor = new Color[choiceCount];

			for (int i = 0; i < choiceCount; i++) {
				var refScript = Instantiate(buttonPrefab, buttons[i]).GetComponent<DialogueChoiceButtonReference>();
				refScript.textmesh.text = choice[i].text;
				buttonRefs[i] = refScript;
				tmpColor[i] = refScript.image.color;
			}

			selectionIndex = 0;
			ChangeSelectionIndex(0);

			DialogueManager.ChoiceActive = true;
			gameObject.SetActive(true);

		}

		Color[] tmpColor;

		/// <summary>
		/// Move the selected choice box down by 1.
		/// </summary>
		public void ChoiceUp() => ChangeSelectionIndex(-1);

		/// <summary>
		/// Move the selected choice box down by 1.
		/// </summary>
		public void ChoiceDown() => ChangeSelectionIndex(1);

		private void ChangeSelectionIndex(int delta) {
			selectionIndex += delta;
			selectionIndex %= choiceCount;
			if (selectionIndex < 0) selectionIndex += choiceCount;

			for (int i = 0; i < choiceCount; i++) {
				buttonRefs[i].image.color = i == selectionIndex ? tmpHighlightColor : tmpColor[i];
			}
		}

		/// <summary>
		/// Select the current choice.
		/// </summary>
		public void SelectCurrent() {
			buttonRefs[selectionIndex].image.color = tmpSelectColor;
			Invoke("SelectCurrentDelayed", delayAfterSelect);
		}

		private void SelectCurrentDelayed() {
			var selection = choice[selectionIndex];
			if (selection.loadType != MetaLoadType.None) {
				try {
					DialogueManager.LoadKey(selection.loadType, choice[selectionIndex].trigger);
				} catch (Exception) {
					Debug.LogError(Prefix + " Failed to execute choice output trigger");
				}
				if (!(selection.loadType == MetaLoadType.Choice || selection.loadType == MetaLoadType.Level)) {
					Close();
				}
			}
		}

		/// <summary>
		/// Close the currently active choice dialogue.
		/// </summary>
		public void Close() {
			StopAllCoroutines();
			gameObject.SetActive(false);
			DialogueManager.ChoiceActive = false;
		}

		private void LoadPrefab(Transform container, GameObject prefab, string name, string key) {
			if (prefab != null) {
				Instantiate(prefab, container);
			} else {
				Debug.LogWarning(Prefix + $" Failed to resolve {name} for '{key}'");
			}
		}

	}

}