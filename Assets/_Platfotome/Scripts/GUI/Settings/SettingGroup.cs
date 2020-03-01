using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class SettingGroup : MonoBehaviour {

		private IEditableSetting[] allSettings;

		[SerializeField] private int m_selectedIndex = 0;

		public int SelectedIndex {
			get => m_selectedIndex;
			set {
				value = Util.Loop(value, 0, allSettings.Length);

				allSettings[m_selectedIndex].Deselect();
				allSettings[value].Select();
				m_selectedIndex = value;
			}
		}

		private void Awake() {

			allSettings = GetComponentsInChildren<IEditableSetting>();

			if (allSettings.Length == 0) {
				enabled = false;
				return;
			}
		}

        private void Start() {
            SelectedIndex = 0;
        }

        private void Update() {

			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				--SelectedIndex;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				++SelectedIndex;
			}


			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				allSettings[SelectedIndex].SetPrevious();
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				allSettings[SelectedIndex].SetNext();
			}

		}

	}

}