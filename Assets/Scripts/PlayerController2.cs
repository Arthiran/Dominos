// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController2 : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    private SphereCollider SphereCol;

    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float RayDistanceGround = 0.016f;
    [SerializeField]
    private LayerMask OutOfBoundsMask;
    [SerializeField]
    private GameObject EndUI;
    [SerializeField]
    private TextMeshProUGUI WinLossText;

    private float VVelocity;
    private float HVelocity;

    private bool HasLost = false;

    [HideInInspector] public Vector3 currentForce = new Vector3(0,0,0);

    public Client thisClient;

    private bool isFinished = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SphereCol = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        // Checks if player is outside of tracks
        bool CheckOutOfBounds = IsOutOfBounds();

        // Checks if player officially lost and shows the end UI
        if (HasLost)
        {
            EndUI.SetActive(true);
            WinLossText.text = "GAME OVER";
            return;
        }

        // Gets player input in Update and sets variables which will be used in the FixedUpdate for physics
        VVelocity = Input.GetAxisRaw("Vertical");
        HVelocity = Input.GetAxisRaw("Horizontal");

        // Checks if Jump Key was pressed, sets CanJump to true, executes jump in FixedUpdate

        if (CheckOutOfBounds)
        {
            HasLost = true;
        }
    }

    private void FixedUpdate()
    {
        // Player Movement handles in FixedUpdate
        if (thisClient.playerNum == 2)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        // Uses torque to rotate the ball
        currentForce = new Vector3(VVelocity * MoveSpeed * 100, rb.velocity.y, -HVelocity * MoveSpeed * 100);
        rb.AddTorque(currentForce);
    }

    private bool IsOutOfBounds()
    {
        // Uses a raycast which checks if it's currently hitting the out of bounds layer mask, if true, player has lost
        if (Physics.Raycast(SphereCol.bounds.center, Vector3.down, SphereCol.bounds.extents.y + RayDistanceGround, OutOfBoundsMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player has passed the finishing line
        if (other.tag == "FinishLine" && !isFinished)
        {
            EndUI.SetActive(true);
            WinLossText.text = "YOU'VE FINISHED";
            thisClient.SendClientMessage("HS Finished");
            isFinished = true;
        }
    }
}
