using UnityEngine;
using System.Collections;

namespace BT {

	/// <summary>
	/// BT precondition is used to check if a BTNode can be entered.
	/// Inherit from BTNode means it can be used as a normal node too,
	/// 	it is useful when you need to check some condition to end some logics
	/// 	where it is difficult to add pre condition to the action / logic nodes (due to reuse problem).
	/// </summary>
	public abstract class BTPrecondition : BTNode {

		public BTPrecondition () : base (null) {}

		// Override to provide the condition check.
		public abstract bool Check ();

		// Functions as a node
		public override BTResult Tick () {
			bool success = Check();
			if (success) {
				return BTResult.Ended;	
			}
			else {
				return BTResult.Running;	
			}
		}
	}



	/// <summary>
	/// A pre condition that uses database.
	/// </summary>
	public abstract class BTPreconditionUseDB : BTPrecondition {
		protected string _dataToCheck;
		protected int _dataIdToCheck;
		
		
		public BTPreconditionUseDB (string dataToCheck) {
			this._dataToCheck = dataToCheck;	
		}
		
		public override void Activate (Database database) {
			base.Activate (database);
			
			_dataIdToCheck = database.GetDataId(_dataToCheck);
		}
	}



	/// <summary>
	/// Used to check if the float data in the database is less than / equal to / greater than the data passed in through constructor.
	/// </summary>
	public class BTPreconditionFloat : BTPreconditionUseDB {
		public float rhs;
		private FloatFunction func;
		
		
		public BTPreconditionFloat (string dataToCheck, float rhs, FloatFunction func) : base(dataToCheck){
			this.rhs = rhs;
			this.func = func;
		}
		
		public override bool Check () {
			float lhs = database.GetData<float>(_dataIdToCheck);
			
			switch (func) {
			case FloatFunction.LessThan:
				return lhs < rhs;
			case FloatFunction.GreaterThan:
				return lhs > rhs;
			case FloatFunction.EqualTo:
				return lhs == rhs;
			}
			
			return false;
		}
		
		public enum FloatFunction {
			LessThan = 1,
			GreaterThan = 2,
			EqualTo = 3,
		}
	}



	/// <summary>
	/// Used to check if the boolean data in database is equal to the data passed in through constructor
	/// </summary>
	public class BTPreconditionBool : BTPreconditionUseDB {
		public bool rhs;
		
		public BTPreconditionBool (string dataToCheck, bool rhs) : base (dataToCheck) {
			this.rhs = rhs;
		}
		
		public override bool Check () {
			bool lhs = database.GetData<bool>(_dataIdToCheck);
			return lhs == rhs;
		}
	}



	/// <summary>
	/// Used to check if the boolean data in database is null
	/// </summary>
	public class BTPreconditionNull : BTPreconditionUseDB {
		private NullFunction func;
		
		public BTPreconditionNull (string dataToCheck, NullFunction func) : base (dataToCheck) {
			this.func = func;
		}
		
		public override bool Check () {
			object lhs = database.GetData<object>(_dataIdToCheck);
			
			if (func == NullFunction.NotNull) {
				return lhs != null;
			}
			else {
				return lhs == null;	
			}
		}
		
		public enum NullFunction {
			NotNull = 1,	// return true when dataToCheck is not null
			Null = 2,		// return true when dataToCheck is not null
		}
	}

	public enum CheckType {
		Same,
		Different
	}
}