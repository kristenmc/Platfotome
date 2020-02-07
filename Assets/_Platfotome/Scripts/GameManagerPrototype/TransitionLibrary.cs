using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome {

	public static class TransitionLibrary {

		private static Dictionary<Type, Transition> mapping;

		public static void Initialize(bool showOutput = false) {

			Transition[] transitions = Resources.LoadAll<Transition>("Transitions/");

			mapping = transitions.ToDictionary(k => k.GetType(), v => v);

			if (showOutput) {
				Debug.Log(mapping.ToHeaderedDict("[TransitionLibrary] AllTransitions"));
			}
		}

		public static Transition GetTransition(Type type) {
			try {
				return mapping[type];
			} catch (NullReferenceException) {
				throw new UninitializedException("[TransitionLibrary] Failed to retrieve transition.");
			}
		}

	}

}