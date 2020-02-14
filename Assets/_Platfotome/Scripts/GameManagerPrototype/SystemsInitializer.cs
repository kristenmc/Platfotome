using Platfotome.GUI;

namespace Platfotome {

	/// <summary>
	/// Central location to initialize systems. Initialize is automatically called by GameManager before game begins.
	/// <para>If working directly in a test scene, call Initialize() from a temporary GameManager script to ensure proper initialization.</para>
	/// </summary>
	public static class SystemsInitializer {

		private static bool initialized = false;

		public static void Initialize() {
			if (initialized) return;

			ScriptableDictionaryLibrary.Initialize();
			DialogueManager.Initialize(true);
			TransitionLibrary.Initialize();

			initialized = true;
		}

	}

}