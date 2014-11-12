using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	/// <summary>
	/// BTPrioritySelector selects the first sussessfully evaluated child as the active child.
	/// </summary>
	public class BTPrioritySelector : BTNode {
		
		private BTNode activeChild;

		public BTPrioritySelector (BTPrecondition precondition = null) : base (precondition) {}

		// selects the active child
		protected override bool DoEvaluate () {
			foreach (BTNode child in children) {
				if (child.Evaluate()) {
					if (activeChild != null && activeChild != child) {
						activeChild.Clear();	
					}
					activeChild = child;
					return true;
				}
			}
			return false;
		}
		
		public override void Clear () {
			if (activeChild != null) {
				activeChild.Clear();
				activeChild = null;
			}
		}
		
		public override BTResult Tick () {
			BTResult result = activeChild.Tick();
			if (result != BTResult.Running) {
				activeChild.Clear();
			}
			return result;
		}
	}
}