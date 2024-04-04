using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPartsIncreaser : MonoBehaviour
{
    [SerializeField] private float _maxBlendShapeValue = 70f;
    
    
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


        GameManager.Instance.ChangeSliderValue(Mathf.Lerp(0, 1, currentHealth / maxHealth));
        
        // TODO: Тут нужно сделать обновление UI для уровня змеи  и её размера 
    }


}
