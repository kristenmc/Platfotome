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

	public class OverworldState : GameState {
		public OverworldState() :
			base("Overworld", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
		}
	}

	public class VisualNovelState : GameState {
		public VisualNovelState() :
			base("VisualNovel", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
		}
	}

	public class ChoiceWorldState : GameState {
		public ChoiceWorldState() :
			base("ChoiceWorld", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
		}
	}

}