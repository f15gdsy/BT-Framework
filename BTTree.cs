using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BT;

// How to use:
// 1. Initiate values in the database for the children to use.
// 2. Initiate BT _root
// 3. Some actions & preconditions that will be used later
// 4. Add children nodes
// 5. Activate the _root, including the children nodes' initialization

public abstract class BTTree : MonoBehaviour {
	protected BTNode _root = null;
	[HideInInspector]
	public Database database;

	[HideInInspector]
	public bool isRunning = true;

	protected int _resetDataId;

	void Awake () {
		Init();

		_root.Activate(database);
	}
	void Update () {
		if (!isRunning) return;
		
		if (database.GetData<bool>(Jargon.ShouldReset)) {
			Reset();	
			database.SetData<bool>(Jargon.ShouldReset, false);
		}
		if (_root.Evaluate()) {
			_root.Tick();	
		}
	}

	void OnDestroy () {
		if (_root != null) {
			_root.Clear();
		}
	}

	// Need to be called at the initialization code in the children.
	protected virtual void Init () {
		database = GetComponent<Database>();
		if (database == null) {
			database = gameObject.AddComponent<Database>();
		}

		_resetDataId = database.GetDataId(Jargon.ShouldReset);
		database.SetData<bool>(_resetDataId, false);
	}

	protected void Reset () {
		_root.Clear();	
	}
}
