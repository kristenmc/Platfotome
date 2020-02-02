using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	[CreateAssetMenu(fileName = "New Style", menuName = "GUI Assets/Style", order = 111)]
	internal class CharacterStyleGroup : ScriptableObject {

		public CharacterStyleGroup baseStyle = null;
		public GameObject nameplate = null;
		public GameObject frame = null;
		public GameObject portrait = null;
		public GameObject background = null;

		public void Resolve(out GameObject nameplate, out GameObject frame, out GameObject portrait, out GameObject background) {
			nameplate = RetrieveRecursive(this, x => x.nameplate);
			frame = RetrieveRecursive(this, x => x.frame);
			portrait = RetrieveRecursive(this, x => x.portrait);
			background = RetrieveRecursive(this, x => x.background);
		}

		private GameObject RetrieveRecursive(CharacterStyleGroup start, Func<CharacterStyleGroup, GameObject> retrieve) {
			for (CharacterStyleGroup cur = start; cur != null; cur = cur.baseStyle) {
				if (retrieve(cur) != null) {
					return retrieve(cur);
				}
			}
			return null;
		}

	}

}