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
        public static readonly Vector2 baseResolution = new Vector2(320, 180);

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
                scale = 6,
                screenshake = 100,

                volumeMaster = 10,
                volumeMusic = 10,
                volumeSound = 10
			};
		}

		#region Fields

		public bool fullscreen;
		public float scale;
        public int screenshake;

        public int volumeMaster;
        public int volumeMusic;
        public int volumeSound;

        #endregion

    }

}