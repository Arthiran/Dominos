using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPSScript : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this, 1);
    }
}
