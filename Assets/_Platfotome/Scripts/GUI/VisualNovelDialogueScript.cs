using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome.GUI {

	public class VisualNovelDialogueScript : MonoBehaviour {

		private const string Prefix = "[DialogueScript]";

		public float charactersPerSecond = 20;

		public bool revealingText;

		public RectTransform nameplate = null;
		public RectTransform frame = null;
		public RectTransform portrait = null;
		public RectTransform background = null;

		public TextMeshProUGUI textmesh = null;

		public string[] textSequence;
		public int curSequenceIndex = 0;

		private string CurrentText => textSequence[curSequenceIndex];

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
				textSequence = sequence.text;
				curSequenceIndex = 0;
				if (CheckLoadTrigger()) return;
				textmesh.text = CurrentText;
			} else {
				Debug.LogWarning(Prefix + $" Found no text to display for key '{key}'");
				textmesh.text = string.Empty;
			}

			textmesh.maxVisibleCharacters = 0;
			revealingText = false;
			gameObject.SetActive(true);
			DialogueManager.DialogueActive = true;

		}

		internal void AdvanceText() {
			if (revealingText) {
				SkipToEnd();
			} else if (curSequenceIndex < textSequence.Length - 1) {
				++curSequenceIndex;
				if (CheckLoadTrigger()) return;
				textmesh.text = CurrentText;
				BeginReveal();
			}
		}

		internal void BeginReveal() {
			if (!string.IsNullOrEmpty(textmesh.text)) {
				StopAllCoroutines();
				StartCoroutine(RevealCR());
			}
		}

		internal void Close() {
			StopAllCoroutines();
			DialogueManager.DialogueActive = false;
			gameObject.SetActive(false);
		}

		internal void SkipToEnd() {
			StopReveal();
		}

		private IEnumerator RevealCR() {
			textmesh.maxVisibleCharacters = 0;

			yield return new WaitForSecondsRealtime(0.05f);
			revealingText = true;

			for (int i = 0; i < textmesh.text.Length + 1; i++) {
				textmesh.maxVisibleCharacters = i;
				yield return new WaitForSecondsRealtime(1 / charactersPerSecond);
			}

			revealingText = false;
		}

		private void StopReveal() {
			StopAllCoroutines();
			textmesh.maxVisibleCharacters = textmesh.text.Length + 1;
			revealingText = false;
		}

		private void LoadPrefab(Transform container, GameObject prefab, string name, string key) {
			if (prefab != null) {
				Instantiate(prefab, container);
			} else {
				Debug.LogWarning(Prefix + $" Failed to resolve {name} for '{key}'");
			}
		}

		private bool CheckLoadTrigger() {
			string text = CurrentText;
			if (text.Length > 2 && text[0] == '$' && text[text.Length - 1] == '$') {
				DialogueManager.LoadChoice(text.Substring(1, text.Length - 2));
				Close();
				return true;
			}
			return false;
		}

	}

}