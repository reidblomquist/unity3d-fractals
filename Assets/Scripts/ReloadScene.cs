using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	public KeyCode restartKey;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(restartKey))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	
	}
}
