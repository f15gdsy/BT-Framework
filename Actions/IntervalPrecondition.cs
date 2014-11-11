using UnityEngine;
using System.Collections;
using BT;

public class IntervalPrecondition : BTPrecondition {
	private float _interval;
	private float _minInterval;
	private float _maxInterval;
	private float _time;

	public IntervalPrecondition (float interval) : this (interval, interval) {

	}

	public IntervalPrecondition (float minInterval, float maxInterval) {
		_minInterval = minInterval;
		_maxInterval = maxInterval;
		_time = Random.Range(minInterval, maxInterval);
	}

	public override bool Check () {
		_time -= Time.deltaTime;
		if (_time <= 0) {
			_time = Random.Range(_minInterval, _maxInterval);
			return true;
		}
		return false;
	}
}
