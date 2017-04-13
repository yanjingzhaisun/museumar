using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

	static InputController _instance = null;
	float swipeSpeed = 0.05F;
	float inputX;
	float inputY;
	Vector2 touchDeltaPosition;
	public static InputController instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("GameManager").GetComponent<InputController>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	public Vector2 GetTouchHitPosition()
	{
		if (Input.GetButton("Fire1"))
		{
			Debug.Log("Input Controller: Fire button!");
			if (Input.touchCount > 0)
				return Input.GetTouch(0).position;

		}
		return Vector2.zero;
	}
	public Vector2 GetTouchMoveAxis()
	{
		if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended)
			return Vector2.zero;
		return new Vector2(inputX, inputY);
	}

	Vector2 previousPosition;

	void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				touchDeltaPosition = Input.GetTouch(0).position - previousPosition;
				previousPosition = Input.GetTouch(0).position;
				inputX += touchDeltaPosition.x * swipeSpeed;
				inputY += touchDeltaPosition.y * swipeSpeed;
				//Debug.Log("X, Y: " + touchDeltaPosition.x + ", " + touchDeltaPosition.y);
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Began) {
				previousPosition = Input.GetTouch(0).position;
			}
		}
	}
}
