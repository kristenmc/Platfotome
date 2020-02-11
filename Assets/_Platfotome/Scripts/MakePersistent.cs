using UnityEngine;

namespace Platfotome {

	public sealed class MakePersistent : MonoBehaviour {

		private void Awake() {
			DontDestroyOnLoad(gameObject);
		}

	}

}