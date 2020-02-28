using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome.GUI {

	[Serializable]
	public class TextMeshEditField : MonoBehaviour {

		public TextMeshProUGUI textmesh;

		public bool isLeft;

		/// <summary>
		/// Raw text to display before effects applied.
		/// </summary>
		[SerializeField] private string m_baseText;
		public string BaseText {
			get => m_baseText;
			set {
				if (value != m_baseText) {
					m_baseText = value;
					UpdateTextMesh();
				}
			}
		}

		/// <summary>
		/// Whether current entry shows active angle effect
		/// </summary>
		[SerializeField] private bool m_activated;
		public bool Activated {
			get => m_activated;
			set {
				m_activated = value;
				UpdateTextMesh();
			}
		}

		private void Awake() {
			m_baseText = textmesh.text;
			m_activated = false;
		}

		private void UpdateTextMesh() {
			if (Activated) {
				textmesh.text = isLeft ? "< " + BaseText : BaseText + " >";
			} else {
				textmesh.text = BaseText;
			}
		}

	}

}