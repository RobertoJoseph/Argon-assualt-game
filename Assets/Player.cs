﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    [Tooltip("In m*s^-1")] [SerializeField] float Speed = 90;
    [Tooltip(" In m")] [SerializeField] float xRange = 140f;
    [Tooltip(" In m")] [SerializeField] float yRange = 70f;
    [SerializeField] float PositionPitchFactor = -0.242f;
    [SerializeField] float PositionYawFactor = 0.4f;
    [SerializeField] float ControlPitchFactor = -20f;
    [SerializeField] float ControlRollFactor = -20f;
    float xThrow;
    float yThrow;



    // Start is called before the first frame update
    void Start()
    {
       
    }

  

    // Update is called once per frame
    void Update()
    {
       ProcessMovement();
       ProcessRotation();

    }

    private void ProcessMovement()
    {

         xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
         yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewyPos = transform.localPosition.y + yOffset;

        float clampXPost = Mathf.Clamp(rawNewXPos, -xRange, xRange); //bt5ly el ship tt7rk f range mn kza l kza
        float clampYPost = Mathf.Clamp(rawNewyPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampXPost, clampYPost, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * PositionPitchFactor;
        float pitchDueToControl = yThrow * ControlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;  

        float yawDueToPosition= transform.localPosition.x * PositionYawFactor;
        float yaw = yawDueToPosition;

        float rollDueToControl = xThrow * ControlRollFactor;
        float roll = rollDueToControl;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); // (Pitch , Yaw , Roll)
    }

}