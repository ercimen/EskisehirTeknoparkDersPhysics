using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ClosestEnemyColliderOverlap : MonoBehaviour
{
    public bool Isactive;

    [SerializeField] private LayerMask _detectLayer;

    [SerializeField] private float _overlapRadius = 5.0f;

    private Transform _transform;

    [SerializeField] private Transform _nearestEnemy;

    [SerializeField] private Transform _overlapTransform;

    [SerializeField] private Transform _rotatingTransform;

    private void Start()
    {
        _transform = transform;
        Isactive = true;
    }

    public void SetActive(bool value)
    {
        Isactive = value;
    }


    private void FixedUpdate()
    {
        FindClosestEnemy();
    }


    private Collider[] colliders = new Collider[30];
    private void FindClosestEnemy()
    {

        var newOverlapPosition = new Vector3(_overlapTransform.position.x, 0, _overlapTransform.position.z);

        var hitColliders = Physics.OverlapSphereNonAlloc(newOverlapPosition, _overlapRadius,colliders, _detectLayer);

        float minimumDistance = Mathf.Infinity;

        for (int i = 0; i < hitColliders; i++)
        {
            var target = colliders[i].transform.position;


            if (_transform.position != target)
            {

                float distance = Vector3.Distance(_transform.position, target);

                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    _nearestEnemy = colliders[i].transform;
                }
            }
        }

        if (_nearestEnemy != null)
        {
            Vector3 target = new Vector3(_nearestEnemy.transform.position.x, _rotatingTransform.position.y, _nearestEnemy.transform.position.z);
            _rotatingTransform.DOLookAt(target,0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        var newOverlapPosition = new Vector3(_overlapTransform.position.x, 0, _overlapTransform.position.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(newOverlapPosition, _overlapRadius);
       
    } 
}