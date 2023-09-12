using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _filledImage;
    private float _maxHealth;

    private void Start() {
        _healthBar.SetActive(true);
        _maxHealth = _unit.health.max;

        _unit.health.UpdateHealth += UpdateHealth;
    }

    private void OnDestroy() {
        _unit.health.UpdateHealth -= UpdateHealth;
    }

    private void UpdateHealth(float currentValue) {
        _healthBar.SetActive(true);
        _filledImage.fillAmount = currentValue / _maxHealth;
    }
}
