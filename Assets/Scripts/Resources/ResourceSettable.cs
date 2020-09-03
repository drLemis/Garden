using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSettable : ResourceBase
{
	public GameObject prefab;
	public string[] gatherToolsID;

	[SerializeField]
	private void SetInputMode()
	{
		modeSelector.ChangeInputMode(ModeSelector.InputMode.SLOTSET);
	}
}
