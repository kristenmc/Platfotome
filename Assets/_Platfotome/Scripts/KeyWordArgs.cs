using System;
using System.Collections.Generic;

namespace Platfotome {

	/// <summary>
	/// Represents a collection of strings mapping to objects. Similar to Python style **kwargs.
	/// </summary>
	public class KeyWordArgs : Dictionary<string, object> {

		public KeyWordArgs() { }

		public KeyWordArgs(params (string key, object value)[] args) : base(args.Length) {
			foreach (var (key, value) in args) {
				Add(key, value);
			}
		}

		public bool TryGetValue<T>(string key, out T value) {
			if (base.TryGetValue(key, out object o)) {
				try {
					value = (T)o;
					return true;
				} catch (InvalidCastException) {
				}
			}
			value = default;
			return false;
		}

		public override string ToString() => string.Format("KeyWordArgs({0})", this.ToCommaString(x => $"{x.Key} : {x.Value}"));

	}

}