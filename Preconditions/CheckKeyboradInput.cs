using UnityEngine;
using System.Collections;
using BT;

public class CheckKeyboradInput : BTPrecondition {

	private KeyCode _targetKey;
	private KeyState _keyState;


	public CheckKeyboradInput (KeyCode targetKey, KeyState keyState) {
		_targetKey = targetKey;
		_keyState = keyState;
	}

	public override bool Check () {
		bool result = false;

		if (_keyState.keyDown) {
			result = Input.GetKeyDown(_targetKey);
		}
		if (_keyState.keyUp && !result) {
			result = Input.GetKeyUp(_targetKey);
		}
		if (_keyState.keyHold && !result) {
			result = Input.GetKey(_targetKey);
		}
		return result;
	}
}

public struct KeyState {
	public bool keyDown;
	public bool keyUp;
	public bool keyHold;

	public KeyState (bool keyDown, bool keyUp, bool keyHold) {
		this.keyDown = keyDown;
		this.keyUp = keyUp;
		this.keyHold = keyHold;
	}
}
