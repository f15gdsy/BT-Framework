using UnityEngine;
using System.Collections;

namespace BT {

	public class BTPrecondition2 : BTPrecondition {
		private BTPrecondition _precondition1;
		private BTPrecondition _precondition2;
		private BTPrecondition2Param _param;


		public BTPrecondition2 (BTPrecondition precondition1, BTPrecondition precondition2, BTPrecondition2Param param) {
			_precondition1 = precondition1;
			_precondition2 = precondition2;
			_param = param;
		}

		public override void Activate (Database database) {
			base.Activate (database);

			_precondition1.Activate(database);
			_precondition2.Activate(database);
		}

		public override bool Check () {
			if (_param == BTPrecondition2Param.And) {
				return _precondition1.Check() && _precondition2.Check();
			}
			else {
				return _precondition1.Check() || _precondition2.Check();
			}
		}

		public enum BTPrecondition2Param {
			And,
			Or,
		}
	}

}