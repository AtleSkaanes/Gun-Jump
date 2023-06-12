using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationInstantiator
{
    public static void TranslateNotificationPosition(GameObject Notification, Transform Parent, RectTransform DesiredSize)
    {
        RectTransform NotificationSize = Notification.GetComponent<RectTransform>();

        Notification.transform.SetParent(Parent);

        NotificationSize.sizeDelta = DesiredSize.sizeDelta;
        NotificationSize.localScale = DesiredSize.localScale;
        NotificationSize.localPosition = DesiredSize.position;
    }
}
