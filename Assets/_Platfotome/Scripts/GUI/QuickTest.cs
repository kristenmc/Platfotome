using Platfotome.GUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class QuickTest : MonoBehaviour {

		private void Start() {


		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				DialogueManager.Load("test0");
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				DialogueManager.Load("test1");
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				DialogueManager.AdvanceDialogue();
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				DialogueManager.CloseCurrent();
			}
		}

	}

}