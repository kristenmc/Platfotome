﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class ChoiceTextAnimate : MonoBehaviour {

		public float amplitude = 2f;
		public float period = 3f;
		private Vector2 startPos;

		private Vector2 Position {
			get => transform.position;
			set => transform.position = value;
		}

		private void Awake() {
			startPos = Position;
		}

		private void Update() {
			Position = startPos + new Vector2(0, amplitude * Mathf.Pow(Mathf.Sin(2 * Mathf.PI * Time.time / period), 2));
		}

	}

}