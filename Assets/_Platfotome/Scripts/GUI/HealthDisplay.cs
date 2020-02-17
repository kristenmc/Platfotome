using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome {

	public class HealthDisplay : MonoBehaviour {

		public static HealthDisplay Instance { get; private set; }

		[SerializeField] private int m_health;
		public int Health { get => m_health; private set => m_health = value; }
		public int MaxHealth => hearts.Length;

		public HeartDisplayAnimate[] hearts;

		public float animationOffset = 0.1f;
		public float waveOffset = 0.1f;

		public HealthDisplay() {
			Instance = this;
		}

		private void Awake() {
			Health = MaxHealth;
			for (int i = 0; i < hearts.Length; i++) {
				hearts[i].GetComponentInChildren<Animator>().SetFloat("Offset", (hearts.Length - i + 1) * animationOffset);
				hearts[i].GetComponent<ChoiceTextAnimate>().offset = i * waveOffset;
			}
		}

		public void SetHealth(int amount, bool doAnimate = true) {

			amount = Mathf.Clamp(amount, 0, MaxHealth);

			if (doAnimate) {

				if (amount < Health) {
					for (int i = amount; i < Health; i++) {
						hearts[i].StartLose();
					}

				} else if (amount > Health) {
					for (int i = Health; i < amount; i++) {
						hearts[i].StartGain();
					}

				}

			}

			Health = amount;

		}

		private void OnDestroy() {
			Instance = null;
		}

	}

}