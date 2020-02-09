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

		public int selectionIndex;
		private int choiceCount;

		private void Awake() {
			ClearAll();
			gameObject.SetActive(false);
		}

		internal void ClearAll() {
			Array.ForEach(buttons, x => Util.DestroyChildGameObjects(x));
			Util.DestroyChildGameObjects(portrait);
			Util.DestroyChildGameObjects(background);
		}

		internal void LoadChoice(string key, DialogueChoice choice) {
			ClearAll();

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

			ChangeSelectionIndex(0);

			DialogueManager.ChoiceActive = true;
			gameObject.SetActive(true);

		}

		Color[] tmpColor;

		internal void ChangeSelectionIndex(int delta) {
			selectionIndex += delta;
			selectionIndex %= choiceCount;
			if (selectionIndex < 0) selectionIndex += choiceCount;

			for (int i = 0; i < choiceCount; i++) {
				buttonRefs[i].image.color = i == selectionIndex ? Color.grey : tmpColor[i];
			}
		}

		internal void SelectCurrent() {
			buttonRefs[selectionIndex].image.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
			Debug.Log($"Selected choice {selectionIndex}");
		}

		internal void Close() {
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