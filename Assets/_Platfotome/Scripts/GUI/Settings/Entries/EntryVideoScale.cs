using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public class EntryVideoScale : SettingEntry<float> {

		public EntryVideoScale() :
			base(() => Mathf.Min(Screen.width / GameConfig.BaseResolution.x, Screen.height / GameConfig.BaseResolution.y), x => GameConfig.Current.scale = x) {
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
            Screen.SetResolution(Mathf.RoundToInt(GameConfig.BaseResolution.x * current), Mathf.RoundToInt(GameConfig.BaseResolution.y * current), GameConfig.Current.fullscreen);
			Debug.Log($"{GetType().Name} {current}");
		}
	}

}