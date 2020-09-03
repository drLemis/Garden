using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotificationsManager : MonoBehaviour
{
	public Transform notifOrigin;
	public GameObject gatherPrefab;

	private void Awake()
	{
		GameManager.Instance.notificationsManager = this;
	}

	public void CreateNotificationGatherable(Transform slotFrom, ResourcePlantable resourceOrig, int addAmount)
	{
		Vector3 fromPos = Camera.main.WorldToScreenPoint(slotFrom.position);
		Vector3 toPos = GameManager.Instance.resourcesManager.GetResourceGatherableTransform(resourceOrig.gatherToID).position;

		GameObject newNotif = Instantiate(gatherPrefab, fromPos, Quaternion.identity, notifOrigin);
		newNotif.GetComponent<Image>().sprite = resourceOrig.iconSprite;

		newNotif.transform.DOMove(toPos, 1f).OnComplete(() =>
		{
			GameManager.Instance.resourcesManager.AddResourceGatherable(resourceOrig.gatherToID, 1);
			Destroy(newNotif);
		});

		newNotif.GetComponent<Image>().DOFade(0f, 0.25f).SetDelay(0.75f);
	}
}
