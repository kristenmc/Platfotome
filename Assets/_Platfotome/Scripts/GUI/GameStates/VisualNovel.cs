using Platfotome.GUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class VisualNovel : MonoBehaviour {

		public VisualNovelContainerReferences refScript;

		private void Awake() {
			DialogueManager.RefScript = refScript;
			DialogueManager.Load("test0");
		}

		private void OnDestroy() {
			DialogueManager.RefScript = null;
		}

	}

}