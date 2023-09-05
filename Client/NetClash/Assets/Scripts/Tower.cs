using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IHealth
{
    [field: SerializeField] public Health health { get; private set; }
    [field: SerializeField] public float radius { get; private set; } = 2f;

    private void Start() {
        Debug.Log("tower start");
        health.UpdateHP(); 
    }
    public float GetDistance(in Vector3 point) => Vector3.Distance(transform.position, point) - radius;
}
