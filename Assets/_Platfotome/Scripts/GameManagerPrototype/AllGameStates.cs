using UnityEngine.SceneManagement;

namespace Platfotome {

	public class OpeningTitlesState : GameState {
		public OpeningTitlesState() :
			base("OpeningTitles", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0f, 0.2f, 1.2f)) {
		}
	}

	public class MainMenuState : GameState {
		public MainMenuState() :
			base("MainMenu", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.3f, 0.2f, 0.3f)) {
		}
	}

}