using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
	public KeyCode triggerKeyNext = KeyCode.Equals;
	public KeyCode triggerKeyPrev = KeyCode.Minus;

	void Update()
	{
		// Increment scenes
		if (Input.GetKeyDown(triggerKeyNext))
		{
			int nextLevel = Application.loadedLevel;
			nextLevel++;
			if (nextLevel >= Application.levelCount)
			{
				nextLevel = 0;
			}
			Application.LoadLevel(nextLevel);
		}
	if (Input.GetKeyDown(triggerKeyPrev))
		{
			int prevLevel = Application.loadedLevel;
			prevLevel--;
			if (prevLevel == -1)
			{
				prevLevel = Application.levelCount - 1;
			}
			Application.LoadLevel(prevLevel);
		}
	}
}