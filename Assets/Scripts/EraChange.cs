using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EraChange : MonoBehaviour
{
	Sequence sequence,sequence2;
	CanvasGroup cv;

	// Use this for initialization
	void Start()
	{
		sequence = DOTween.Sequence();
		cv = GetComponent<CanvasGroup>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ChangeEra()
	{
		int targetEra = Mathf.RoundToInt(GameObject.Find("Canvas").transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value);
		sequence = DOTween.Sequence();
		sequence.Append(cv.DOFade(0f, 0.5f));
		sequence.AppendCallback(() =>
		{
			switch (targetEra)
			{
				case 0:
					cv.GetComponentInChildren<UnityEngine.UI.Text>().text = "Cretaceous";
					break;
				case 1:
					cv.GetComponentInChildren<UnityEngine.UI.Text>().text = "Jurassic";
					break;
				case 2:
					cv.GetComponentInChildren<UnityEngine.UI.Text>().text = "Triassic";
					break;
				case 3:
					cv.GetComponentInChildren<UnityEngine.UI.Text>().text = "Modern";
					break;
				default:
					break;
			}
		});
		sequence.Append(cv.DOFade(1f, 0.5f));

		sequence2 = DOTween.Sequence();
		Transform earthModel = GameObject.Find("earthModel").transform;
		sequence2.Append(earthModel.DOScale(Vector3.one * 3f,0.5f).SetEase(Ease.OutSine));
		sequence2.Join(earthModel.DORotate(new Vector3(0, 7200f, 0), 1f, RotateMode.FastBeyond360)).SetEase(Ease.InOutExpo);
		sequence2.Insert(0.5f, earthModel.DOScale(Vector3.one,0.5f).SetEase(Ease.InSine));

	}
}
