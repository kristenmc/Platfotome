using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.ScriptableDictInternal {

	public abstract class ScriptableDictionary<TValue> : ScriptableObject {

		public string dictionaryName;

		protected ScriptableDictionary() {
			ScriptableDictionaryLibrary.OnLoad += Register;
		}

		protected abstract void Register();

		protected internal Dictionary<string, TValue> GenerateDictionary(IEnumerable<KeyValuePair<string, TValue>> enumerable) {
			var dictionary = new Dictionary<string, TValue>();
			foreach (var i in enumerable) {
				if (string.IsNullOrEmpty(i.Key)) continue;
				if (dictionary.ContainsKey(i.Key)) {
					Debug.LogWarning($"[ScriptableDictionary] Dictionary '{dictionaryName}' contains duplicate key '{i.Value}'. Ignoring key...");
				} else {
					dictionary.Add(i.Key, i.Value);
				}
			}
			return dictionary;
		}

	}

}
