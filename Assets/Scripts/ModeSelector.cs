using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModeSelector : MonoBehaviour
{
	public enum InputMode
	{
		NULL,
		SLOTSET,
		SLOTPLANT,
		SLOTGATHER
	}

	public InputMode inputMode;

	private void Awake()
	{
		GameManager.Instance.modeSelector = this;
	}

	public void ChangeInputMode(InputMode newMode)
	{
		inputMode = newMode;
	}
}
