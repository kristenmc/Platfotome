using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class EntryVideoScreenshake : SettingEntry<int> {

		public EntryVideoScreenshake() :
			base(() => GameConfig.Current.screenshake, x => GameConfig.Current.screenshake = x) {
		}

		protected override int GetNext(int current) {
			return Mathf.Min(current + 50, 100);
		}

		protected override int GetPrevious(int current) {
			return Mathf.Max(current - 50, 0);
		}

		protected override string GetValueString(int current) {
            return $"{current}%";
		}

		protected override void OnEdit(int current) {
			Debug.Log($"{GetType().Name} {current}");
		}
	}

}