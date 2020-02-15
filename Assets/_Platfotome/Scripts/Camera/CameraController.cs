using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public class CameraController : MonoBehaviour {

		public static CameraController Instance { get; private set; }
		public static Camera MainCamera { get; private set; }

		/// <summary>
		/// Raised every LateUpdate() after the camera has moved. Includes offset from screenshake.
		/// </summary>
		public static Action<Vector2> OnCameraMove;

		/// <summary>
		/// Raised every LateUpdate() after the camera has moved. Does not include offset from screenshake.
		/// </summary>
		public static Action<Vector2> OnCameraMoveRaw;


		[SerializeField] private Vector2 m_position;
		/// <summary>
		/// The raw position of the camera before screenshake is applied.
		/// <para><i>Use this property over transform.position</i></para>
		/// </summary>
		public Vector2 Position {
			get => m_position;
			set => m_position = value;
		}

		[SerializeField] private CameraMode m_mode;
		public CameraMode Mode {
			get => m_mode;
			private set => m_mode = value;
		}

		public Transform trackTarget;
		public float smoothDampTime = 0.05f;
		public CameraTrackObject track;
		private Vector2 trackSmoothDampVel;

		private Animator animator;
		[SerializeField] private SimpleTimer animationTimer = new SimpleTimer();

		private float defaultZoom;

		public ScreenShakeTool screenshake;

		public CameraController() {
			Instance = this;
			screenshake = new ScreenShakeTool();
		}

		private void Awake() {
			MainCamera = GetComponent<Camera>();
			animator = GetComponent<Animator>();

			MainCamera.cullingMask &= ~Constants.Mask.UIScroll;
			defaultZoom = MainCamera.orthographicSize;
			ResetProperties();
		}

		private void LateUpdate() {

			switch (Mode) {
				case CameraMode.Idle:
					break;
				case CameraMode.Track:
					DoTrack();
					break;
				case CameraMode.Animation:
					DoAnimate();
					break;
				case CameraMode.Map:
					break;
				default:
					Mode = CameraMode.Idle;
					break;
			}

			if (Mode != CameraMode.Animation) {

				screenshake.Update();
				Vector2 pos = Position + screenshake.Offset;

				transform.position = new Vector3(pos.x, pos.y, transform.position.z);

				OnCameraMoveRaw?.Invoke(Position);
				OnCameraMove?.Invoke(pos);

			} else {

				OnCameraMoveRaw?.Invoke(transform.position);
				OnCameraMove?.Invoke(transform.position);

			}
		}

		private void DoTrack() {
			if (trackTarget != null) {
				Vector2 target = trackTarget.position;
				if (track != null) {
					target = track.Clamp(target);
				}
				Position = Vector2.SmoothDamp(Position, target, ref trackSmoothDampVel, smoothDampTime);
			}
		}

		private void DoAnimate() {
			animationTimer.Update();
			if (animationTimer.Done) {
				animator.enabled = false;
				Mode = CameraMode.Idle;
			}
		}

		private static Rect GetChoiceWorldViewport() {
			RectTransform rt = GameObject.FindWithTag("ChoiceWorldViewport").GetComponent<RectTransform>();
			return Rect.MinMaxRect(rt.anchorMin.x, rt.anchorMin.y, rt.anchorMax.x, rt.anchorMax.y);
		}

		#region Public Interface

		/// <summary>
		/// Reset camera to default settings.
		/// </summary>
		public void ResetProperties() {
			Mode = CameraMode.Idle;
			trackTarget = null;
			MainCamera.orthographicSize = defaultZoom;
			animator.enabled = false;
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

		/// <summary>
		/// Request the camera to switch to the idle state.
		/// </summary>
		public void RequestIdle() {
			Mode = CameraMode.Animation;
		}

		/// <summary>
		/// Request the camera to play an animation. Upon completion, camera will return to last state.
		/// </summary>
		public void RequestAnimation(AnimationClip clip) {
			Mode = CameraMode.Animation;
			animator.enabled = true;
			animator.Play(clip.name);
			animationTimer.Duration = clip.length;
			animationTimer.Start();
		}

		/// <summary>
		/// Request the camera to follow the given object.
		/// </summary>
		public void RequestTrackMode(Transform trackedObject) {
			animator.enabled = false;
			Mode = CameraMode.Track;
			trackTarget = trackedObject;
			Position = trackTarget.position;
		}

		/// <summary>
		/// Requests the camera return to whatever it was tracking last.
		/// </summary>
		public void RequestReturnToTrackMode() {
			Mode = CameraMode.Track;
		}

		/// <summary>
		/// Set the region of space the camera can freely pan inside.
		/// </summary>
		public void SetCameraTrack(CameraTrackObject track) {
			this.track = track;
		}

		/// <summary>
		/// Enter map zoom mode with specified parameters.
		/// </summary>
		public void RequestEnterMapZoom(float zoom, Vector2 center) {
			animator.enabled = false;
			Mode = CameraMode.Map;
			MainCamera.orthographicSize = zoom;
			Position = center;
		}

		/// <summary>
		/// Exit map zoom mode and return to previously tracked target.
		/// </summary>
		public void RequestExitMapZoom() {
			Mode = CameraMode.Track;
			MainCamera.orthographicSize = defaultZoom;
			Position = trackTarget.position;
		}

		/// <summary>
		/// Request the camera begin a screenshake with given magnitude and initial direction.
		/// </summary>
		public void RequestScreenShake(float magnitude, Vector2 direction) {
			if (Mode != CameraMode.Animation) {
				screenshake.Start(magnitude, (direction.sqrMagnitude == 0 ? Vector2.down : direction).normalized);
			}
		}

		/// <summary>
		/// Request the camera begin a screenshake with given magnitude and a random initial direction.
		/// </summary>
		public void RequestScreenShake(float magnitude) => RequestScreenShake(magnitude, UnityEngine.Random.insideUnitCircle);

		/// <summary>
		/// Quickly damp current screenshake to 0.
		/// </summary>
		public void StopScreenShake() => screenshake.Stop();

		#endregion

	}

}