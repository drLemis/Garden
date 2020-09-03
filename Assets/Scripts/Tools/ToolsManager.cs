using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolsManager : MonoBehaviour
{
	private Dictionary<string, Tool> tools = new Dictionary<string, Tool>();

	public Tool activeTool = null;

	[SerializeField]
	private ToolsConfig toolsConfig;

	[SerializeField]
	private Transform buttonsOrigin;

	private void Awake()
	{
		GameManager.Instance.toolsManager = this;
		Spawn();
	}

	private void Spawn()
	{
		foreach (var tool in toolsConfig.tools)
		{
			if (tools.ContainsKey(tool.id))
			{
				Debug.LogError("Tool ID " + tool.id + " already exists!");
				return;
			}
			else
			{
				Tool newTool = Instantiate(toolsConfig.prefab, buttonsOrigin).GetComponent<Tool>();
				newTool.nameObj = tool.name;
				newTool.id = tool.id;
				newTool.iconSprite = tool.icon;

				tools[tool.id] = newTool;

				newTool.gameObject.name = tool.name;
			}
		}
	}
}
