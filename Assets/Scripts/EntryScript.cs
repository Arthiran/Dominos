// Aaron Chan - 100657311, Arthiran Sivarajah - 100660300

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntryScript : MonoBehaviour
{
    public Text serverIP;

    public void GetServerIP()
    {
        PlayerPrefs.SetString("ServerIP", serverIP.text);
        SceneManager.LoadScene("GameScene");
    }
}
