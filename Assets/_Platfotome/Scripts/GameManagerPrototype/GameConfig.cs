using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	/// <summary>
	/// Stores all persistent game configuration data.
	/// </summary>
	[Serializable]
	public sealed class GameConfig {

		public const int MaxScale = 6;
		public const int MinScale = 2;

		/// <summary>
		/// Return the currently active game config.
		/// </summary>
		public static GameConfig Current { get; set; }

		private GameConfig() { }

		/// <summary>
		/// Create a new GameConfig with default configuration values.
		/// </summary>
		/// <returns></returns>
		public static GameConfig GetDefault() {
			return new GameConfig() {
				fullscreen = true,
				scale = 6
			};
		}

		#region Fields

		public bool fullscreen;
		public float scale;

		#endregion

	}

}