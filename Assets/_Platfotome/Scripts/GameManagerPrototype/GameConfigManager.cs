using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Platfotome {
    public class GameConfigManager : MonoBehaviour {
        public static GameConfigManager Instance { get; private set; }

        void Awake() {
            string dataPath = Path.Combine(Application.persistentDataPath, "settings.txt");

            if (File.Exists(dataPath)) {
                using (StreamReader reader = File.OpenText(dataPath)) {
                    GameConfig.Current = JsonUtility.FromJson<GameConfig>(reader.ReadToEnd());
                }
            } else { 
                GameConfig.Current = GameConfig.GetDefault();
            }

            Instance = this;
        }

        public void Save() {
            string dataPath = Path.Combine(Application.persistentDataPath, "settings.txt");

            using (StreamWriter writer = File.CreateText(dataPath)) {
                writer.Write(JsonUtility.ToJson(GameConfig.Current));
            }
        }
    }
}