using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 4f;
    [field: SerializeField] public float modelRadius { get; private set; } = 1f;

    [field: SerializeField] public float startChaseDistance { get; private set; } = 5f;

    [field: SerializeField] public float stopChaseDistance { get; private set; } = 7f;

    public float startAttackDistance { get { return modelRadius + _startAttackDistance; } }
    [SerializeField] private float _startAttackDistance;

    public float stopAttackDistance { get { return modelRadius + stopAttackDistance; } }
    [SerializeField] private float _stopAttackDistance;

}
