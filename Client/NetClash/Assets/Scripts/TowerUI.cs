using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _filledImage;
    private float _maxHealth;

    private void Start() {
        _healthBar.SetActive(true);
        _maxHealth = _tower.health.max;

        _tower.health.HealthUpdate += UpdateHealth;
    }

    private void OnDestroy() {
        _tower.health.HealthUpdate -= UpdateHealth;
    }

    private void UpdateHealth(float currentValue) {
        _healthBar.SetActive(true);
        _filledImage.fillAmount = currentValue / _maxHealth;
    }
}
