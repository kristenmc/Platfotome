using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class __QuickTest__ : MonoBehaviour {

		private void Start() {

		}

		private void Update() {

			if (Input.GetKeyDown(KeyCode.T)) {
				CameraController.Instance.RequestAnimation("TestCameraPan");
			}

			if (Input.GetKeyDown(KeyCode.I)) {
				DialogueManager.Load("test0");
			}
			if (Input.GetKeyDown(KeyCode.O)) {
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