using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolster : MonoBehaviour
{
    [SerializeField] private Transform HMD;
    [SerializeField] private float verticalOffset = -0.5f;
    [SerializeField] private float horizontalOffset;
    [SerializeField] private float depth;
    private Vector3 _transformEulerAngles;

    void Update()
    {
        transform.position = new Vector3(HMD.position.x + horizontalOffset, HMD.position.y + verticalOffset,
            HMD.position.z + depth);

        _transformEulerAngles = transform.eulerAngles;
        _transformEulerAngles.y = HMD.eulerAngles.y;
    }
}