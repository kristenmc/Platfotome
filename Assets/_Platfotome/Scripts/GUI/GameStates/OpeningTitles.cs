using System.Collections;
using TMPro;
using UnityEngine;

namespace Platfotome {

	public sealed class OpeningTitles : MonoBehaviour {

		public string clipname;
		public TextMeshProUGUI textmesh;
		public float totalScrollTime = 1.2f;

		private Animator animator;

		private void Awake() {
			animator = GetComponent<Animator>();
			animator.speed = 0;
			animator.Play(clipname);
			textmesh.maxVisibleCharacters = 0;
		}

		private void Loaded() {
			animator.speed = 1;
		}

		public void BeginScroll() {
			StartCoroutine(RevealText());
		}

		IEnumerator RevealText() {

			for (int i = 1; i < textmesh.text.Length + 1; i++) {
				textmesh.maxVisibleCharacters = i;
				yield return new WaitForSecondsRealtime(totalScrollTime / textmesh.text.Length);
			}

			yield return new WaitForSecondsRealtime(3f);

			GameManager.RequestStateTransition(new MainMenuState());

		}

	}

}