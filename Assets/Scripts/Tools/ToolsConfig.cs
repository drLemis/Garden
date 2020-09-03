using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu()]
public class ToolsConfig : ScriptableObject
{
	public GameObject prefab;
	public Config[] tools;


	[Serializable]
	public class Config
	{
		public string name;
		public string id;
		public Sprite icon;
	}
}