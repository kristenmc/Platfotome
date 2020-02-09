using UnityEngine.SceneManagement;

namespace Platfotome {

	public class OpeningTitlesState : GameState {
		public OpeningTitlesState() :
			base("OpeningTitles", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0f, 0.2f, 1.2f)) {
			AllowPause = false;
		}
	}

	public class MainMenuState : GameState {
		public MainMenuState() :
			base("MainMenu", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.3f, 0.2f, 0.3f)) {
			AllowPause = false;
		}
	}

	public class OverworldState : GameState {
		public OverworldState() :
			base("Overworld", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
		}
	}

	public class VisualNovelState : GameState {
		public const string DialogueKey = "dialogue_key";
		public VisualNovelState(string dialogueKey) :
			base("VisualNovel", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
			Args.Add(DialogueKey, dialogueKey);
		}
	}

	public class ChoiceWorldState : GameState {
		public const string LevelKey = "level_key";
		public const string QuestionKey = "question_key";
		public ChoiceWorldState(string levelKey, string question) :
			base("ChoiceWorld", TransitionLibrary.GetTransition(typeof(FadeToBlack)), new TransitionArguments(0.2f, 0.1f, 0.2f)) {
			Args.Add(LevelKey, levelKey);
			Args.Add(QuestionKey, question);
		}
	}

}