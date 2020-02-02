using Platfotome.GUI;
using UnityEngine;

namespace Platfotome {

    public class GameManagerTmp : MonoBehaviour {

        private void Awake() {

            ScriptableDictionaryLibrary.LoadAllDictionaries(showOutput: false);
            DialogueManager.Init(showOutput: true);

        }

    }

}