using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public string targetScene;
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


	Tween tweener = null;
	float previousSliderValue = 0;
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
		tweener.Pause();
		EarthBehaviour earth = GameObject.Find("PenguinRoot").GetComponent<EarthBehaviour>();

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

    

	public void ResetState()
	{
		FindObjectsOfType<PoseController>().ToList().ForEach(p => p.ResetState());
		Transform t = GameObject.Find("SampleUI").transform.Find("SampleCanvas/RootPanel/Slider");
		if (t!=null)
			t.gameObject.SetActive(false);
		t = GameObject.Find("SampleUI").transform.Find("SampleCanvas/RootPanel/ConfirmButtonBorder");
		if (t!=null)
			t.gameObject.SetActive(false);

	}

	public void QuizCorrect() {
		GotoNextStages("Vuforia-2-ARScene");
	}

	public void GotoNextStages(string name)
	{
		targetScene = name;
		SceneManager.LoadScene("Vuforia-1-Loading");
	}
}



