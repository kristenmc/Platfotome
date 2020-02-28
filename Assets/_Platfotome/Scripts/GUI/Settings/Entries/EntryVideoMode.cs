using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome.GUI {

	public class EntryVideoMode : SettingEntry<bool> {

		public EntryVideoMode() :
			base(() => GameConfig.Current.fullscreen, x => GameConfig.Current.fullscreen = x) {
		}

		protected override string GetValueString(bool current) {
			return current ? "Fullscreen" : "Windowed";
		}

		protected override bool GetNext(bool current) {
			return !current;
		}

		protected override bool GetPrevious(bool current) {
			return !current;
		}

		protected override void OnEdit(bool current) {
			Debug.Log($"{GetType().Name} {current}");
		}
	}

}