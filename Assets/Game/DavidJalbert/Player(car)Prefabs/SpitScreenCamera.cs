using DavidJalbert;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class SpitScreenCamera : MonoBehaviour
{
    private Camera cam;
    private int index;
    private int totalPlayers;


    private void Awake()
    {
       PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoined;
    }
    private void Start()
    {
        index = GetComponent<PlayerInput>().playerIndex;
        totalPlayers = PlayerInput.all.Count;
        cam = GetComponent<Camera>();
        cam.depth = index;

        SetupCamera();

    }
  

    private void HandlePlayerJoined(PlayerInput obj)
    {
        totalPlayers = PlayerInput.all.Count;
        SetupCamera();

    }

    private void SetupCamera()
    {
        if (totalPlayers == 1)
        {
            cam.rect = new Rect(0, 0, 1, 1);
        }
        else if (totalPlayers == 2)
        {
            cam.rect = new Rect(index == 0 ? 0 : 0.5f, 0, 0.5f, 1);
        }
        else if (totalPlayers == 3)
        {
            cam.rect = new Rect(index == 0 ? 0 : (index == 1 ? 0.5f : 0), index < 2 ? 0 : 0.5f, 0.5f, 0.5f);
        }
        else if (totalPlayers == 4)
        {
            cam.rect = new Rect(index % 2 * 0.5f, index < 2 ? 0 : 0.5f, 0.5f, 0.5f);
        }
    }
}
