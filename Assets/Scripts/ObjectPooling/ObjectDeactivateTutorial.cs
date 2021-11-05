using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDeactivateTutorial : MonoBehaviour
{
    [HideInInspector]
    public BombManagerTutorial bombManager;
    public GameObject ExplosionPS; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (bombManager.isTutorial)
            {
                bombManager.StopSpawning();
            }
            else
            {
                bombManager.TutorialText.text = "Ouch, you would've lost!";
            }
            gameObject.SetActive(false);
        }

        Instantiate(ExplosionPS, transform.position, Quaternion.identity);
    }
}
