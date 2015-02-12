using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT.Ex {

	public class CheckVector3 : BTPreconditionUseDB {

		private Function _function;
		private List<Vector3> _targets;


		public CheckVector3 (string dataToCheck, Function function, List<Vector3> targets = null) : base (dataToCheck) {
			_function = function;
			_targets = targets;
		}

		public override bool Check () {
			Vector3 data = database.GetData<Vector3>(_dataIdToCheck);

			if (_function == Function.NotZero) {
				return data != Vector3.zero;
			}
			else if (_function == Function.Zero) {
				return data == Vector3.zero;
			}

			else if (_function == Function.XNotZero) {
				return data.x != 0;
			}
			else if (_function == Function.XZero) {
				return data.x == 0;
			}
			else if (_function == Function.XPositive) {
				return data.x > 0;
			}
			else if (_function == Function.XNegative) {
				return data.x < 0;
			}
			else if (_function == Function.XNotPositive) {
				return data.x <= 0;
			}
			else if (_function == Function.XNotNegative) {
				return data.x >= 0;
			}

			else if (_function == Function.YNotZero) {
				return data.y != 0;
			}
			else if (_function == Function.YZero) {
				return data.y == 0;
			}
			else if (_function == Function.YPositive) {
				return data.y > 0;
			}
			else if (_function == Function.YNegative) {
				return data.y < 0;
			}
			else if (_function == Function.YNotPositive) {
				return data.y <= 0;
			}
			else if (_function == Function.YNotNegative) {
				return data.y >= 0;
			}

			else if (_function == Function.ZNotZero) {
				return data.z != 0;
			}
			else if (_function == Function.ZZero) {
				return data.z == 0;
			}
			else if (_function == Function.ZPositive) {
				return data.z > 0;
			}
			else if (_function == Function.ZNegative) {
				return data.z < 0;
			}
			else if (_function == Function.ZNotPositive) {
				return data.z <= 0;
			}
			else if (_function == Function.ZNotNegative) {
				return data.z >= 0;
			}

			else if (_function == Function.MatchAny) {
				foreach (Vector3 target in _targets) {
					if (data == target) {
						return true;
					}
				}
				return false;
			}
			else if (_function == Function.MatchNone) {
				foreach (Vector3 target in _targets) {
					if (data == target) {
						return false;
					}
				}
				return true;
			}

			return false;
		}

		public enum Function {
			NotZero,
			Zero,

			XNotZero,
			XZero,
			XPositive,
			XNegative,
			XNotPositive,
			XNotNegative,

			YNotZero,
			YZero,
			YPositive,
			YNegative,
			YNotPositive,
			YNotNegative,

			ZNotZero,
			ZZero,
			ZPositive,
			ZNegative,
			ZNotPositive,
			ZNotNegative,

			MatchAny,
			MatchNone
		}
	}

}