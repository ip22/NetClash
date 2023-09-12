using UnityEngine;

[CreateAssetMenu(fileName = "Empty", menuName = "UnitState/Empty")]
public class EmptyUnitState : UnitState
{
    public override void Init() {
        _unit.SetState(UnitStateType.Default);
    }

    public override void Run() {
    }

    public override void Finish() {
        Debug.Log($"Unit {_unit.name} was in a empty state, switch it to Default state");
    }
}
