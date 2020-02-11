using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platfotome {

	public abstract class Transition : MonoBehaviour {

		[SerializeField] protected TransitionArguments arguments;

		private static bool loaded = false;
		protected static WaitUntil WaitUntilSceneLoad = new WaitUntil(() => loaded);

		static Transition() {
			SceneManager.sceneLoaded += (s, m) => loaded = true;
		}

		internal void Config(TransitionArguments args) {
			loaded = false;
			arguments = args;
		}

		public override string ToString() => $"Transition({GetType().Name}, {arguments})";

		protected void SetTransitionComplete() {
			GameManager.TransitionComplete();
		}

	}

}