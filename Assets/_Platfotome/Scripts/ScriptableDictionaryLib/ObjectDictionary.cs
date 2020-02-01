using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome.ScriptableDictInternal {

	[CreateAssetMenu(fileName = "ObjectDict", menuName = "Scriptable Dictionaries/Object Dictionary", order = 100)]
	public class ObjectDictionary : ScriptableDictionary<UnityEngine.Object> {

		[Serializable]
		public struct KVPair {
			public string key;
			public UnityEngine.Object value;
		}

		public List<KVPair> entries;

		protected override void Register() {
			ScriptableDictionaryLibrary.Register(dictionaryName, 
				GenerateDictionary(entries.Select(x => new KeyValuePair<string, UnityEngine.Object>(x.key, x.value))));
		}

	}

}