using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome.GUI {

	public static class DialogueManager {

		private static readonly Dictionary<string, DialogueSequence> dialogue = new Dictionary<string, DialogueSequence>();
		private static VisualNovelContainerReferences references;

		internal const string Prefix = "[DialogueManager]";

		private const string Indent = "    ";

		public static bool Initialized { get; private set; }

		/// <summary>
		/// Call exactly once during game load to initalize all dialogue data.
		/// </summary>
		public static void Initialize(bool showOutput = false) {

			foreach (var item in Resources.LoadAll<DialogueSequence>("Dialogue")) {
				if (!dialogue.ContainsKey(item.name)) {
					dialogue.Add(item.name, item);
				} else {
					Debug.LogWarning(Prefix + $" Found duplicate key '{item.name}'. Ignoring duplicate.");
				}
			}

			try {
				var canvas = GameObject.FindWithTag("MainCanvas");
				var prefab = Resources.Load<GameObject>("GUI/Visual Novel");
				references = UnityEngine.Object.Instantiate(prefab, canvas.transform).GetComponent<VisualNovelContainerReferences>();
				Initialized = true;
			} catch (NullReferenceException) {
				Debug.LogError(Prefix + " Failed to find Visual Novel References script. <b>Dialogue cannot be loaded.</b>");
				return;
			}

			if (showOutput) {
				Debug.Log($"<b>{Prefix} Start printout.</b>");

				Debug.Log(Indent + "Dialogue Sequences");
				foreach (var item in dialogue) {
					Debug.Log(Indent + Indent + $"{item.Key} : {item.Value}");
				}
				if (dialogue.Count == 0) Debug.Log(Indent + Indent + "(none)");

				var styles = dialogue.Select(x => x.Value).Select(x => x.style);

				Debug.Log(Indent + "Character Styles");
				foreach (var item in styles) {
					Debug.Log(Indent + Indent + $"{item.name} : {item}");
				}
				if (!styles.Any()) Debug.Log(Indent + Indent + "(none)");

				Debug.Log(Indent + $"Loaded {dialogue.Count} dialogue sequences and {styles.Count()} character styles.");

				Debug.Log(Indent + "Initialization completed successfully.");

				Debug.Log($"<b>{Prefix} End printout.</b>");
			}

		}

		/// <summary>
		/// Loads a dialogue sequence from the given key.
		/// </summary>
		public static void Load(string key, bool showOutput = false) {

			if (!Initialized) {
				Debug.LogError(Prefix + $" Request to load key '{key}' failed. DialogueManager was not fully initialized.");
				return;
			}

			if (dialogue.TryGetValue(key, out DialogueSequence sequence)) {
				references.LoadSequence(key, sequence);
			} else {
				Debug.LogError(Prefix + $" Failed to find requested key '{key}'");
			}

		}

		/// <summary>
		/// Advance to the next page of text.
		/// </summary>
		public static void AdvanceDialogue() {
			if (!Initialized) {
				Debug.LogError(Prefix + " Request to advance text failed. DialogueManager was not fully initialized.");
				return;
			}
			references.AdvanceText();
		}

		/// <summary>
		/// Close the currently active dialogue sequence.
		/// </summary>
		public static void CloseCurrent() {
			if (!Initialized) {
				Debug.LogError(Prefix + " Request to close dialogue box failed. DialogueManager was not fully initialized.");
				return;
			}
			references.Close();
		}

	}

}