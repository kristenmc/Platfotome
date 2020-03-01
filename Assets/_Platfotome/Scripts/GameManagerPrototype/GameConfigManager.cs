using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Platfotome {

    public class GameConfigManager : MonoBehaviour {

        private const string LogName = "[GameConfig]";

        public static GameConfigManager Instance { get; private set; }
        private static string SavePath => Path.Combine(Application.persistentDataPath, "settings.txt");

        private void Awake() {

            if (File.Exists(SavePath)) {
                try {
                    GameConfig.Current = JsonUtility.FromJson<GameConfig>(File.ReadAllText(SavePath));
                } catch (IOException) {
                    Debug.LogWarning(LogName + " Attempt to read file failed. Loading factory defaults.");
                    GameConfig.Current = GameConfig.GetDefault();
                }
            } else { 
                GameConfig.Current = GameConfig.GetDefault();
            }

            Instance = this;
        }

        public void Save() {
            try {
                File.WriteAllText(SavePath, JsonUtility.ToJson(GameConfig.Current));
            } catch (IOException) {
                Debug.LogWarning(LogName + " Attempt to write file failed. Settings not saved.");
            }
        }

    }

}