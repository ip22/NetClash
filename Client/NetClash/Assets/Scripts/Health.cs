using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float _max { get; private set; } = 10f;
    private float _current;

    private void Start() {
        _current = _max;
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) _current = 0;

        Debug.Log($"Object {name}: last - {_current + value}, new {_current}");
    }
}

interface IHealth
{
    Health health { get; }
}
