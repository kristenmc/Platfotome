using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class HeManAffinity : CharacterAffinity<HeManAffinity.State> {
        public enum State {
            Start, Happy, Angry, End, Reconcile, Sad
        }

        public HeManAffinity() :
            base("HeMan") {
            AddEdge(State.Start, State.Happy, State.Angry);

            graph.Add(State.Angry, new State[] { State.Sad });
            graph.Add(State.Sad, new State[] { State.Reconcile });
            graph.Add(State.Reconcile, new State[] { State.Happy });
            graph.Add(State.Happy, new State[] { State.End });
            graph.Add(State.End, new State[0]);
        }
    }
}