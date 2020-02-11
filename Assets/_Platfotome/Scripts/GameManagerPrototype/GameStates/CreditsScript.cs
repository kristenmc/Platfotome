using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Platfotome {

	public class CreditsScript : MonoBehaviour {

		public TextMeshProUGUI title;
		public RectTransform scrollBox;
		public TextMeshProUGUI mainText;
		public TextAsset creditText;

		public Gradient titleGradient;

		public float fullRainbowTime = 10;
		public float scrollSpeed = 5f;
		public float fastScrollSpeed = 5f;

		public void Awake() {
			mainText.text = ParseText(creditText.text);
		}

		private static string ParseText(string raw) {
			StringBuilder sb = new StringBuilder();

			void Append(string style, string text) => sb.AppendLine($"<style=\"{style}\">{text.Substring(1)}</style>");

			foreach (string line in raw.Split('\n')) {
				if (line.Length >= 2) {
					if (line[0] == '#') {
						Append("Header", line);
					} else if (line[0] == '@') {
						Append("Contributor", line);
					} else if (line[0] == '>') {
						Append("Info", line);
					} else if (line[0] == '^') {
						Append("TinyInfo", line);
					}
				} else {
					sb.AppendLine(line);
				}
			}

			return sb.ToString();
		}

		private void Update() {
			title.color = titleGradient.Evaluate(Time.time / fullRainbowTime % 1f);
			float speed = Input.GetButton(Constants.Keys.SkipCutscene) ? fastScrollSpeed : scrollSpeed;
			scrollBox.transform.Translate(new Vector3(0, (float)Screen.width / Screen.height * speed * Time.deltaTime));
		}

	}

}