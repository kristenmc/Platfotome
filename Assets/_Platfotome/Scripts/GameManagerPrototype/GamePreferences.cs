using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Platfotome {

	/// <summary>
	/// Central location for managing game preferences. Sets factory defaults, contains PlayerPref keys, and raises OnApply event.
	/// </summary>
	public static class GamePreferences {

		/// <summary>
		/// A list of every key in PlayerPrefs.
		/// </summary>
		public static class Key {
			public const string Screenshake = "ScreenshakeFactor";
		}

		/// <summary>
		/// Raised when systems should read and apply new configuration data from PlayerPrefs.
		/// </summary>
		public static event Action OnApply;

		/// <summary>
		/// Set all preferences to factory defaults. Does NOT raise OnApply event.
		/// </summary>
		public static void ResetToFactory() {
			PlayerPrefs.SetFloat(Key.Screenshake, 1f);
		}

		/// <summary>
		/// Apply current settings in PlayerPrefs
		/// </summary>
		public static void Apply() {
			OnApply?.Invoke();
		}

		public static string ToStringAll() {
			StringBuilder sb = new StringBuilder("Game Preferences\n");

			void Write(string name, object value) => sb.AppendLine($"    {name}: {value}");

			Write(Key.Screenshake, PlayerPrefs.GetFloat(Key.Screenshake));

			return sb.ToString();

		}

	}

}