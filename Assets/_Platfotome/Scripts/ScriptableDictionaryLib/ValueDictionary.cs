using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome.ScriptableDictInternal {

	[CreateAssetMenu(fileName = "ValueDict", menuName = "Scriptable Dictionaries/Value Dictionary", order = 101)]
	public class ValueDictionary : ScriptableDictionary<string> {

		[Serializable]
		public struct KVPair {
			public string key;
			public string value;
		}

		public List<KVPair> entries;

		protected override void Register() {
			ScriptableDictionaryLibrary.Register(dictionaryName, 
				GenerateDictionary(entries.Select(x => new KeyValuePair<string, string>(x.key, x.value))));
		}

	}

}