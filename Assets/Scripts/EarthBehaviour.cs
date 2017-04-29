using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EarthBehaviour : MonoBehaviour
{
	[System.Serializable]
	public struct VectorStringStruct
	{
		public EarthLevelManager.Location name;
		[System.Serializable]
		public struct EraLocalPos
		{
			public EarthLevelManager.TimeEra era;
			public Vector3 localPos;
		}
		public EraLocalPos[] eraLocalPos;
	}
	public Transform earthModel;
	Vector2 hitPosition;
	public GameObject pinObject;
	public Transform lightPosition;
	public Transform lightIndicator;

	public Texture modernEarth, jurassicEarth;
	public VectorStringStruct[] targetLocation;

	Sequence sequence;

	public int currentSelectedLocation = -1;


	// Update is called once per frame
	void Update()
	{
		Vector2 inputInfo = InputController.instance.GetTouchHitPosition(InputController.TouchHit.Release);

		if (inputInfo != Vector2.zero)
		{
			RaycastHit hitInfo;
			var ray = InputController.instance.cam.ScreenPointToRay(inputInfo);

			if (Physics.Raycast(ray, out hitInfo))
			{
				if (earthModel.transform.childCount > 0 && earthModel.transform.GetChild(0).gameObject.name.ToLower().Contains("sphere"))
				{
					Destroy(earthModel.transform.GetChild(0).gameObject);
				}

				Debug.Log("EarthBehaviour : " + hitInfo.transform.name);
				SetPinPoint(hitInfo);

			}
		}
		else SetLocationName(-1);
		//Debug.Log("inputAxis:" + InputController.instance.GetTouchMoveAxis());
		earthModel.Rotate(new Vector3(0f, InputController.instance.GetTouchMoveAxis().x * (-1f), 0f));
		Vector3 relPos = transform.InverseTransformPoint(lightPosition.position);
		GetComponent<Renderer>().material.SetVector("_LightDir", new Vector4(relPos.x, relPos.y, relPos.z, 1));
		lightIndicator.localPosition = relPos;


	}

	public void SetTexture(string targetEra)
	{
		if (targetEra.ToLower().Contains("modern"))
			GetComponent<Renderer>().material.SetTexture("_DiffuseTex", modernEarth);
		else if (targetEra.ToLower().Contains("jurassic"))
			GetComponent<Renderer>().material.SetTexture("_DiffuseTex", jurassicEarth);
	}

	//void SetPinPoint(Vector3 point)
	//{
	//	GameObject go = Instantiate(pinObject, earthModel);
	//	go.transform.SetAsFirstSibling();
	//	go.transform.position = point;
	//}

	void SetPinPoint(RaycastHit hitInfo)
	{
		currentSelectedLocation = (CheckHitInfo(hitInfo.point));
		if (currentSelectedLocation > -1)
		{
			var vss = targetLocation[currentSelectedLocation];
			var indx = vss.eraLocalPos.ToList().FindIndex(p => p.era == EarthLevelManager.instance.currentTimeEra);
			if (indx > 0)
			{
				GameObject go = Instantiate(pinObject, earthModel);
				go.transform.SetAsFirstSibling();

				go.transform.localPosition = targetLocation[currentSelectedLocation].eraLocalPos[indx].localPos * 1.1f;
			}

		}
		else
		{
			GameObject go = Instantiate(pinObject, earthModel);
			go.transform.SetAsFirstSibling();

			go.transform.position = hitInfo.point;
		}
		SetLocationName(currentSelectedLocation);
	}

	int CheckHitInfo(Vector3 hitPoint)
	{
		int ans = -1;

		Vector3 localspace = transform.InverseTransformPoint(hitPoint);
		for (int i = 0; i < targetLocation.Length; i++)
		{
			VectorStringStruct vss = targetLocation[i];
			int indx = vss.eraLocalPos.ToList().FindIndex(p => p.era == EarthLevelManager.instance.currentTimeEra);
			if (Vector3.Distance(localspace, vss.eraLocalPos[indx].localPos) < 0.1f)
				return i;

			//vss.eraLocalPos;
		}
		return -1;

	}

	void SetLocationName(int targetIndex)
	{
		Transform locationtransform = GameObject.Find("Canvas").transform.Find("LocationName");
		CanvasGroup cv = locationtransform.GetComponent<CanvasGroup>();
		if (targetIndex == -1)
		{
			EarthLevelManager.instance.currentLocation = EarthLevelManager.Location.None;
			sequence = DOTween.Sequence();
			sequence.Append(cv.DOFade(0f, 0.5f));
			return;
		}
		sequence = DOTween.Sequence();

		sequence.Append(cv.DOFade(0f, 0.5f));
		string Cname = targetLocation[targetIndex].name.ToString();
		sequence.AppendCallback(() =>
		{
			cv.GetComponentInChildren<UnityEngine.UI.Text>().text = Cname;
			EarthLevelManager.instance.currentLocation = targetLocation[targetIndex].name;
		});
		sequence.Append(cv.DOFade(1f, 0.5f));


	}

}


