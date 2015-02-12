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

	public const string RESET = "Rest";
	private static int _resetId;


	void Awake () {
		Init();

		_root.Activate(database);
	}
	void Update () {
		if (!isRunning) return;
		
		if (database.GetData<bool>(RESET)) {
			Reset();	
			database.SetData<bool>(RESET, false);
		}

		// Iterate the BT tree now!
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

		_resetId = database.GetDataId(RESET);
		database.SetData<bool>(_resetId, false);
	}

	protected void Reset () {
		if (_root != null) {
			_root.Clear();	
		}
	}
}
