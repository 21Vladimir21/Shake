using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StartPanel _startPanel;

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPanel.Disable();
        YandexMetricaWrapper.SendStartEvent();
    }
}
