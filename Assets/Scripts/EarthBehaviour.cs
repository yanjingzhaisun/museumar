using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBehaviour : MonoBehaviour
{
	public Transform earthModel;
	Vector2 hitPosition;
	public GameObject pinObject;
	public Transform lightPosition;
	public Transform lightIndicator;






	// Update is called once per frame
	void Update()
	{
		Vector2 inputInfo = InputController.instance.GetTouchHitPosition();

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
				GameObject go = Instantiate(pinObject, earthModel);
				go.transform.SetAsFirstSibling();
				go.transform.position = hitInfo.point;
			}
		}
		Debug.Log("inputAxis:" + InputController.instance.GetTouchMoveAxis());
		earthModel.Rotate(new Vector3(0f, InputController.instance.GetTouchMoveAxis().x * (-1f), 0f));
		Vector3 relPos = transform.InverseTransformPoint(lightPosition.position);
		GetComponent<Renderer>().material.SetVector("_LightDir", new Vector4(relPos.x, relPos.y, relPos.z, 1));
		lightIndicator.localPosition = relPos;

	}
}
