using UnityEngine;
using System.Collections;
using BT;

public class CheckChance : BTPrecondition {
	private float _chance;
	private CheckChanceParam _param;


	public CheckChance (int chance, CheckChanceParam param) {
		_chance = chance;
		_param = param;
	}

	public override bool Check () {
		float chance = Random.Range(0, 100);
		if ((chance < _chance && _param == CheckChanceParam.GreaterThan) || (chance > _chance && _param == CheckChanceParam.LessThan)) {
			return true;
		}
		return false;
	}

	public enum CheckChanceParam {
		GreaterThan,
		LessThan
	}
}
