using UnityEngine;
using System.Collections;
using BT;

public class ResetData<T> : BTAction {
	private string _dataToReset;
	private T _defaultData;
	private string _targetData;
	private bool _shouldClear;
	
	
	public ResetData (string dataToReset, T defaultData, bool shouldClear = false) : base (null) {
		_dataToReset = dataToReset;
		_defaultData = defaultData;
		_targetData = Jargon.Invalid;
		_shouldClear = shouldClear;
	}

	public ResetData (string dataToReset, string targetData, bool shouldClear = false) : base (null) {
		_dataToReset = dataToReset;
		_targetData = targetData;
		_shouldClear = shouldClear;
	}
	
	protected override BTResult Execute () {
		if (_targetData.Equals(Jargon.Invalid)) {
			database.SetData<T>(_dataToReset, _defaultData);
		}
		else {
			T data = database.GetData<T>(_targetData);
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