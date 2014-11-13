using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	/// <summary>
	/// BTParallelFlexible evaluates all children, if all children fails evaluation, it fails. 
	/// Any child passes the evaluation will be regarded as active.
	/// 
	/// BTParallelFlexible ticks all active children, if all children ends, it ends.
	/// 
	/// NOTE: Order of child node added does matter!
	/// </summary>
	public class BTParallelFlexible : BTNode {

		private List<bool> _activeList = new List<bool>();


		public BTParallelFlexible (BTPrecondition precondition = null) : base (precondition) {}
		
		protected override bool DoEvaluate () {
			int numActiveChildren = 0;

			for (int i=0; i<children.Count; i++) {
				BTNode child = children[i];
				if (child.Evaluate()) {
					_activeList[i] = true;
					numActiveChildren++;
				}
				else {
					_activeList[i] = false;
				}
			}

			if (numActiveChildren == 0) {
				return false;
			}

			return true;
		}

		public override BTResult Tick () {
			int numRunningChildren = 0;

			for (int i=0; i<_children.Count; i++) {
				bool active = _activeList[i];
				if (active) {
					BTResult result = _children[i].Tick();
					if (result == BTResult.Running) {
						numRunningChildren++;
					}
				}
			}

			if (numRunningChildren == 0) {
				return BTResult.Ended;
			}

			return BTResult.Running;
		}

		public override void AddChild (BTNode aNode) {
			base.AddChild (aNode);
			_activeList.Add(false);
		}

		public override void RemoveChild (BTNode aNode) {
			int index = _children.IndexOf(aNode);
			_activeList.RemoveAt(index);
			base.RemoveChild (aNode);
		}

		public override void Clear () {
			base.Clear ();

			foreach (BTNode child in children) {
				child.Clear();
			}
		}
	}

}