using System;
using DG.Tweening;
using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Slider slider;
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField] private Transform endLevelPosition;
    private float _levelMagnitude;


    private void Awake()
    {
#if !UNITY_EDITOR
        GameReady.NotifyLoadingCompleted();
#endif
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        _levelMagnitude = (endLevelPosition.position-spawnPoint.PlayerStartPosition).magnitude;
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += InBackground;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= InBackground;
    }

    private void InBackground(bool isInBackground)
    {
        Time.timeScale = Convert.ToInt16(!isInBackground);
        Debug.Log("Time.timeScale" + Time.timeScale);
        AudioListener.pause = isInBackground;
        Debug.Log("Volume" + AudioListener.pause);
    }

    private void Update()
    {

        var currentMagnitude = (spawnPoint.PlayerStartPosition- spawnPoint.PlayerPosition).magnitude;
        var progress = currentMagnitude / _levelMagnitude;
        slider.value = progress;
    }

    public void ChangeSliderValue(float value)
    {
        // slider.DOKill();
        // slider.DOValue(value, 0.25f).SetEase(Ease.InOutSine);
    }


    public void ResetSlider()
    {
        // slider.DOKill();
        // slider.value = 0f;
    }

    public void DisableSlider()
    {
        ChangeSliderState(false);
    }

    public void EnableSlider()
    {
        ChangeSliderState(true);
    }

    private void ChangeSliderState(bool state)
    {
        slider.transform.parent.gameObject.SetActive(state);
    }
}