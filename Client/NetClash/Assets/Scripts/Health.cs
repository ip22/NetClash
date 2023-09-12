using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> HealthUpdate;

    [field: SerializeField] public float max { get; private set; } = 10f;
    private float _current;

    private void Start() {
        _current = max;
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) _current = 0;
        HealthUpdate?.Invoke(_current);

        Debug.Log($"Object {name}: last - {_current + value}, new {_current}");
    }
}

interface IHealth
{
    Health health { get; }
}
