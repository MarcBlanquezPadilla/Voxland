using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    #region Instance

    private static NotificationsManager _instance;
    public static NotificationsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NotificationsManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private GameObject[] notificationsTemplates;

    public void ShowNotification(Sprite icon, string stringkey)
    {
        bool found = false;
        GameObject notification = null;

        for (int i = 0; i < notificationsTemplates.Length && !found; i++)
        {
            if (!notificationsTemplates[i].activeInHierarchy)
            {
                found = true;
                notification = notificationsTemplates[i];
            }
        }

        if (found)
        {
            notification.GetComponentInChildren<TextIdiom>().SetStringKey(stringkey);
            notification.GetComponent<Notification>().notificationIcon.sprite = icon;
            notification.SetActive(true);
        }
    }
}
