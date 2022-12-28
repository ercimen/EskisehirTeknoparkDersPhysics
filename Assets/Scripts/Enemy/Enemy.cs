using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private float speed;



    private RagdollController _ragdollController;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _target;

    private void Awake()
    {
        _ragdollController = GetComponent<RagdollController>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        Init();
    }

    private void Init()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        _animator.enabled = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = false;
    }

    private void OnEnable()
    {
       
        _rigidbody.velocity = Vector3.zero;
        _ragdollController.SetRagdollMode(false);
        Invoke(nameof(SetRotation), 0.1f);
        Init();
    }

    private void SetRotation()
    {
        _target = GameManager.instance.PlayerTransform.position;
        _transform.DOLookAt(_target, 0.3f);
    }

    private void FixedUpdate()
    {

        _rigidbody.MovePosition(_transform.position + _transform.forward * Time.deltaTime * speed);
    }

    public void GetHit()
    {
        gameObject.layer = LayerMask.NameToLayer("Water");
        _ragdollController.RagdollOnOff();
        Invoke(nameof(ReleaseMe), 3f);

    }

    private void ReleaseMe()
    {
        EnemyPool.instance.ReleaseEnemy(gameObject);
    }

}
