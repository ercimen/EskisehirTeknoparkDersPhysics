using UnityEngine;

public class RagdollController : MonoBehaviour
{
   [SerializeField] private Collider _parentCollider;
   [SerializeField]  private Animator _animator;

    public bool IsRagdollEnabled;

    private void Awake()
    {
        SetRagdollMode(IsRagdollEnabled);
    }
    private void SetKinematicRigidBodies(bool value)
    {
        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody childRigidBody in rigidBodies)
        {
            childRigidBody.velocity = Vector3.zero;
            childRigidBody.isKinematic = value;
        }

    }

    private void SetColliders(bool value)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider childCollider in colliders)
        {
            childCollider.enabled = value;
        }
        _parentCollider.enabled = true;

        if (value == true)
        {
            _parentCollider.enabled = false;
        }
    }

    public void SetRagdollMode(bool value)
    {
        IsRagdollEnabled = value;

        if (value)
        {
            _animator.enabled = false;
            SetKinematicRigidBodies(false);
            SetColliders(true);
        }
        else
        {
            _animator.enabled = true;

            SetKinematicRigidBodies(true);
            SetColliders(false);
        }
    }


    /// <summary>
    /// Button ve Class Etkileþimi için Örnek Method
    /// </summary>
    public void RagdollOnOff()
    {
        IsRagdollEnabled = !IsRagdollEnabled;

        if (!IsRagdollEnabled)
        {
            transform.position = Vector3.zero;
        }

        SetRagdollMode(IsRagdollEnabled);
    }

}
