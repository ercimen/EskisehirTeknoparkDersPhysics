using UnityEngine;

public class CL_PointDrawer : MonoBehaviour
{
    [Header("General Variables")]
    public Color gizmosColor = Color.blue;
    public float range = 0.5f;
    public Vector3 offset;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position+offset, range);

    } // OnDrawGizmos()

} // class
