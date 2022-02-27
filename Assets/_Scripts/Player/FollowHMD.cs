using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHMD : MonoBehaviour
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
        
        transform.eulerAngles = new Vector3(0, HMD.eulerAngles.y, 0);

        // _transformEulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        // transform.eulerAngles = _transformEulerAngles;
    }
}