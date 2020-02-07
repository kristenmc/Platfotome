using System;

namespace Platfotome {

	/// <summary>
	/// Configure how long each part of a transition lasts.
	/// </summary>
	[Serializable]
	public struct TransitionArguments {

		public float InTime;
		public float HoldTime;
		public float OutTime;

		/// <summary>
		/// Configure transition time. Transition will hold a minimum of holdTime, waiting until scene is loaded before continuing.
		/// </summary>
		public TransitionArguments(float inTime, float holdTime, float outTime) {
			InTime = inTime;
			HoldTime = holdTime;
			OutTime = outTime;
		}

		public override string ToString() => $"TransitionArguments({InTime}, {HoldTime}, {OutTime})";

	}

}