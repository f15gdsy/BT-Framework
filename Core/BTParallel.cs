using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BT;

namespace BT {

	/// <summary>
	/// BTParallel evaluates all children, if any of them fails the evaluation, BTParallel fails.
	/// 
	/// BTParallel ticks all children, if 
	/// 	1. ParallelFunction.And: 	ends when all children ends
	/// 	2. ParallelFunction.Or: 	ends when any of the children ends
	/// </summary>
	public class BTParallel : BTNode {
		protected List<BTResult> results;
		protected ParallelFunction func;
		
		public BTParallel (ParallelFunction func) : this (func, null) {}

		public BTParallel (ParallelFunction func, BTPrecondition precondition) : base (precondition) {
			results = new List<BTResult>();
			this.func = func;
		}

		protected override bool DoEvaluate () {
			foreach (BTNode child in children) {
				if (!child.Evaluate()) {
					return false;	
				}
			}
			return true;
		}
		
		public override BTResult Tick () {
			int endingResultCount = 0;
			
			for (int i=0; i<children.Count; i++) {
				
				if (func == ParallelFunction.And) {
					if (results[i] == BTResult.Running) {
						results[i] = children[i].Tick();	
					}
					if (results[i] != BTResult.Running) {
						endingResultCount++;	
					}
				}
				else {
					if (results[i] == BTResult.Running) {
						results[i] = children[i].Tick();	
					}
					if (results[i] != BTResult.Running) {
						BTResult result = results[i];
						ResetResults();
						return result;
					}
				}
			}
			if (endingResultCount == children.Count) {	// only apply to AND func
				ResetResults();
				return BTResult.Ended;
			}
			return BTResult.Running;
		}
		
		public override void Clear () {
			ResetResults();
			
			foreach (BTNode child in children) {
				child.Clear();	
			}
		}
		
		public override void AddChild (BTNode aNode) {
			base.AddChild (aNode);
			results.Add(BTResult.Running);
		}
		
		private void ResetResults () {
			for (int i=0; i<results.Count; i++) {
				results[i] = BTResult.Running;	
			}
		}
		
		public enum ParallelFunction {
			And = 1,	// returns Ended when all results are not running
			Or = 2,		// returns Ended when any result is not running
		}
	}

}