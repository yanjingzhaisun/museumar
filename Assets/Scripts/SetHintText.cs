using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SetHintText : MonoBehaviour {
	Sequence sequence;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHintTextContent(bool isSuccess, System.Action callbackAction = null) {
		sequence = DOTween.Sequence();
		GetComponent<Text>().text = isSuccess ? "Congradulation! This is the location and time period!" : "Sorry, this is the wrong location/era.";
		CanvasGroup cv = GetComponent<CanvasGroup>();
		sequence.Append(cv.DOFade(1f, 0.5f));

		sequence.Append(cv.DOFade(0f, 0.5f));
		if (callbackAction != null)
			sequence.AppendCallback(()=>callbackAction());
	}
}
