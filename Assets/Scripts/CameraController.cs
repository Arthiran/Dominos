// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private float SmoothSpeed = 0.05f;
    [SerializeField]
    private float RotateSpeed = 1f;

    private Vector3 Offset;

    private void Awake()
    {
        // Sets offset of camera
        Offset = transform.position;
    }

    private void FixedUpdate()
    {
        // Smoothly changes camera position using Lerp
        LerpCamera(Player.transform.position, SmoothSpeed);
    }

    private void LerpCamera(Vector3 newPos, float newSmooth)
    {
        // Target position is the player position + offset
        Vector3 targetPos = newPos + Offset;
        // Smoothed posiiton is the lerped value of current camera position to the target position
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, newSmooth);
        transform.position = smoothedPos;
    }
}
