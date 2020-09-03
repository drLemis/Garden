using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesManager : MonoBehaviour
{
	private Dictionary<string, ResourceSettable> resourcesSettable = new Dictionary<string, ResourceSettable>();
	private Dictionary<string, ResourcePlantable> resourcesPlantable = new Dictionary<string, ResourcePlantable>();
	private Dictionary<string, ResourceGatherable> resourcesGatherable = new Dictionary<string, ResourceGatherable>();

	public ResourceBase activeResource;

	[SerializeField]
	private ResourcesConfig resourcesConfig;

	[SerializeField]
	private Transform originSettable;
	[SerializeField]
	private Transform originPlantable;
	[SerializeField]
	private Transform originGatherable;

	private void Awake()
	{
		GameManager.Instance.resourcesManager = this;

		Spawn();
	}

	private void Spawn()
	{
		foreach (var resource in resourcesConfig.resourcesSettable)
		{
			ResourceSettable newRes = Instantiate(resourcesConfig.prefabSettable, originSettable).GetComponent<ResourceSettable>();

			if (SetIntoList(ref resourcesSettable, newRes, resource.id))
			{
				newRes.Initialize(resource);

				newRes.prefab = resource.prefab;
				newRes.gatherToolsID = resource.gatherToolsID;
			}
		}

		foreach (var resource in resourcesConfig.resourcesPlantable)
		{
			ResourcePlantable newRes = Instantiate(resourcesConfig.prefabPlantable, originPlantable).GetComponent<ResourcePlantable>();

			if (SetIntoList(ref resourcesPlantable, newRes, resource.id))
			{
				newRes.Initialize(resource);

				newRes.growTimeSeconds = resource.growTimeSeconds;
				newRes.needSettableID = resource.needSettableID;
				newRes.gatherToID = resource.gatherToID;
				newRes.gatherToolsID = resource.gatherToolsID;
				newRes.prefabs = resource.prefabs;
			}
		}

		foreach (var resource in resourcesConfig.resourcesGatherable)
		{
			ResourceGatherable newRes = Instantiate(resourcesConfig.prefabGatherable, originGatherable).GetComponent<ResourceGatherable>();

			if (SetIntoList(ref resourcesGatherable, newRes, resource.id))
			{
				newRes.Initialize(resource);
			}
		}
	}

	private bool SetIntoList<T>(ref Dictionary<string, T> dict, T resource, string id)
	{
		if (dict.ContainsKey(id))
		{
			Debug.LogError("Resource ID " + id + " already exists!");
			return false;
		}
		else
		{
			dict[id] = resource;
			return true;
		}
	}

	private T GetFromList<T>(Dictionary<string, T> dict, string id)
	{
		if (dict.ContainsKey(id))
		{
			return dict[id];
		}
		else
		{
			return default(T);
		}
	}

	// futureproof
	public Transform GetResourceSettableTransform(string id)
	{
		return GetFromList(resourcesSettable, id).transform;
	}

	// futureproof
	public Transform GetResourcePlantableTransform(string id)
	{
		return GetFromList(resourcesPlantable, id).transform;
	}

	public Transform GetResourceGatherableTransform(string id)
	{
		return GetFromList(resourcesGatherable, id).transform;
	}

	public bool AddResourceSettable(string id, int addValue)
	{
		if (!resourcesSettable[id].CheckAmount(addValue))
			return false;

		resourcesSettable[id].ChangeAmount(addValue);
		return true;
	}

	public bool AddResourcePlantable(string id, int addValue)
	{
		if (!resourcesPlantable[id].CheckAmount(addValue))
			return false;

		resourcesPlantable[id].ChangeAmount(addValue);
		return true;
	}

	public bool AddResourceGatherable(string id, int addValue)
	{
		if (!resourcesGatherable[id].CheckAmount(addValue))
			return false;

		resourcesGatherable[id].ChangeAmount(addValue);
		return true;
	}
}
