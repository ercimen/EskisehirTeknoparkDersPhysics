using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoRotation : MonoBehaviour
{
    [SerializeField] Transform rotationObject;
    [SerializeField] float rotationValue;
    [SerializeField] float rotateTime;

    private void Start()
    {
        var newVector = new Vector3(0, rotationValue, 0);
        rotationObject.DOLocalRotate(newVector, rotateTime).SetLoops(-1, LoopType.Incremental);
       
    }
}
