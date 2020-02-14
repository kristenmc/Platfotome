using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	[CreateAssetMenu(fileName = "New Dialogue Choice", menuName = "GUI Assets/Dialogue Choice", order = 112)]
	public class DialogueChoice : ScriptableObject {

		[Serializable]
		public class Choice {
			public string text = null;
			public MetaLoadEntry loadEntry = null;
		}

		public CharacterStyleGroup style = null;
		public Choice[] choices = null;

		public Choice this[int index] => choices[index];
		public int Count => choices.Length;

		public override string ToString() => $"DialogueChoice('{name}', style '{style.name}', length {choices.Length})";

	}

}