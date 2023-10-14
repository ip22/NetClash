using UnityEngine;

[CreateAssetMenu(fileName = "_NavMeshTowerMove", menuName = "UnitState/NavMeshTowerMove")]
public class NavMeshTowerMove : UnitSateNavMeshMove
{
    protected override bool TryFindTarget(out UnitStateType changeType) {
        float distanceTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceTarget <= _unit.parameters.startAttackDistance) {
            changeType = UnitStateType.Attack;
            return true;
        } else {
            changeType = UnitStateType.None;
            return false;
        }
    }
}
