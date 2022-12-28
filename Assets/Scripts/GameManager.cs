using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Güvenliksiz kullaným örneðidir.
    public Transform PlayerTransform;

    private void Awake()
    {
        instance = this;
    }


}
