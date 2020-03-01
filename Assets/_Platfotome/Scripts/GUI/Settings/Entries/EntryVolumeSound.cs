using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {
    public class EntryVolumeSound : SettingEntry<int> {
        public EntryVolumeSound() :
            base(() => GameConfig.Current.volumeSound, x => GameConfig.Current.volumeSound = x) {
        }

        protected override int GetNext(int current) {
            return Mathf.Min(current + 1, 10);
        }

        protected override int GetPrevious(int current) {
            return Mathf.Max(current - 1, 0);
        }

        protected override string GetValueString(int current) {
            return current.ToString();
        }

        protected override void OnEdit(int current) {
            Debug.Log($"{GetType().Name} {current}");
        }
    }
}