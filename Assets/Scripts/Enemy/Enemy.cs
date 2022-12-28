using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private float speed;

    private RagdollController _ragdollController;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _target;

    private bool _isDead;
    private float _deadTimer;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _ragdollController = GetComponent<RagdollController>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        SetRotation();
        _ragdollController.SetRagdollMode(false);
    }

    private void SetRotation()
    {
        _target = GameManager.instance.PlayerTransform.position;
        _transform.DOLookAt(_target, 0.3f);
    }

    private void FixedUpdate()
    {
        if (_isDead)
        {
            _deadTimer += Time.deltaTime;

            if (_deadTimer>=2f)
            {
                _isDead = false;
                _deadTimer = 0;
                ReleaseMe();
            }
        }

        _rigidbody.MovePosition(_transform.position + _transform.forward * Time.deltaTime * speed);
    }

    public void GetHit()
    {

        _isDead = true;
        _ragdollController.RagdollOnOff();
     //   Invoke(nameof(ReleaseMe), 3f);


    }

    private void ReleaseMe()
    {
        EnemyPool.instance.ReleaseEnemy(gameObject);
    }

}
