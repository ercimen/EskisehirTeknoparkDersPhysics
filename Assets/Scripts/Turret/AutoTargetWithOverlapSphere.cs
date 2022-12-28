using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoTargetWithOverlapSphere : MonoBehaviour
{
    [SerializeField] private Transform rotationObject;
    [SerializeField] private float rotateTime;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerName;

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(rotationObject.transform.position, radius,layerName);
       
        foreach (var hitCollider in hitColliders)
        {
            Vector3 target = hitCollider.gameObject.transform.position;
            rotationObject.transform.DOLookAt(target, rotateTime);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        // Daha maliyetsiz ve hýzlý çalýþan eriþme yöntemi.
        if (other.TryGetComponent(out IHitable hitobject))
        {
         //   _transform.LookAt(other.transform.position); Anýnda o pozisyona bakar.
            rotationObject.transform.DOLookAt(other.transform.position, rotateTime); // belirttiðimiz sürede rotate olarak bakar.
        }

    }
}
