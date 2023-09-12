using UnityEngine;

public abstract class UnitState : ScriptableObject
{
    protected Unit _unit;
    public virtual void Constuctor(Unit unit) {
        _unit = unit;
    }
    public abstract void Init();
    public abstract void Run();
    public abstract void Finish(); // aka OnDisable

#if UNITY_EDITOR
    public virtual void DebugDrawGizmos(Unit unit) { }
#endif
}

public enum UnitStateType
{
    None,
    Default,
    Chase,
    Attack
}