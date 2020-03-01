using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class EntryVideoScreenshake : SettingEntry<float> {

		public EntryVideoScreenshake() :
			base(() => GameConfig.Current.screenshake, x => GameConfig.Current.screenshake = x) {
		}

		protected override float GetNext(float current) {
			return Mathf.Min(current + 0.5f, 1f);
		}

		protected override float GetPrevious(float current) {
			return Mathf.Max(current - 0.5f, 0);
		}

		protected override string GetValueString(float current) {
            return $"{100 * current}%";
		}

		protected override void OnEdit(float current) {
			Debug.Log($"{GetType().Name} {current}");
		}
	}

}