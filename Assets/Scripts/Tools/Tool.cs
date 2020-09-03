using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
	public GameObject spriteSelected;
	public string nameObj;
	public string id;
	public Sprite iconSprite;
	[SerializeField]
	private Image iconImage;

	private ToolsManager toolsManager;
	private ResourcesManager resourcesManager;
	private ModeSelector modeSelector;

	private void Start()
	{
		resourcesManager = GameManager.Instance.resourcesManager;
		modeSelector = GameManager.Instance.modeSelector;
		toolsManager = GameManager.Instance.toolsManager;
		iconImage.sprite = iconSprite;
	}

	[SerializeField]
	private void Push()
	{
		if (resourcesManager.activeResource != null)
		{
			resourcesManager.activeResource.Select(false);
			resourcesManager.activeResource = null;
		}

		if (toolsManager.activeTool != null)
		{
			toolsManager.activeTool.Select(false);
			if (toolsManager.activeTool == this)
			{
				toolsManager.activeTool = null;
				return;
			}
			toolsManager.activeTool = null;
		}

		if (toolsManager.activeTool == this)
		{
			toolsManager.activeTool = null;
			Select(false);

			modeSelector.ChangeInputMode(ModeSelector.InputMode.NULL);
		}
		else
		{
			if (toolsManager.activeTool != null)
				toolsManager.activeTool.Select(false);
			toolsManager.activeTool = this;
			Select(true);

			modeSelector.ChangeInputMode(ModeSelector.InputMode.SLOTGATHER);
		}
	}

	public void Select(bool select)
	{
		spriteSelected.SetActive(select);
	}
}
