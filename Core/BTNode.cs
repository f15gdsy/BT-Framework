using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT {

	public abstract class BTNode {
		public List<BTNode> children;
		public BTPrecondition precondition;
		public Database database;

		private BTNode _parent;
		public BTNode parent {
			get{return _parent;}
			set{_parent = value;}
		}

		public float interval = 0;
		private float _lastTimeEvaluated = 0;

		private bool _activated;
		public bool activated {get{return _activated;}}


		public BTNode () : this (null) {}

		public BTNode (BTPrecondition precondition) {
			this.precondition = precondition;
		}
		
		// to use with BTNode's constructor to provide initialization delay
		public virtual void Init () {}
		public bool Evaluate () {
			bool timerOk = (Time.time - _lastTimeEvaluated) > interval;
			if (timerOk) {
				_lastTimeEvaluated = Time.time;
			}
			return timerOk && (precondition == null || precondition.Check()) && DoEvaluate();
		}
		public virtual BTResult Tick () {return BTResult.Ended;}
		public virtual void Clear () {}
		
		protected virtual bool DoEvaluate () {return true;}
		
		public virtual void AddChild (BTNode aNode) {
			if (children == null) {
				children = new List<BTNode>();	
			}
			if (aNode != null) {
				children.Add(aNode);
				aNode.parent = this;
			}
		}
		
		public virtual void Activate (Database database) {
			if (_activated) return ;

			this.database = database;
			Init();
			
			if (precondition != null) {
				precondition.Activate(database);
			}
			if (children != null) {
				foreach (BTNode child in children) {
					child.Activate(database);
				}
			}

			_activated = true;
		}	
	}
	
	
	public enum BTResult {
		Ended = 1,
		Running = 2,
	}
}