using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome.GUI {

	public interface IEditableSetting: ISelectableUIElement {

		void SetNext();
		void SetPrevious();

	}

}