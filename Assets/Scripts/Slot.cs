using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slot : MonoBehaviour
{
	[Serializable]
	public struct Parameters
	{
		public int x;
		public int y;
		public bool havePlantGrown;
	}

	private GameObject setObject;
	private GameObject plantObject;

	public Parameters paramsData;

	private ResourceSettable resourceSettable;
	private ResourcePlantable resourcePlantable;


	private ModeSelector modeSelector;
	private ResourcesManager resourcesManager;
	private ToolsManager toolsManager;
	private NotificationsManager notificationsManager;

	private IEnumerator plantGrow;

	private void Start()
	{
		modeSelector = GameManager.Instance.modeSelector;
		resourcesManager = GameManager.Instance.resourcesManager;
		toolsManager = GameManager.Instance.toolsManager;
		notificationsManager = GameManager.Instance.notificationsManager;
	}

	public void ProcessInput()
	{
		switch (modeSelector.inputMode)
		{
			case ModeSelector.InputMode.SLOTSET:
				if (resourcesManager.activeResource != null)
					CreateSettable((ResourceSettable)resourcesManager.activeResource);
				break;
			case ModeSelector.InputMode.SLOTPLANT:
				if (resourcesManager.activeResource != null)
					CreatePlantable((ResourcePlantable)resourcesManager.activeResource);
				break;
			case ModeSelector.InputMode.SLOTGATHER:
				if (toolsManager.activeTool != null)
					GatherSlot();
				break;
			default:
				break;
		}
	}

	private void CreateSettable(ResourceSettable resource)
	{
		if (!resource.CheckAmount(-1))
			return;

		if (resourceSettable == null)
		{
			resourceSettable = resource;
			setObject = Instantiate(resource.prefab, transform);

			resource.ChangeAmount(-1);
		}
	}

	private void CreatePlantable(ResourcePlantable resource)
	{
		if (!resource.CheckAmount(-1))
			return;

		if (resourceSettable != null && resourcePlantable == null)
		{

			foreach (var id in resource.needSettableID)
			{
				if (resourceSettable.id == id)
				{
					resourcePlantable = resource;
					plantObject = Instantiate(resource.prefabs[0], transform);
					plantGrow = PlantGrow(resource);
					StartCoroutine(plantGrow);

					resource.ChangeAmount(-1);
				}
			}
		}
	}

	private void GatherSlot()
	{
		if (resourcePlantable != null && paramsData.havePlantGrown)
		{
			int pos = Array.IndexOf(resourcePlantable.gatherToolsID, toolsManager.activeTool.id);
			if (pos > -1)
			{
				resourcesManager.AddResourcePlantable(resourcePlantable.id, 1);

				notificationsManager.CreateNotificationGatherable(this.transform, resourcePlantable, 1);

				Destroy(plantObject);
				resourcePlantable = null;
				paramsData.havePlantGrown = false;
			}
		}

		if (resourceSettable != null)
		{
			int pos = Array.IndexOf(resourceSettable.gatherToolsID, toolsManager.activeTool.id);
			if (pos > -1)
			{
				if (resourcePlantable != null)
				{
					resourcesManager.AddResourcePlantable(resourcePlantable.id, 1);

					if (paramsData.havePlantGrown)
					{
						notificationsManager.CreateNotificationGatherable(this.transform, resourcePlantable, 1);
					}
				}

				resourcesManager.AddResourceSettable(resourceSettable.id, 1);

				Destroy(setObject);
				Destroy(plantObject);
				resourceSettable = null;
				resourcePlantable = null;
				paramsData.havePlantGrown = false;

				if (plantGrow != null)
				{
					StopCoroutine(plantGrow);
					plantGrow = null;
				}
			}
		}
	}

	private IEnumerator PlantGrow(ResourcePlantable resource)
	{
		yield return new WaitForSeconds(resource.growTimeSeconds);
		if (resourcePlantable != null)
		{
			Destroy(plantObject);
			plantObject = Instantiate(resource.prefabs[1], transform);
			paramsData.havePlantGrown = true;
		}
		yield return null;
	}
}
