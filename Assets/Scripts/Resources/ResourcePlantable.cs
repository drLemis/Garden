using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePlantable : ResourceBase
{
	public float growTimeSeconds;
	public string[] needSettableID;
	public string gatherToID;
	public GameObject[] prefabs;
	public string[] gatherToolsID;

	public void SetInputMode()
	{
		modeSelector.ChangeInputMode(ModeSelector.InputMode.SLOTPLANT);
	}
}
