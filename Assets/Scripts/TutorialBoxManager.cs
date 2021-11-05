using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialBoxManager : MonoBehaviour
{
    public BombManagerTutorial bombManager;
    public TextMeshProUGUI TutorialText;
    public string Message;
    public bool DropBombs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DropBombs)
            {
                bombManager.CanSpawn = true;
            }
            TutorialText.text = Message;
        }

    }
}
