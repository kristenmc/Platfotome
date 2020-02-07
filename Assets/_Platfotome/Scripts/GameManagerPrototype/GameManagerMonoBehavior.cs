using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class GameManagerMonoBehavior : MonoBehaviour {

		private void Awake() {
			GameManager.Initialize();
		}

		private void Start() {
			GameManager.LoadOpeningTitles();
		}

		private void Update() {
			GameManager.Update();
		}

	}

}