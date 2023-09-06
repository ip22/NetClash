using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public Unit _warriorPrefab;
    [SerializeField] public Material _warriorSkin;
    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [SerializeField] private float _delay = 2f;

    private float _time = 0f;

    private void Update() { if (CheckTime(_delay)) CreateWarrior(isEnemy); }

    private bool CheckTime(float delay) {
        _time += Time.deltaTime;
        if (_time < delay) return false;
        _time -= delay;
        return true;
    }

    public void CreateWarrior(bool enemy) =>
        Instantiate(_warriorPrefab, transform.position, Quaternion.identity).Init(enemy, _warriorSkin);
}