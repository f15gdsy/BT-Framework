using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	public class BTParallelLoose : BTNode {

		private List<BTNode> activeChildren = new List<BTNode>();


		public BTParallelLoose (BTPrecondition precondition = null) : base (precondition) {}
		
		protected override bool DoEvaluate () {
			foreach (BTNode child in children) {
				if (!child.Evaluate()) {
					if (activeChildren.Contains(child)) {
						activeChildren.Remove(child);
					}
				}
				else if (!activeChildren.Contains(child)) {
					activeChildren.Add(child);
				}
			}
			if (activeChildren.Count == 0) {
				return false;
			}
			return true;
		}

		public override BTResult Tick () {

			for (int i=activeChildren.Count-1; i>=0; i--) {
				BTNode child = activeChildren[i];
				BTResult result = child.Tick();
				if (result == BTResult.Ended) {
					activeChildren.RemoveAt(i);
				}
			}

			if (activeChildren.Count <= 0) {
				return BTResult.Ended;
			}

			return BTResult.Running;
		}

		public override void Clear () {
			base.Clear ();

			foreach (BTNode child in children) {
				child.Clear();
			}
		}
	}

}