using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 _initialAttachLocalPos;
    private Quaternion _initialAttachLocalRot;

    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }
        
        _initialAttachLocalPos = attachTransform.localPosition;
        _initialAttachLocalRot = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.position = _initialAttachLocalPos;
            attachTransform.rotation = _initialAttachLocalRot;
        }

        base.OnSelectEntered(args);
    }
}