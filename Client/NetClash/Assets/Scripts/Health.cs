using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI _ui;
    [field: SerializeField] public float _max { get; private set; } = 10f;

    private float _current;

    private void Start() {
        Debug.Log("health start");
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
        if (_current < 0) _current = 0;
        UpdateHP();

        Debug.Log($"{name}: {_current + value} - {value} = {_current}");
    }

    public void UpdateHP() => _ui.UpdateHealth(_max, _current);
}

interface IHealth
{
    Health health { get; }
}
