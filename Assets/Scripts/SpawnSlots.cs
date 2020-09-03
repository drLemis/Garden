using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlots : MonoBehaviour
{
	[SerializeField]
	private Transform slotOrigin;
	[SerializeField]
	private GameObject slotPrefab;

	public Slot[,] slotGrid;

	void Start()
	{
		Spawn();
	}

	void Spawn(int size = 5)
	{
		float offset = size / 2;

		slotGrid = new Slot[size, size];

		float currentX = -offset;
		float currentY = -offset;

		for (int i = 0; i < size * size; i++)
		{
			Vector3 position = new Vector3(currentX, 0f, currentY);
			Slot newSlot = Instantiate(slotPrefab, position, Quaternion.identity, slotOrigin).GetComponent<Slot>();

			newSlot.paramsData.x = i % size;
			newSlot.paramsData.y = i / size;

			// futureproof, not used for now
			slotGrid[newSlot.paramsData.y, newSlot.paramsData.x] = newSlot;

			currentX += 1f;
			if (currentX > offset)
			{
				currentX = -offset;
				currentY += 1f;
			}
		}
	}

}
