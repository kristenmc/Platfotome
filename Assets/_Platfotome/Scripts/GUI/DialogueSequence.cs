using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "GUI Assets/Dialogue Sequence", order = 110)]
	internal class DialogueSequence : ScriptableObject {

		public CharacterStyleGroup style = null;
		public string[] text = null;

		public override string ToString() => $"DialogueSequence ('{name}', style '{style.name}', length {text.Length})";

	}

}