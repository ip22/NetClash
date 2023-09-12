using UnityEditor;
using UnityEngine;

public abstract class UnitStateAttack : UnitState
{
    [SerializeField] private float _damage = 1.6f;
    [SerializeField] private float _delay = .6f;

    protected bool _targetIsEnemy;
    private float _stopAttackDistance = 0;
    private float _time = 0f;

    protected Health _target;

    public override void Constuctor(Unit unit) {
        base.Constuctor(unit);

        _targetIsEnemy = _unit.isEnemy == false;
    }

    public override void Init() {
        if (TryFindTarget(out _stopAttackDistance) == false) {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        _time = 0f;
        _unit.transform.LookAt(_target.transform.position);
    }

    public override void Run() {
        _time += Time.deltaTime;
        if (_time < _delay) return;
        _time -= _delay;

        if (_target == false) {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_target.transform.position, _unit.transform.position);
        if (distanceToTarget > _stopAttackDistance) _unit.SetState(UnitStateType.Chase);

        _target.ApplyDamage(_damage);
    }

    public override void Finish() {

    }

    protected abstract bool TryFindTarget(out float stopAttackDistance);


#if UNITY_EDITOR
    public override void DebugDrawGizmos(Unit unit) {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.startChaseDistance);
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.stopChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.startAttackDistance);
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.stopAttackDistance);
    }
#endif
}
