using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoRotation : MonoBehaviour {


	public float speed;
	public bool Paused;

	Tween spiningTween;

	// Use this for initialization
	void Start () {
		spiningTween = transform.DORotate(new Vector3(0, 180f, 0), 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
		spiningTween.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		if (!Paused)
			spiningTween.Play();
		else
			spiningTween.Pause();
	}
}
