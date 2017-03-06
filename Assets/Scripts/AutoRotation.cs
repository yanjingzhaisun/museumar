using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoRotation : MonoBehaviour {
	Tween tween;

	void Awake() { 
		tween = transform.DORotate(new Vector3(0, 360f, 0), 10f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		tween.Pause();
	}
	void OnEnable() {
		tween.Play();
	}

	void OnDisable() {
		tween.Pause();
	}

}
