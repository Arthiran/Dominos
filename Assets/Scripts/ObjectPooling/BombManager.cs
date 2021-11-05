using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    [SerializeField]
    private GameObject BombPrefab;
    private List<GameObject> BombPool = new List<GameObject>();

    [SerializeField]
    private int numBombs;

    [SerializeField]
    private Vector2 BombOffset;

    [SerializeField]
    private float BombSpawnTimer;

    private int CurrentDirection = 0;

    private bool SpawnBombs = true;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        for (int i = 0; i < numBombs; i++)
        {
            GameObject SpawnedBomb = Instantiate(BombPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            SpawnedBomb.GetComponent<ObjectDeactivate>().bombManager = this;
            SpawnedBomb.SetActive(false);
            BombPool.Add(SpawnedBomb);
        }

        StartCoroutine(SpawnBombOnTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentDirection = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CurrentDirection = -1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CurrentDirection = -2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CurrentDirection = 2;
        }
    }

    private void SpawnBomb()
    {
        Vector3 NewBombOffset = new Vector3(0, 0, 0);

        if (CurrentDirection == 1)
        {
            NewBombOffset = new Vector3(0, BombOffset.x, BombOffset.y);
        }
        else if (CurrentDirection == -1)
        {
            NewBombOffset = new Vector3(0, BombOffset.x, -BombOffset.y);
        }
        else if (CurrentDirection == 2)
        {
            NewBombOffset = new Vector3(BombOffset.y, BombOffset.x, 0);
        }
        else if (CurrentDirection == -2)
        {
            NewBombOffset = new Vector3(-BombOffset.y, BombOffset.x, 0);
        }
        else
        {
            NewBombOffset = new Vector3(0, BombOffset.x, BombOffset.y);
        }

        GameObject CurrentBomb;
        CurrentBomb = BombPool[0];
        BombPool.RemoveAt(0);
        BombPool.Add(CurrentBomb);
        CurrentBomb.SetActive(true);
        CurrentBomb.transform.SetPositionAndRotation(transform.position + NewBombOffset, Quaternion.Euler(-90, 0, 0));
    }

    private IEnumerator SpawnBombOnTimer()
    {
        yield return new WaitForSeconds(BombSpawnTimer);

        if (SpawnBombs)
        {
            SpawnBomb();
            StartCoroutine(SpawnBombOnTimer());
        }
    }

    public void StopSpawning()
    {
        playerController.HitByBomb();
        SpawnBombs = false;
    }
}
