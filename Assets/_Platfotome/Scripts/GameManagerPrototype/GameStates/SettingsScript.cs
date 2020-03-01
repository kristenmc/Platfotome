using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class SettingsScript : MonoBehaviour {

		private void OnDestroy() {
			Debug.Log("Serializing Changes");
			GameConfigManager.Instance.Save();
		}

	}

}