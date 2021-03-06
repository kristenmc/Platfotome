﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	/// <summary>
	/// Central location for defining shared constants.
	/// </summary>
	public static class Constants {

		public static class Keys {

			public const string Pause = "Cancel";

			public const string AdvanceDialogue = "Jump";
			public const string SkipDialogue = "Fire1";

			public const string ChooseDialogueChoice = "Jump";

			public const string SkipCutscene = "Jump";

		}

		public static class Mask {

			public const int UIScroll = 1 << 25;

		}

		public static class Screenshake {

			public const float PlayerDeath = 0.3f;

		}

	}

}