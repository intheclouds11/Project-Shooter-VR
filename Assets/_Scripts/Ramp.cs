using System;
using System.Collections;
using UnityEngine;

public class Ramp : MonoBehaviour, IInteractable
{
    [Header("Elevate properties")] [SerializeField] private Vector3 _elevateAmount = new Vector3(0, 2, 0);
    [SerializeField] private bool _elevated;
    [SerializeField] private float _smoothing = 10f;

    [Header("Rotate properties")] [SerializeField] private Vector3 _rotateAmount = new Vector3(0, 90, 0);
    private bool _rotated;

    private Vector3 _startPosition;
    private Quaternion _startQuaternion;


    private void Start()
    {
        _startPosition = transform.position;
        _startQuaternion = transform.rotation;
    }

    private IEnumerator Elevate()
    {
        if (!_elevated)
        {
            var endPosition = _startPosition + _elevateAmount;
            
            while (Vector3.Distance(transform.position, endPosition) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, endPosition, _smoothing * Time.deltaTime);
                yield return null;
            }

            _elevated = true;
        }

        else
        {
            while (Vector3.Distance(transform.position, _startPosition) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, _startPosition, _smoothing * Time.deltaTime);
                yield return null;
            }

            _elevated = false;
        }
    }

    public IEnumerator Rotate()
    {
        if (!_rotated)
        {
            var endRotation = Quaternion.LookRotation(_rotateAmount, new Vector3(0, 1, 0));
            
            while (Quaternion.Angle(transform.rotation, endRotation) > 0.05f)
            {
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, endRotation, _smoothing * Time.deltaTime);
                yield return null;
            }

            _rotated = true;
        }
        else
        {
            while (Quaternion.Angle(transform.rotation, _startQuaternion) > 0.05f)
            {
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, _startQuaternion, _smoothing * Time.deltaTime);
                yield return null;
            }

            _rotated = false;
        }
    }

    public void Interact()
    {
        StartCoroutine(Elevate());
        // StartCoroutine(Rotate());
    }
}