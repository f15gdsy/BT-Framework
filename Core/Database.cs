using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Database is the blackboard in a classic blackboard system. 
/// (I found the name "blackboard" a bit hard to understand so I call it database ;p)
/// 
/// It is the place to store data from local nodes, cross-tree nodes, and even other scripts.

/// Nodes can read the data inside a database by the use of a string, or an int id of the data.
/// The latter one is prefered for efficiency's sake.
/// </summary>
public class Database : MonoBehaviour {

	// _database & _dataNames are 1 to 1 relationship
	private List<object> _database = new List<object>();
	private List<string> _dataNames = new List<string>();
	

	// Should use dataId as parameter to get data instead of this
	public T GetData<T> (string dataName) {
		int dataId = IndexOfDataId(dataName);
		if (dataId == -1) Debug.LogError("Database: Data for " + dataName + " does not exist!");

		return (T) _database[dataId];
	}

	// Should use this function to get data!
	public T GetData<T> (int dataId) {
		if (BT.BTConfiguration.ENABLE_DATABASE_LOG) {
			Debug.Log("Database: getting data for " + _dataNames[dataId]);
		}
		return (T) _database[dataId];
	}
	
	public void SetData<T> (string dataName, T data) {
		int dataId = GetDataId(dataName);
		_database[dataId] = (object) data;
	}

	public void SetData<T> (int dataId, T data) {
		_database[dataId] = (object) data;
	}

	public int GetDataId (string dataName) {
		int dataId = IndexOfDataId(dataName);
		if (dataId == -1) {
			_dataNames.Add(dataName);
			_database.Add(null);
			dataId = _dataNames.Count - 1;
		}

		return dataId;
	}

	private int IndexOfDataId (string dataName) {
		for (int i=0; i<_dataNames.Count; i++) {
			if (_dataNames[i].Equals(dataName)) return i;
		}

		return -1;
	}

	public bool ContainsData (string dataName) {
		return IndexOfDataId(dataName) != -1;
	}
}


// IMPORTANT: users may want to put Jargon in a separate file
//public enum Jargon {
//	ShouldReset = 1,
//}



