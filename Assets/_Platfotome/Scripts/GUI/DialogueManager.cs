using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome.GUI {

	public static class DialogueManager {

		private static readonly Dictionary<string, DialogueSequence> dialogue = new Dictionary<string, DialogueSequence>();
		private static readonly Dictionary<string, DialogueChoice> choices = new Dictionary<string, DialogueChoice>();
		internal static VisualNovelDialogueScript DialogueScript { get; set; }
		internal static VisualNovelChoiceScript ChoiceScript { get; set; }
		public static bool DialogueActive { get; internal set; }
		public static bool ChoiceActive { get; internal set; }

		internal const string Prefix = "[DialogueManager]";

		private const string Indent = "    ";

		public static bool Initialized { get; private set; }

		/// <summary>
		/// Call exactly once during game load to initalize all dialogue data.
		/// </summary>
		public static void Initialize(bool showOutput = false) {

			LoadDict(dialogue);
			LoadDict(choices);
			Initialized = true;

			if (showOutput) {
				Debug.Log($"<b>{Prefix} Start printout.</b>");


				var styles = dialogue.Select(x => x.Value).Select(x => x.style);

				Debug.Log(Indent + "Character Styles");
				foreach (var item in styles) {
					Debug.Log(Indent + Indent + $"{item.name} : {item}");
				}
				if (!styles.Any()) Debug.Log(Indent + Indent + "(none)");


				Debug.Log(Indent + "Dialogue Sequences");
				foreach (var item in dialogue) {
					Debug.Log(Indent + Indent + $"{item.Key} : {item.Value}");
				}
				if (dialogue.Count == 0) Debug.Log(Indent + Indent + "(none)");


				Debug.Log(Indent + "Dialogue Choices");
				foreach (var item in choices) {
					Debug.Log(Indent + Indent + $"{item.Key} : {item.Value}");
				}
				if (choices.Count == 0) Debug.Log(Indent + Indent + "(none)");


				Debug.Log(Indent + $"Loaded {dialogue.Count} dialogue sequences and {choices.Count} dialogue choices.");

				Debug.Log(Indent + "Initialization completed successfully.");

				Debug.Log($"<b>{Prefix} End printout.</b>");
			}

		}

		private static void LoadDict<T>(Dictionary<string, T> dict) where T: UnityEngine.Object {
			foreach (var item in Resources.LoadAll<T>("Dialogue")) {
				if (!dict.ContainsKey(item.name)) {
					dict.Add(item.name, item);
				} else {
					Debug.LogWarning(Prefix + $" Found duplicate key '{item.name}'. Ignoring duplicate.");
				}
			}
		}

		#region Dialogue

		/// <summary>
		/// Loads a dialogue sequence from the given key.
		/// </summary>
		public static void LoadDialogue(string key) {
			if (WarnInit($"Request to load dialogue key '{key}' failed.")) return;
			if (dialogue.TryGetValue(key, out DialogueSequence sequence)) {
				DialogueScript.LoadSequence(key, sequence);
			} else {
				Debug.LogError(Prefix + $" Failed to find requested dialogue key '{key}'");
			}
		}

		/// <summary>
		/// Begins the process of revealing text.
		/// </summary>
		public static void BeginReveal() {
			if (WarnInit("Request to begin revealing text failed.")) return;
			if (DialogueScript != null && DialogueActive) {
				DialogueScript.BeginReveal();
			}
		}

		public static void SkipToEnd() {
			if (WarnInit("Request to skip to end of text failed.")) return;
			if (DialogueScript != null && DialogueActive) {
				DialogueScript.SkipToEnd();
			}
		}

		/// <summary>
		/// Advance to the next page of text.
		/// </summary>
		public static void AdvanceDialogue() {
			if (WarnInit("Request to advance text failed.")) return;
			if (DialogueScript != null && DialogueActive) {
				DialogueScript.AdvanceText();
			}
		}

		/// <summary>
		/// Close the currently active dialogue sequence.
		/// </summary>
		public static void CloseCurrent() {
			if (WarnInit("Request to close dialogue box failed.")) return;
			if (DialogueScript != null && DialogueActive) {
				DialogueScript.Close();
			}
		}

		#endregion

		#region Choice Public Interface

		/// <summary>
		/// Load a dialogue choice from the given key.
		/// </summary>
		public static void LoadChoice(string key) {
			if (WarnInit($"Request to load choice key '{key}' failed.")) return;
			if (choices.TryGetValue(key, out DialogueChoice choice)) {
				ChoiceScript.LoadChoice(key, choice);
			} else {
				Debug.LogError(Prefix + $" Failed to find requested choice key '{key}'");
			}
		}

		/// <summary>
		/// Move the selected choice box down by 1.
		/// </summary>
		public static void ChoiceUp() => ChoiceDelta(-1);

		/// <summary>
		/// Move the selected choice box down by 1.
		/// </summary>
		public static void ChoiceDown() => ChoiceDelta(1);

		private static void ChoiceDelta(int delta) {
			if (WarnInit("Request to change choice selection failed.")) return;
			if (ChoiceScript != null && ChoiceActive) {
				ChoiceScript.ChangeSelectionIndex(delta);
			}
		}

		/// <summary>
		/// Select the current choice.
		/// </summary>
		public static void SelectCurrentChoice() {
			if (WarnInit("Request to select current choice failed.")) return;
			if (ChoiceScript != null && ChoiceActive) {
				ChoiceScript.SelectCurrent();
			}
		}

		#endregion

		private static bool WarnInit(string warning) {
			if (!Initialized) {
				Debug.LogError(Prefix + $" {warning} DialogueManager was not fully initialized.");
			}
			return !Initialized;
		}

	}

}