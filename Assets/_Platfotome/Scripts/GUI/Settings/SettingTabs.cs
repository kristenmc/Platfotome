using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platfotome.GUI {

	/// <summary>
	/// This class is a bodge
	/// </summary>
	public class SettingTabs : MonoBehaviour {

		public GameObject[] targets;

		public Transform tabContainer;
		public Image[] tabs;

		[SerializeField] private int m_selectedIndex;
		public int SelectedIndex {
			get => m_selectedIndex;
			set {
				m_selectedIndex = Util.Loop(value, 0, targets.Length);
				for (int i = 0; i < targets.Length; i++) {
					targets[i].SetActive(i == m_selectedIndex);
					tabs[i].color = Util.SetAlpha(tabs[i].color, i == m_selectedIndex ? 1f : 0.4f);
				}
			}
		}

		private void Awake() {
			targets = new GameObject[transform.childCount];
			for (int i = 0; i < transform.childCount; i++) {
				targets[i] = transform.GetChild(i).gameObject;
			}

			tabs = new Image[tabContainer.childCount];
			for (int i = 0; i < tabContainer.childCount; i++) {
				tabs[i] = tabContainer.GetChild(i).GetComponentInChildren<Image>();
			}

			SelectedIndex = 0;
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.E)) {
				++SelectedIndex;
			}
			if (Input.GetKeyDown(KeyCode.Q)) {
				--SelectedIndex;
			}
		}

	}

}