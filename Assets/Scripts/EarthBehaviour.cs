using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBehaviour : MonoBehaviour
{
	Transform earthModel;
	Vector2 hitPosition;
	public GameObject pinObject;

	public bool working = false;

	// Use this for initialization
	void Start()
	{
		earthModel = transform.Find("PenguinContainer/PenguinModel");
	}



	// Update is called once per frame
	void Update()
	{
		Vector2 inputInfo = InputController.instance.GetTouchHitPosition();

		if (inputInfo != Vector2.zero)
		{
			RaycastHit hitInfo;
			var ray = Camera.main.ScreenPointToRay(inputInfo);

			if (Physics.Raycast(ray, out hitInfo))
			{
				if (earthModel.transform.childCount > 0 && earthModel.transform.GetChild(0).gameObject.name.ToLower().Contains("sphere"))
				{
					Destroy(earthModel.transform.GetChild(0).gameObject);
				}

				Debug.Log("EarthBehaviour : " + hitInfo.transform.name);
				GameObject go = Instantiate(pinObject, earthModel);
				go.transform.SetAsFirstSibling();
				go.transform.position = hitInfo.point;
			}
		}
		Debug.Log(InputController.instance.GetTouchMoveAxis());
		earthModel.Rotate(new Vector3(0f, InputController.instance.GetTouchMoveAxis().x * (-1f), 0f));

	}
}
