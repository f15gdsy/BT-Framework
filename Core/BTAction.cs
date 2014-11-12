using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	/// <summary>
	/// BTAction is the base class for behavior node.
	/// 
	/// It cannot add / remove child.
	/// 
	/// Override the following to build a behavior (all are optional):
	/// - Enter
	/// - Execute
	/// - Exit
	/// - Clear
	/// </summary>
	public class BTAction : BTNode {
		private BTActionStatus status = BTActionStatus.Ready;
		
		public BTAction (BTPrecondition precondition = null) : base (precondition) {}


		protected virtual void Enter () {
			if (BTConfiguration.ENABLE_LOG) {	// For debug
				Debug.Log("Enter " + this.GetType().ToString());
			}
		}

		protected virtual void Exit () {
			if (BTConfiguration.ENABLE_LOG) {	// For debug
				Debug.Log("Exit " + this.GetType().ToString());
			}
		}

		protected virtual BTResult Execute () {
			return BTResult.Running;
		}
		
		public override void Clear () {
			if (status != BTActionStatus.Ready) {	// not cleared yet
				Exit();
				status = BTActionStatus.Ready;
			}
		}
		
		public override BTResult Tick () {
			BTResult result = BTResult.Ended;
			if (status == BTActionStatus.Ready) {
				Enter();
				status = BTActionStatus.Running;
			}
			if (status == BTActionStatus.Running) {		// not using else so that the status changes reflect instantly
				result = Execute();
				if (result != BTResult.Running) {
					Exit();
					status = BTActionStatus.Ready;
				}
			}
			return result;
		}

		public override void AddChild (BTNode aNode) {
			Debug.LogError("BTAction: Cannot add a node into BTAction.");
		}

		public override void RemoveChild (BTNode aNode) {
			Debug.LogError("BTAction: Cannot remove a node into BTAction.");
		}
		
		
		private enum BTActionStatus {
			Ready = 1,
			Running = 2,
		}
	}
}