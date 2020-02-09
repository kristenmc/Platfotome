using System.Collections.Generic;

namespace Platfotome {

	/// <summary>
	/// Represents a collection of strings mapping to objects. Similar to Python style **kwargs.
	/// </summary>
	public class KeyWordArgs : Dictionary<string, object> {
		public KeyWordArgs(params (string key, object value)[] args) : base(args.Length) {
			foreach (var (key, value) in args) {
				Add(key, value);
			}
		}
	}

}