using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool _targetIsEnemy;
    private Tower _nearestTower;
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
        _targetPosition = _nearestTower.transform.position;
        _agent.SetDestination(_targetPosition);
    }

    public override void Run() {
        TryAttackTower();
    }

    private void TryAttackTower() {
        float distanceTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceTarget <= _unit.parameters.startAttackDistance) {
            Debug.Log("Unit reach target.");

            _unit.SetState(UnitStateType.Attack);
        }
    }

    public override void Finish() {
        _agent.isStopped = true;
    }
}
