using UnityEngine;
using UnityEngine.AI;

public abstract class UnitSateNavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Tower _nearestTower;

    public override void Constuctor(Unit unit) {
        base.Constuctor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"{unit.name} doesn't have NavMeshAgent");

        _agent.speed = _unit.parameters.speed;
        _agent.radius = _unit.parameters.modelRadius;
        _agent.stoppingDistance = _unit.parameters.startAttackDistance;
    }

    public override void Init() {
        Vector3 unitPosition = _unit.transform.position;
        _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _targetIsEnemy);
        _agent.SetDestination(_nearestTower.transform.position);
    }

    public override void Run() {
        if (TryFindTarget(out UnitStateType changeType))
            _unit.SetState(changeType);
    }

    protected abstract bool TryFindTarget(out UnitStateType changeType);

    public override void Finish() {
        _agent.SetDestination(_unit.transform.position);
    }
}
