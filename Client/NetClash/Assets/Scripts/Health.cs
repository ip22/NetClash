using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI _ui;
    [field: SerializeField] public float _max { get; private set; } = 10f;

    private float _current;
    public bool isDead { get; private set; } = false;

    private void Start() {
        Debug.Log("health start");
        isDead = false;
        _current = _max;
        UpdateHP();
    }

    public void SetMax(int max) {
        _max = max;
        UpdateHP();
    }

    public void SetCurrent(int current) {
        _current = current;
        UpdateHP();
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) {
            isDead = true;
            _current = 0;
            gameObject.SetActive(false);
        }
        UpdateHP();

        Debug.Log($"{name} health: {_current + value} - {value} = {_current}");
    }

    public void UpdateHP() => _ui.UpdateHealth(_max, _current);
}

interface IHealth
{
    Health health { get; }
}
