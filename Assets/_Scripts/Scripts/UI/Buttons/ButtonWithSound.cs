using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithSound : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _audioClip;

    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        _button.onClick?.RemoveListener(PlaySound);
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlaySound(_audioClip);
    }
}
