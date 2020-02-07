using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class __QuickTest__ : MonoBehaviour {

		private void Start() {

		}

		private void Update() {

			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				GameManager.RequestStateTransition(new MainMenuState());
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
			}

			//if (Input.GetKeyDown(KeyCode.Alpha1)) {
			//	DialogueManager.Load("test0");
			//}
			//if (Input.GetKeyDown(KeyCode.Alpha2)) {
			//	DialogueManager.Load("test1");
			//}
			//if (Input.GetKeyDown(KeyCode.RightArrow)) {
			//	DialogueManager.AdvanceDialogue();
			//}
			//if (Input.GetKeyDown(KeyCode.DownArrow)) {
			//	DialogueManager.CloseCurrent();
			//}
		}

	}

}