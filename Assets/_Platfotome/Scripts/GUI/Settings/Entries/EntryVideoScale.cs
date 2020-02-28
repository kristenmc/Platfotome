using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class EntryVideoScale : SettingEntry<float> {

		public EntryVideoScale() :
			base(() => GameConfig.Current.scale, x => GameConfig.Current.scale = x) {
		}

		protected override float GetNext(float current) {
			return Mathf.Min(Mathf.Floor(current) + 1, GameConfig.MaxScale);
		}

		protected override float GetPrevious(float current) {
			return Mathf.Max(Mathf.Ceil(current) - 1, GameConfig.MinScale);
		}

		protected override string GetValueString(float current) {
			if (Mathf.Approximately(current, Mathf.Round(current))) {
				return $"x{current:0}";
			} else {
				return $"x{current:0.00}";
			}
		}

		protected override void OnEdit(float current) {
			Debug.Log($"{GetType().Name} {current}");
		}
	}

}