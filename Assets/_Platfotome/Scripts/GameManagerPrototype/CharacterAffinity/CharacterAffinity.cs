using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome {

    public abstract class CharacterAffinity<T> where T: Enum {

        public T CurrentState { get; protected set; }
        protected readonly Dictionary<T, T[]> graph = new Dictionary<T, T[]>();
        public string Name { get; }

        protected CharacterAffinity(string name) {
            Name = name;
        }

        public void RequestTransition(T newState) {
            Debug.Log($"[CharacterAffinity] State transition from {CurrentState} to {newState}");
            T[] edges = graph[CurrentState];
            if (edges.Contains(newState)) {
                CurrentState = newState;
            } else {
                Debug.LogError($"[CharacterAffinity] Invalid state request from {CurrentState} to {newState}. Request denied.");
            }
        }

        protected void AddEdge(T from, params T[] to) => graph.Add(from, to);

        public string GetDialogueKey() => $"{Name}_{CurrentState}";

        public override string ToString() {
            return $"CharacterAffinity('{Name}', {CurrentState})";
        }

#if UNITY_EDITOR
        public void _Editor_SetCurrentState(Enum state) => CurrentState = (T)state;
#endif

    }

}