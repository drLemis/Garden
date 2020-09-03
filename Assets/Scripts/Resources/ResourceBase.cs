using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class ResourceBase : MonoBehaviour
{
	[SerializeField]
	private string nameObj;
	[SerializeField]
	private Text nameText;
	public string id;
	private int amount;
	private int amountMax;
	[SerializeField]
	private Text amountText;

	public Sprite iconSprite;
	[SerializeField]
	private Image iconImage;

	[SerializeField]
	private GameObject spriteSelected;

	protected ResourcesManager resourcesManager;
	protected ToolsManager toolsManager;
	protected ModeSelector modeSelector;

	private void Start()
	{
		resourcesManager = GameManager.Instance.resourcesManager;
		toolsManager = GameManager.Instance.toolsManager;
		modeSelector = GameManager.Instance.modeSelector;
		SyncVisual();
	}

	public void Initialize(ResourcesConfig.ResourceBase resource)
	{
		this.gameObject.name = this.nameObj = resource.name;
		this.id = resource.id;
		this.amount = resource.amount;
		this.amountMax = resource.amountMax;
		this.iconSprite = resource.icon;
	}

	public bool ChangeAmount(int delta)
	{
		if (CheckAmount(delta))
		{
			amount += delta;
			SyncAmountText();
			return true;
		}
		return false;
	}

	public bool CheckAmount(int delta)
	{
		if (amount < 0 || amount + delta >= 0)
			return true;
		else
			return false;
	}

	private void SyncVisual()
	{
		nameText.text = nameObj;
		iconImage.sprite = iconSprite;

		SyncAmountText();
	}

	private void SyncAmountText()
	{
		if (amount >= 0 && amountMax >= 0)
			amountText.text = (amountMax - amount).ToString() + "/" + amountMax.ToString();
		else if (amount >= 0)
			amountText.text = amount.ToString();
		else
			amountText.text = "";
	}

	public void Push()
	{
		if (toolsManager.activeTool != null)
		{
			toolsManager.activeTool.Select(false);
			toolsManager.activeTool = null;
		}

		if (resourcesManager.activeResource == this)
		{
			resourcesManager.activeResource = null;

			Select(false);
		}
		else
		{
			if (resourcesManager.activeResource != null)
				resourcesManager.activeResource.Select(false);

			resourcesManager.activeResource = this;

			Select(true);
		}
	}

	public void Select(bool select)
	{
		if (spriteSelected != null)
			spriteSelected.SetActive(select);
	}
}
