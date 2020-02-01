using UnityEngine;

namespace Platfotome {

    public class GameManagerTmp : MonoBehaviour {

        private void Awake() {

            ScriptableDictionaryLibrary.LoadAllDictionaries(showOuput: false);

        }

    }

}