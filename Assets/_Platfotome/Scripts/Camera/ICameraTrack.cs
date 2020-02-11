using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {

	public interface ICameraTrack {

		Vector2 Clamp(Vector2 position);

	}

}