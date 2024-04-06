using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPartsIncreaser : MonoBehaviour
{
    [SerializeField] private Transform scaleBody;
    [SerializeField] private TMP_Text levelText;
    
    private Player _player;
    private float _lastLevel;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;

    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {

        levelText.text = currentHealth.ToString();
        var levelDifference = currentHealth - _lastLevel;
        GameManager.Instance.ChangeSliderValue(Mathf.Lerp(0, 1, currentHealth / maxHealth));
            scaleBody.localScale += Vector3.one * (levelDifference * 0.05f);
            
        _lastLevel = currentHealth;
    }


}
