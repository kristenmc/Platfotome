using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome {

	/// <summary>
	/// Central retrieval location for dictionaries represented as scriptable objects in the editor.
	/// </summary>
	public static class ScriptableDictionaryLibrary {

		private static readonly Dictionary<string, Dictionary<string, UnityEngine.Object>> objectDicts = new Dictionary<string, Dictionary<string, UnityEngine.Object>>();
		private static readonly Dictionary<string, Dictionary<string, string>> valueDicts = new Dictionary<string, Dictionary<string, string>>();

		internal static event Action OnLoad;

		private static bool loaded = false;

		private const string Indent = "    ";

		/// <summary>
		/// Call this function exactly once during init to load dictionaries.
		/// </summary>
		public static void LoadAllDictionaries(bool showOuput = false) {
			if (loaded) {
				Debug.LogWarning("[ScriptableDictionary] Attempted to load library twice.");
				return;
			}
			OnLoad?.Invoke();

			if (showOuput) {

				Debug.Log("<b>[ScriptableDictionary] Start printout.</b>");

				Debug.Log(Indent + "Start object dictionaries");
				foreach (var item in objectDicts) {
					Debug.Log(Indent + Indent + $"{item.Key} : {item.Value.ToCommaString()}");
				}
				if (objectDicts.Count == 0) Debug.Log(Indent + Indent + "(none)");

				Debug.Log(Indent + "Start value dictionaries");
				foreach (var item in valueDicts) {
					Debug.Log(Indent + Indent + $"{item.Key} : {item.Value.ToCommaString()}");
				}
				if (valueDicts.Count == 0) Debug.Log(Indent + Indent + "(none)");

				Debug.Log(Indent + $"Loaded {objectDicts.Count} object dicts and {valueDicts.Count} value dicts.");

				Debug.Log("<b>[ScriptableDictionary] End printout.</b>");

			}

			loaded = true;
		}

		internal static void Register(string name, Dictionary<string, string> d) {
			try {
				valueDicts.Add(name, d);
			} catch (ArgumentException) {
				Debug.LogWarning($"[ScriptableDictionary] Attempted to add duplicate value dictionary '{name}'. Ignoring duplicate.");
			}
		}

		internal static void Register(string name, Dictionary<string, UnityEngine.Object> d) {
			try {
				objectDicts.Add(name, d);
			} catch (ArgumentException) {
				Debug.LogWarning($"[ScriptableDictionary] Attempted to add duplicate object dictionary '{name}'. Ignoring duplicate.");
			}
		}

		/// <summary>
		/// Retrieve a object dictionary by name.
		/// </summary>
		/// <exception cref="KeyNotFoundException" />
		public static Dictionary<string, UnityEngine.Object> GetObjectDictionary(string name) {
			if (!CheckLoaded()) throw new ArgumentNullException("Object dictionaries have not been initialized");
			return objectDicts[name];
		}

		/// <summary>
		/// Retrieve a value dictionary by name.
		/// </summary>
		/// <exception cref="KeyNotFoundException" />
		public static Dictionary<string, string> GetValueDictionary(string name) {
			if (!CheckLoaded()) throw new ArgumentNullException("Object dictionaries have not been initialized");
			return valueDicts[name];
		}

		/// <summary>
		/// Try to cast a object dictionary to a more specific type. Returns true if successful.
		/// </summary>
		/// <typeparam name="T">Type of dictionary values</typeparam>
		/// <param name="name">Unique name of object dictionary</param>
		/// <param name="dict">Output dictionary of new type</param>
		public static bool TryCastObjectDictionary<T>(string name, out Dictionary<string, T> dict) where T : UnityEngine.Object {
			dict = null;
			if (!CheckLoaded()) return false;
			if (objectDicts.TryGetValue(name, out var d)) {
				try {
					dict = d.Select(x => new KeyValuePair<string, T>(x.Key, (T)x.Value)).ToDictionary(x => x.Key, x => x.Value);
				} catch (InvalidCastException) {
					return false;
				}
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to cast a value dictionary to a more specific type. Returns true if successful.
		/// </summary>
		/// <typeparam name="T">Type of dictionary values</typeparam>
		/// <param name="name">Unique name of value dictionary</param>
		/// <param name="dict">Output dictionary of new type</param>
		/// <param name="converter">Function called to transform string into more specific type</param>
		public static bool TryCastValueDictionary<T>(string name, out Dictionary<string, T> dict, Func<string, T> converter) {
			dict = null;
			if (!CheckLoaded()) return false;
			if (valueDicts.TryGetValue(name, out var d)) {
				try {
					dict = d.Select(x => new KeyValuePair<string, T>(x.Key, converter(x.Value))).ToDictionary(x => x.Key, x => x.Value);
				} catch (Exception) {
					return false;
				}
				return true;
			} else {
				return false;
			}
		}

		private static bool CheckLoaded() {
			if (!loaded) Debug.LogError("[ScriptableDictionary] Cannot retrieve dictionary until LoadAllDictionaries() has been called");
			return loaded;
		}

	}

}
