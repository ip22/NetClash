using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState
{
    [SerializeField] private bool _isEnemy = false;
    [SerializeField] private float _moveOffset = 1f;
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    public override void Init() {
        Vector3 unitPosition = _unit.transform.position;
        _targetPosition = MapInfo.Instance.GetNearestTowerPosition(in unitPosition, _isEnemy == false);

        _agent = _unit.GetComponent<NavMeshAgent>();
        _agent.SetDestination(_targetPosition);
    }

    public override void Run() {
        float distanceTarget = Vector3.Distance(_unit.transform.position, _targetPosition);
        if (distanceTarget <= _moveOffset) {
            _unit.SetState(UnitStateType.Attack);
            Debug.Log("Unit reach target.");
        }
    }

    public override void Finish() {
        _agent.isStopped = true;
    }

}
