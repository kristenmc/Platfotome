using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platfotome.GUI {

	/// <summary>
	/// Base class for an editable setting entry.
	/// </summary>
	/// <typeparam name="T">Type of the entry</typeparam>
	public abstract class SettingEntry<T> : MonoBehaviour, IEditableSetting {

		private TextMeshEditField left, right;

        /// <summary>
        /// Whether a setting value should be continuously updated.
        /// </summary>
        [SerializeField] protected bool continuousUpdate = false;

		/// <summary>
		/// The value of this entry.
		/// </summary>
		protected T EntryValue {
			get => readBinding();
			set {
				writeBinding(value);
				RightText = GetValueString(value);
				OnEdit(value);
                GameConfigManager.Instance.Save();
			}
		}

		/// <summary>
		/// The base text of the textmesh on the right.
		/// </summary>
		protected string RightText {
			get => right.BaseText;
			set => right.BaseText = value;
		}

		/// <summary>
		/// The base text of the textmesh on the left.
		/// </summary>
		protected string LeftText {
			get => left.BaseText;
			set => left.BaseText = value;
		}

		private readonly Func<T> readBinding;
		private readonly Action<T> writeBinding;

		protected SettingEntry(Func<T> readBinding, Action<T> writeBinding) {
			this.readBinding = readBinding;
			this.writeBinding = writeBinding;
		}

		private void Awake() {
			var editFields = GetComponentsInChildren<TextMeshEditField>();
			left = editFields[0];
			right = editFields[1];
		}

		private void OnEnable() {
			EntryValue = readBinding();
		}

		/// <summary>
		/// Get the string representation of the current value.
		/// </summary>
		protected abstract string GetValueString(T current);

		/// <summary>
		/// What action to perform once the entry value is changed.
		/// </summary>
		/// <param name="current"></param>
		protected abstract void OnEdit(T current);

		/// <summary>
		/// Set the current entry value to the next value.
		/// </summary>
		public void SetNext() => EntryValue = GetNext(EntryValue);

		/// <summary>
		/// Set the current entry value to the previous value.
		/// </summary>
		public void SetPrevious() => EntryValue = GetPrevious(EntryValue);

		/// <summary>
		/// Returns the next value in sequence.
		/// </summary>
		protected abstract T GetNext(T current);

		/// <summary>
		/// Returns the previous value in sequence.
		/// </summary>
		protected abstract T GetPrevious(T current);

		public void Select() {
			left.Activated = right.Activated = true;
		}

		public void Deselect() {
			left.Activated = right.Activated = false;
		}

		public override string ToString() => $"{GetType().Name}<{typeof(T).Name}>({EntryValue})";

        void Update() {
            if (continuousUpdate) {
                RightText = GetValueString(readBinding());
            }
        }
	}

}