using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Güvenliksiz kullanım örneğidir.
    public Transform PlayerTransform;

    private void Awake()
    {
        instance = this;
    }


}
