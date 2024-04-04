using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPartsIncreaser : MonoBehaviour
{
    [SerializeField] private Transform scaleBody;
    [SerializeField] private TMP_Text levelText;
    
    private Player _player;
    private PlayerSkinSwitcher _skinSwitcher;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _skinSwitcher = GetComponent<PlayerSkinSwitcher>();
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
        GameManager.Instance.ChangeSliderValue(Mathf.Lerp(0, 1, currentHealth / maxHealth));
            scaleBody.localScale += Vector3.one * 0.1f;

        // TODO: Тут нужно сделать обновление UI для уровня змеи  и её размера 
    }


}
