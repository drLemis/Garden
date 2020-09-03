using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputRaycast : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.GetComponent<Slot>() != null)
				{
					hit.collider.GetComponent<Slot>().ProcessInput();
				}
			}
		}
	}
}
