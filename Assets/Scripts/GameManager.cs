using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // G�venliksiz kullan�m �rne�idir.
    public Transform PlayerTransform;

    private void Awake()
    {
        instance = this;
    }


}
