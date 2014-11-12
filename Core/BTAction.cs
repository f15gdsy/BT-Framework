using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {
	public class BTAction : BTNode {
		private BTActionStatus status = BTActionStatus.Ready;
		
		public BTAction (BTPrecondition precondition = null) : base (precondition) {}
		
//		protected virtual void Enter () {Debug.Log("Enter "+name);}
//		protected virtual void Exit () {Debug.Log("Exit "+name);}
		protected virtual void Enter () {
			if (BTConfiguration.ENABLE_LOG) {
				Debug.Log("Enter " + this.GetType().ToString());
			}
		}
		protected virtual void Exit () {
			if (BTConfiguration.ENABLE_LOG) {
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
		
		
		private enum BTActionStatus {
			Ready = 1,
			Running = 2,
		}
	}
}