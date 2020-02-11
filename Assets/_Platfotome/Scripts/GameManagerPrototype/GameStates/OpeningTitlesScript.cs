using System.Collections;
using TMPro;
using UnityEngine;

namespace Platfotome {

	public sealed class OpeningTitlesScript : MonoBehaviour {

		public string clipname;
		public GameObject sparkle;
		public Transform sparkleContainer;
		public TextMeshProUGUI textmesh;
		public float totalScrollTime = 1.2f;

		private Animator animator;
		private bool hasRequest;

		private void Awake() {
			hasRequest = false;
			animator = GetComponent<Animator>();
			animator.speed = 0;
			animator.Play(clipname);
			textmesh.maxVisibleCharacters = 0;
		}

		private void Loaded() {
			animator.speed = 1;
		}

		private void Update() {
			if (Input.GetButtonDown(Constants.Keys.SkipCutscene)) {
				RequestNext();
			}
		}

		public void BeginScroll() {
			StartCoroutine(RevealText());
		}

		IEnumerator RevealText() {

			for (int i = 1; i < textmesh.text.Length + 1; i++) {
				textmesh.maxVisibleCharacters = i;
				yield return new WaitForSecondsRealtime(totalScrollTime / textmesh.text.Length);
			}

			yield return new WaitForSecondsRealtime(0.3f);
			Instantiate(sparkle, sparkleContainer);

			yield return new WaitForSecondsRealtime(3f);

			RequestNext();

		}

		private void RequestNext() {
			if (!hasRequest) {
				GameManager.RequestStateTransition(new MainMenuState());
				hasRequest = true;
			}
		}

	}

}