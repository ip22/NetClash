using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshChase", menuName = "UnitState/NavMeshChase")]
public class NavMeshChase : UnitState
{
    private bool _targetIsEnemy;
    private float _startAttackDistance = 0;
    private NavMeshAgent _agent;
    private Unit _targetUnit;

    public override void Constuctor(Unit unit) {
        base.Constuctor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"{unit.name} doesn't have NavMeshAgent");
    }

    public override void Init() {
        MapInfo.Instance.TryGetNearestUnit(_unit.transform.position, _targetIsEnemy, out _targetUnit, out float distance);
        _startAttackDistance = _unit.parameters.startAttackDistance + _targetUnit.parameters.modelRadius;
    }

    public override void Run() {
        if (_targetUnit == null) {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);

        if (distanceToTarget > _unit.parameters.stopChaseDistance) _unit.SetState(UnitStateType.Default);
        else if (distanceToTarget <= _startAttackDistance) _unit.SetState(UnitStateType.Attack);
        else _agent.SetDestination(_targetUnit.transform.position);
    }

    public override void Finish() {
        _agent.SetDestination(_unit.transform.position);
    }

#if UNITY_EDITOR
    public override void DebugDrawGizmos(Unit unit) {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.startChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.stopChaseDistance);
    }
#endif
}
