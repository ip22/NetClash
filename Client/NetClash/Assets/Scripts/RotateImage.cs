using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float _speed = 24f;
    void Update() => transform.Rotate(0, 0, _speed * Time.deltaTime);

}
