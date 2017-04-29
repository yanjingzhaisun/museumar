using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EarthLevelManager : MonoBehaviour
{
	public static EarthLevelManager instance;

	void Awake()
	{
		instance = this;
	}


	public enum TimeEra
	{
		Modern,
		Triassic,
		Jurassic,
		Cretaceous
	}

	public enum Location
	{
		None,
		Wyoming
	}
	public TimeEra currentTimeEra = TimeEra.Modern;
	public Location currentLocation = Location.None;

	bool _isConfirmable = false;
	public bool isConfirmable
	{
		get { return _isConfirmable; }
		set
		{
			if (_isConfirmable != value) {
				_isConfirmable = value;
				if (value)
					SetButtonConfirmEnabled(true);

				else
					SetButtonConfirmEnabled(false);
			}
		}
	}

	void SetButtonConfirmEnabled(bool target)
	{
		CanvasGroup cv = GameObject.Find("Canvas").transform.Find("ConfirmButton").GetComponent<CanvasGroup>();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(cv.DOFade(target ? 1f : 0f, 0.5f));
		sequence.AppendCallback(() => cv.interactable = target);
	}

	private void Update() {
		if (currentTimeEra != TimeEra.Modern && currentLocation != Location.None)
			isConfirmable = true;
	}

	public void ConfirmNextLevel() {
		if (currentTimeEra == TimeEra.Jurassic && currentLocation == Location.Wyoming)
		{
			GameManager.instance.GotoNextStages("Vuforia-3-ARScene-Dino");
		}
	}
}

