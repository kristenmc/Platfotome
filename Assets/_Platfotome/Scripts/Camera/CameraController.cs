using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class CameraController : MonoBehaviour {

		public static CameraController Instance { get; private set; }
		public static Camera MainCamera { get; set; }

		private Vector2 Position {
			get => transform.position;
			set => transform.position = new Vector3(value.x, value.y, transform.position.z);
		}

		public CameraMode mode;

		public Transform trackTarget;
		public float smoothDampTime = 0.05f;
		public CameraTrackObject track;
		private Vector2 trackSmoothDampVel;

		private Animator animator;


		public CameraController() {
			Instance = this;
		}

		private void Awake() {
			MainCamera = GetComponent<Camera>();
			animator = GetComponent<Animator>();
		}

		private void LateUpdate() {

			switch (mode) {
				case CameraMode.Track:
					DoTrack();
					break;
				case CameraMode.Animation:
					DoAnimate();
					break;
				case CameraMode.Map:
					DoMap();
					break;
				default:
					mode = CameraMode.Track;
					break;
			}
		}

		private void DoTrack() {
			Vector2 target = trackTarget == null ? Position : trackTarget.position.GetXY();
			if (track != null) {
				target = track.Clamp(target);
			}
			Position = Vector2.SmoothDamp(Position, target, ref trackSmoothDampVel, smoothDampTime);
		}

		private void DoAnimate() {

		}

		private void DoMap() {

		}

		public void SetViewportMode(ViewportMode mode) {
			switch (mode) {
				case ViewportMode.Normal:
					MainCamera.rect = new Rect(0, 0, 1, 1);
					break;
				case ViewportMode.Choiceworld:
					MainCamera.rect = GetChoiceWorldViewport();
					break;
			}

		}

		private static Rect GetChoiceWorldViewport() {
			RectTransform rt = GameObject.FindWithTag("ChoiceWorldViewport").GetComponent<RectTransform>();
			return Rect.MinMaxRect(rt.anchorMin.x, rt.anchorMin.y, rt.anchorMax.x, rt.anchorMax.y);
		}

		public void RequestAnimation(string clipname) {
			mode = CameraMode.Animation;
			animator.Play(clipname);
		}

	}

}