using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome.GUI {

	internal class VisualNovelContainerReferences : MonoBehaviour {

		public float charactersPerSecond = 20;

		public RectTransform nameplate = null;
		public RectTransform frame = null;
		public RectTransform portrait = null;
		public RectTransform background = null;

		public TextMeshProUGUI textmesh = null;

		private const string Prefix = DialogueManager.Prefix;

		public string[] curText;
		public int curTextIndex = 0;

		private void Awake() {
			ClearAll();
			gameObject.SetActive(false);
		}

		internal void ClearAll() {
			Util.DestroyChildGameObjects(nameplate);
			Util.DestroyChildGameObjects(frame);
			Util.DestroyChildGameObjects(portrait);
			Util.DestroyChildGameObjects(background);
			textmesh.text = string.Empty;
		}

		internal void LoadSequence(string key, DialogueSequence sequence) {
			ClearAll();

			sequence.style.Resolve(out var np, out var fr, out var pt, out var bg);

			LoadPrefab(nameplate, np, "nameplate", key);
			LoadPrefab(frame, fr, "frame", key);
			LoadPrefab(portrait, pt, "portrait", key);
			LoadPrefab(background, bg, "background", key);

			if (sequence.text != null && sequence.text.Length > 0) {
				curText = sequence.text;
				curTextIndex = 0;
				gameObject.SetActive(true);
				SetText(curTextIndex);
			} else {
				Debug.LogWarning(Prefix + $" Found no text to display for key '{key}'");
				textmesh.text = string.Empty;
				gameObject.SetActive(true);
			}


		}

		private void LoadPrefab(Transform container, GameObject prefab, string name, string key) {
			if (prefab != null) {
				Instantiate(prefab, container);
			} else {
				Debug.LogWarning(Prefix + $" Failed to resolve {name} for '{key}'");
			}
		}

		internal void AdvanceText() {
			if (curTextIndex < curText.Length - 1) {
				++curTextIndex;
				SetText(curTextIndex);
			} else {
				Close();
			}
		}

		private void SetText(int index) {
			textmesh.text = curText[index];
			StopAllCoroutines();
			StartCoroutine(RevealCR());
		}

		internal void Close() {
			StopAllCoroutines();
			gameObject.SetActive(false);
		}

		private IEnumerator RevealCR() {
			textmesh.maxVisibleCharacters = 0;

			yield return new WaitForSecondsRealtime(0.05f);
			for (int i = 0; i < textmesh.text.Length + 1; i++) {
				textmesh.maxVisibleCharacters = i;
				yield return new WaitForSecondsRealtime(1 / charactersPerSecond);
			}
		}



	}

}