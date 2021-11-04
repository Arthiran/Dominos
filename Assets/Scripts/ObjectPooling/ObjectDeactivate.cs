using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivate : MonoBehaviour
{
    [HideInInspector]
    public BombManager bombManager;

    public GameObject ExplosionPS;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            bombManager.StopSpawning();
            gameObject.SetActive(false);
        }

        Instantiate(ExplosionPS, transform.position, Quaternion.identity);
    }
}
