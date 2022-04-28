using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoPickup : MonoBehaviour
{
    private Vector3 initialPos;
    private GameObject ammoPrefab;
    public float forceMultiplier;
    private Vector3 velocity = Vector3.zero;
    private bool forcePulling;
    private Rigidbody rb;
    [SerializeField] private InputActionAsset XRControls;
    private InputActionMap gameplayActionMapLh;
    private InputActionMap gameplayActionMapRh;
    private InputAction gripLh;
    private InputAction gripRh;
    public GameObject leftHand;
    public GameObject rightHand;
    public bool withinReach;
    [SerializeField] private GameObject _fxPrefab;
    [SerializeField] private int _ammoAmount;
    [SerializeField] private WeaponType _weaponType;
    private PlayerAmmo _playerAmmo;

// levitating variables. Put into reusable levitating script instead?
    [SerializeField] private float _levitateMagnitude;
    private Vector3 tempPos;
    private Vector3 positionOffset;
    public float frequency;

    private void Awake()
    {
        gameplayActionMapLh = XRControls.FindActionMap("XRI LeftHand");
        gripLh = gameplayActionMapLh.FindAction("Select");
        gameplayActionMapRh = XRControls.FindActionMap("XRI RightHand");
        gripRh = gameplayActionMapRh.FindAction("Select");
        ammoPrefab = (GameObject) Resources.Load("Ammo Pickup - shotgun");
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        positionOffset = transform.position;
        _playerAmmo = FindObjectOfType<PlayerAmmo>();
        initialPos = transform.position;
    }

    void Update()
    {
        // tempPos = positionOffset;
        // tempPos.y += Mathf.Sin(frequency * Time.fixedTime) * _levitateMagnitude;
        // transform.position = tempPos;

        ProximityCheck();
    }

    private void ProximityCheck()
    {
        if (!withinReach) return;
        var distLH = Vector3.Distance(leftHand.transform.position, transform.position);
        var distRH = Vector3.Distance(rightHand.transform.position, transform.position);

        if (gripLh.inProgress)
        {
            // if (distLH < distRH) return;
            // if (Vector3.Angle(transform.up, transform.InverseTransformPoint(leftHand.transform.position)) > 90) return;
            if (distLH < 0.2f) Pickup();
            ForceGrab(leftHand);
        }

        else if (gripRh.inProgress)
        {
            // if (distLH > distRH) return;
            // if (Vector3.Angle(transform.up, transform.InverseTransformPoint(leftHand.transform.position)) > 90) return;
            if (distRH < 0.2f) Pickup();
            ForceGrab(rightHand);
        }

        else
        {
            rb.useGravity = true;
        }
        
    }

    private void ForceGrab(GameObject obj)
    {
        forcePulling = true;
        rb.useGravity = false;
        var distLH = Vector3.Distance(leftHand.transform.position, transform.position);
        var distRH = Vector3.Distance(rightHand.transform.position, transform.position);

        transform.position =
            Vector3.SmoothDamp(transform.position, obj.transform.position, ref velocity,
                forceMultiplier * Time.deltaTime);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, obj.transform.rotation, forceMultiplier * Time.deltaTime);
    }

    private void Pickup()
    {
        _playerAmmo.AddAmmo(_weaponType, _ammoAmount);
        SpawnAmmo();
        Instantiate(_fxPrefab, transform.position, quaternion.identity);
        Destroy(gameObject);
    }

    private void SpawnAmmo()
    {
        var newAmmo = Instantiate(ammoPrefab, initialPos, quaternion.identity);
        newAmmo.GetComponent<AmmoPickup>().leftHand = leftHand;
        newAmmo.GetComponent<AmmoPickup>().rightHand = rightHand;
        newAmmo.GetComponent<AmmoPickup>().ammoPrefab = ammoPrefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) withinReach = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) withinReach = false;
    }
}