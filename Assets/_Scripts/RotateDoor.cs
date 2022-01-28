using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    [SerializeField] private float closeSpeed = 1f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Rotate()
    {
        _rb.AddTorque(0, closeSpeed, 0);
    }
}