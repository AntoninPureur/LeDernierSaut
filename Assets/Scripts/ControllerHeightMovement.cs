using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class ControllerHeightMovement : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject SidewayDirection;
    [SerializeField] private GameObject ForwardDirection;

    //Vector3 Positions
    [SerializeField] private Vector3 PositionPreviousFrameLeftHand;
    [SerializeField] private Vector3 PositionPreviousFrameRightHand;
    [SerializeField] private Vector3 PlayerPositionPreviousFrame;
    [SerializeField] private Vector3 PlayerPositionCurrentFrame;
    [SerializeField] private Vector3 PositionCurrentFrameLeftHand;
    [SerializeField] private Vector3 PositionCurrentFrameRightHand;
    [SerializeField] private double XDistanceHands;
    [SerializeField] private double YDistanceHands;
    [SerializeField] private bool IsGrounded;

    //Speed
    [SerializeField] private float ForwardSpeed = 10;
    [SerializeField] private float BackwardSpeed = 10;
    [SerializeField] private float SidewardSpeed = 6;

    //x Distance where the movement is neither forward or backward
    [SerializeField] private double RefXDistance = 0.15;

    void Start()
    {
       
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);

        PlayerPositionPreviousFrame = transform.position; //set current positions
        PositionPreviousFrameLeftHand = LeftHand.transform.position; //set previous positions
        PositionPreviousFrameRightHand = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);

        // get forward direction from the center eye camera and set it to the forward direction object
        float yRotation = MainCamera.transform.eulerAngles.y;
        SidewayDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);
        ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        // get positons of hands
        PositionCurrentFrameLeftHand = LeftHand.transform.position;
        PositionCurrentFrameRightHand = RightHand.transform.position;

        // position of player
        PlayerPositionCurrentFrame = transform.position;

        // get distances between controllers
        var yDistanceHands = PositionCurrentFrameLeftHand.y - PositionCurrentFrameRightHand.y;
        var xDistanceHands =Abs(PositionCurrentFrameLeftHand.x- PositionCurrentFrameRightHand.x);


        XDistanceHands = xDistanceHands;
        YDistanceHands = yDistanceHands;
        IsGrounded = isGrounded;



        if (Time.timeSinceLevelLoad > 1f)
        {
            transform.position += SidewayDirection.transform.right * yDistanceHands * SidewardSpeed * Time.deltaTime;
            if (xDistanceHands > RefXDistance+0.1) 
            {
                transform.position += -ForwardDirection.transform.forward * (float)(yDistanceHands-RefXDistance) * ForwardSpeed * Time.deltaTime;
            }
            if (xDistanceHands < RefXDistance - 0.1)
            {
               transform.position += ForwardDirection.transform.forward * (float)(yDistanceHands - RefXDistance) * BackwardSpeed * Time.deltaTime;
            }

            //
        }
        // set previous position of hands for next frame
        PositionPreviousFrameLeftHand = PositionCurrentFrameLeftHand;
        PositionPreviousFrameRightHand = PositionCurrentFrameRightHand;
        // set player position previous frame
        PlayerPositionPreviousFrame = PlayerPositionCurrentFrame;
    }
}

