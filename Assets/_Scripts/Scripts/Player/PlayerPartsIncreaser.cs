using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPartsIncreaser : MonoBehaviour
{
    [SerializeField] private Transform scaleBody;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private SnakeTail snakeTail;
    
    
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

    public void ResetLevel() => scaleBody.localScale = Vector3.one;

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {

        levelText.text = currentHealth.ToString();
        var levelDifference = currentHealth - _lastLevel;
        GameManager.Instance.ChangeSliderValue(Mathf.Lerp(0, 1, currentHealth / maxHealth));
        var newScale = scaleBody.localScale;
        newScale  += Vector3.one * (levelDifference * 0.05f);
        scaleBody.DOScale(newScale, 1f);
            // scaleBody.localScale += Vector3.one * (levelDifference * 0.05f);
        //     
        //     snakeTail.ChaneTailScale(levelDifference,(int)currentHealth,true );
        _lastLevel = currentHealth;
    }
    


}
