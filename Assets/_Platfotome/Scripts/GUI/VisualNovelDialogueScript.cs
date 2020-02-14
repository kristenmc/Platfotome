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

		private DialogueSequence sequence;

		internal void ClearAll() {
			Util.DestroyChildGameObjects(nameplate);
			Util.DestroyChildGameObjects(frame);
			Util.DestroyChildGameObjects(portrait);
			Util.DestroyChildGameObjects(background);
			textmesh.text = string.Empty;
			sequence = null;
		}

		internal void LoadSequence(string key, DialogueSequence sequence) {

			ClearAll();
			this.sequence = sequence;

			revealingText = false;
			sequence.style.Resolve(out var np, out var fr, out var pt, out var bg);

			LoadPrefab(nameplate, np, "nameplate", key);
			LoadPrefab(frame, fr, "frame", key);
			LoadPrefab(portrait, pt, "portrait", key);
			LoadPrefab(background, bg, "background", key);

			if (sequence.text != null && sequence.text.Length > 0) {
				textSequence = sequence.text;
				curSequenceIndex = 0;
				if (CheckMetaLoad()) return;
				textmesh.text = CurrentText;
			} else {
				Debug.LogWarning(Prefix + $" Found no text to display for key '{key}'");
				textmesh.text = string.Empty;
			}

			textmesh.maxVisibleCharacters = 0;
			gameObject.SetActive(true);
			DialogueManager.DialogueActive = true;

		}

		/// <summary>
		/// Begins the process of revealing text.
		/// </summary>
		public void BeginReveal() {
			if (gameObject.activeSelf && !string.IsNullOrEmpty(textmesh.text)) {
				StopAllCoroutines();
				StartCoroutine(RevealCR());
			}
		}

		/// <summary>
		/// Advance to the next page of text.
		/// </summary>
		public void AdvanceText() {
			if (revealingText) {
				SkipToEnd();
			} else {
				++curSequenceIndex;
				if (CheckMetaLoad()) return;
				textmesh.text = CurrentText;
				BeginReveal();
			}
		}

		/// <summary>
		/// Close the currently active dialogue sequence.
		/// </summary>
		public void Close() {
			StopAllCoroutines();
			DialogueManager.DialogueActive = false;
			gameObject.SetActive(false);
		}

		/// <summary>
		/// Immediately finish the process of revealing text.
		/// </summary>
		public void SkipToEnd() {
			StopReveal();
		}

		private IEnumerator RevealCR() {
			textmesh.maxVisibleCharacters = 0;

			yield return new WaitForSeconds(0.05f);
			revealingText = true;

			for (int i = 0; i < textmesh.text.Length + 1; i++) {
				textmesh.maxVisibleCharacters = i;
				yield return new WaitForSeconds(1 / charactersPerSecond);
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

		private bool CheckMetaLoad() {
			if (curSequenceIndex >= textSequence.Length) {
				if (sequence.loadEntry != null && sequence.loadEntry.type != MetaLoadType.None) {
					if (sequence.loadEntry.type == MetaLoadType.Choice) Close();
					DialogueManager.ExecuteMetaLoad(sequence.loadEntry);
				} else {
					Debug.LogWarning(Prefix + " Reached end of dialogue without specifying a load");
				}
				return true;
			}
			return false;
		}

	}

}