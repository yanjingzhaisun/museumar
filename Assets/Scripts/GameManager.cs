using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

	static GameManager _instance = null;
	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	public Tween periodIndicatorTweener;

	void Awake()
	{
		if (GameManager.instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ChangeTimeSlider(float sliderValue)
	{
		if (sliderValue < 0.33f)
		{
			ShowTimeEra("Triassic");
		}
		else if (sliderValue > 0.33f && sliderValue < 0.67f)
		{
			ShowTimeEra("Jurassic");
		}
		else if (sliderValue > 0.67f)
		{
			ShowTimeEra("Cretaceous");
		}
	}
	void ShowTimeEra(string v)
	{
		if (periodIndicatorTweener != null)
			periodIndicatorTweener.Pause();
		UnityEngine.UI.Text text = GameObject.Find("PeriodIndicator").GetComponent<UnityEngine.UI.Text>();
		text.text = v;
		CanvasGroup cv = text.GetComponent<CanvasGroup>();
		cv.alpha = 1;
		periodIndicatorTweener = cv.DOFade(0, 10f);
		periodIndicatorTweener.Restart();
	}
}



