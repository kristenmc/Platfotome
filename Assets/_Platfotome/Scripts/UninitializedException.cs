using System;

namespace Platfotome {

    /// <summary>
    /// Exception for when a tool is used before being initialized.
    /// </summary>
    [Serializable]
    public class UninitializedException : Exception {

        private const string Suggestion = " Are you not calling SystemsInitializer.Init()?";

        public UninitializedException() { }
        public UninitializedException(string message) : base(message + Suggestion) { }
        public UninitializedException(string message, Exception inner) : base(message + Suggestion, inner) { }
        protected UninitializedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}