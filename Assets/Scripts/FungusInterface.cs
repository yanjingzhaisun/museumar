using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusInterface : MonoBehaviour {
	public string sceneInfo;
	public void QuizCorrect(){
		GameManager.instance.QuizCorrect();
	}
}
