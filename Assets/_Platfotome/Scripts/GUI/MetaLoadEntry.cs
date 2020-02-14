using System;

namespace Platfotome.GUI {

	[Serializable]
	public class MetaLoadEntry {

		public MetaLoadType type;
		public DialogueSequence dialogue;
		public DialogueChoice choice;
		public UnityEngine.Object level;

		public override string ToString() {

			string typeStr;
			switch (type) {
				case MetaLoadType.None:
					typeStr = "None";
					break;
				case MetaLoadType.Dialogue:
					typeStr = dialogue.name;
					break;
				case MetaLoadType.Choice:
					typeStr = choice.name;
					break;
				case MetaLoadType.Level:
					typeStr = level.name;
					break;
				default:
					typeStr = "InvalidType";
					break;
			}

			return $"MetaLoadEntry({type} -> {typeStr})";
		}

	}

}