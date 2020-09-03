using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu()]
public class ResourcesConfig : ScriptableObject
{
	public GameObject prefabSettable;
	public ResourceSettable[] resourcesSettable;

	public GameObject prefabPlantable;
	public ResourcePlantable[] resourcesPlantable;

	public GameObject prefabGatherable;
	public ResourceGatherable[] resourcesGatherable;

	[Serializable]
	public class ResourceBase
	{
		public string name;
		public string id;
		public int amount;
		public int amountMax;
		public Sprite icon;
	}

	[Serializable]
	public class ResourceSettable : ResourceBase
	{
		public GameObject prefab;
		public string[] gatherToolsID;
	}

	[Serializable]
	public class ResourceGatherable : ResourceBase
	{
	}

	[Serializable]
	public class ResourcePlantable : ResourceBase
	{
		public GameObject[] prefabs;
		public float growTimeSeconds;
		public string[] needSettableID;
		public string gatherToID;
		public string[] gatherToolsID;
	}

}