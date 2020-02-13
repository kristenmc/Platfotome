using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platfotome {
    public abstract class CharacterAffinity<T> where T: Enum {
        public T CurrentStatus { get; protected set; }
        protected readonly Dictionary<T, T[]> graph = new Dictionary<T, T[]>();
        public string Name { get; }

        protected CharacterAffinity(string name) {
            Name = name;
        }

        public void RequestTransition(T newState) {
            Debug.Log($"[CharacterAffinity] State transition from {CurrentStatus} to {newState}");
            T[] edges = graph[CurrentStatus];
            if (edges.Contains(newState)) {
                CurrentStatus = newState;
            } else {
                Debug.LogError("[CharacterAffinity] Invalid state transition");
            }
        }

        protected void AddEdge(T from, params T[] to) => graph.Add(from, to);

        public string GetDialogueKey() => $"{Name}_{CurrentStatus}";
    }
}