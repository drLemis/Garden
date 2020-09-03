using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

	private static readonly GameManager instance = new GameManager();

	public static GameManager Instance
	{
		get { return instance; }
	}

	protected GameManager() { }

	// ########

	public ModeSelector modeSelector;
	public ResourcesManager resourcesManager;
	public ToolsManager toolsManager;
	public NotificationsManager notificationsManager;


}
