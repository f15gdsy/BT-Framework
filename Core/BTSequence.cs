using UnityEngine;
using System.Collections;

namespace BT {

	/// <summary>
	/// BTSequence evaluteas the current active child, or the first child (if no active child).
	/// 
	/// If passed the evaluation, BTSequence ticks the current active child, or the first child (if no active child available),
	/// and if it's result is BTEnded, then change the active child to the next one.
	/// 
	/// </summary>
	public class BTSequence : BTNode {
	
		private BTNode activeChild;
		private int activeIndex = -1;
		
		
		public BTSequence (BTPrecondition precondition = null) : base (precondition) {}
		
		protected override bool DoEvaluate () {
			if (activeChild != null) {
				bool result = activeChild.Evaluate();
				if (!result) {
					activeChild.Clear();
					activeChild = null;
					activeIndex = -1;
				}
				return result;
			}
			else {
				return children[0].Evaluate();
			}
		}
		
		public override BTResult Tick () {
			// first time
			if (activeChild == null) {
				activeChild = children[0];
				activeIndex = 0;
			}

			BTResult result = activeChild.Tick();
			if (result == BTResult.Ended) {	// Current active node over
				activeIndex++;
				if (activeIndex >= children.Count) {	// sequence is over
					activeChild.Clear();
					activeChild = null;
					activeIndex = -1;
				}
				else {	// next node
					activeChild.Clear();
					activeChild = children[activeIndex];
					result = BTResult.Running;
				}
			}
			return result;
		}
		
		public override void Clear () {
			if (activeChild != null) {
				activeChild = null;
				activeIndex = -1;
			}

			foreach (BTNode child in children) {
				child.Clear();
			}
		}
	}

}