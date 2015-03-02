using UnityEngine;
using System.Collections;
using BT;

namespace BT.Ex {

	/// <summary>
	/// Reset the data in database to 
	/// 	1. default data, passed in through constructor, or
	/// 	2. another data in database, whose name is passed in through constructor.
	/// </summary>
	public class ResetData<T> : BTAction {
		private string _dataToReset;
		private int _dataIdToReset;

		private T _defaultData;

		private string _targetData;
		private int _targetDataId;

		// Indicate if the data should be reset when this node is cleared 
		// Clear is called (when it ends, or when its parent ends).
		private bool _shouldClear;
		
		
		public ResetData (string dataToReset, T defaultData, bool shouldClear = false, BTPrecondition precondition = null) : base (precondition) {
			_dataToReset = dataToReset;
			_defaultData = defaultData;
			_shouldClear = shouldClear;
		}

		public ResetData (string dataToReset, string targetData, bool shouldClear = false, BTPrecondition precondition = null) : base (precondition) {
			_dataToReset = dataToReset;
			_targetData = targetData;
			_shouldClear = shouldClear;
		}

		public override void Activate (Database database) {
			base.Activate (database);

			_dataIdToReset = database.GetDataId(_dataToReset);
			_targetDataId = _targetData == null ? -1 : database.GetDataId(_targetData);
		}
		
		protected override BTResult Execute () {
			if (_targetData == null) {
				database.SetData<T>(_dataIdToReset, _defaultData);
			}
			else {
				T data = database.GetData<T>(_targetDataId);
				database.SetData<T>(_dataToReset, data);
			}

			return BTResult.Ended;
		}

		public override void Clear () {
			base.Clear ();
			if (_shouldClear) {
				Execute();
			}
		}
	}


	public class ResetDataWhenClear<T> : BTAction {
		private string _dataToReset;
		private int _dataIdToReset;
		
		private T _defaultData;
		
		private string _targetData;
		private int _targetDataId;


		public ResetDataWhenClear (string dataToReset, T defaultData, BTPrecondition precondition = null) : base (precondition) {
			_dataToReset = dataToReset;
			_defaultData = defaultData;
		}
		
		public ResetDataWhenClear (string dataToReset, string targetData, BTPrecondition precondition = null) : base (precondition) {
			_dataToReset = dataToReset;
			_targetData = targetData;
		}

		public override void Activate (Database database) {
			base.Activate (database);
			
			_dataIdToReset = database.GetDataId(_dataToReset);
			_targetDataId = _targetData == null ? -1 : database.GetDataId(_targetData);
		}

		public override void Clear () {
			base.Clear ();

			if (_targetData == null) {
				database.SetData<T>(_dataIdToReset, _defaultData);
			}
			else {
				T data = database.GetData<T>(_targetDataId);
				database.SetData<T>(_dataToReset, data);
			}
		}
	}

}