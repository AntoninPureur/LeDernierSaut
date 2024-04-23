using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class AerialMovement : MonoBehaviour
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
    [SerializeField] private float BackwardSpeed = 15;
    [SerializeField] private float SidewardSpeed = 8;

    //x Distance where the movement is neither forward or backward
    [SerializeField] private double RefXDistance = 0.35;

    void Start()
    {



        PlayerPositionPreviousFrame = transform.position; //set current positions
        PositionPreviousFrameLeftHand = LeftHand.transform.position; //set previous positions
        PositionPreviousFrameRightHand = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        bool isGrounded = !Physics.Raycast(new Vector2(transform.position.x,transform.position.y+2.0f),Vector3.down,2.0f);
        IsGrounded = isGrounded;

        // get forward direction from the center eye camera and set it to the forward direction object
        float yRotation = MainCamera.transform.eulerAngles.y;
        SidewayDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);
        ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        // get positons of hands
        PositionCurrentFrameLeftHand = LeftHand.transform.position;
        PositionCurrentFrameRightHand = RightHand.transform.position;

        // position of player
        PlayerPositionCurrentFrame = transform.position;

        // get vertical distances between controllers
        var yDistanceHands = PositionCurrentFrameLeftHand.y - PositionCurrentFrameRightHand.y;
        
        // get horizontal distance between controllers
        Vector2 horizontalPositionLeftHands = new Vector2(PositionCurrentFrameLeftHand.x, PositionCurrentFrameLeftHand.z);
        Vector2 horizontalPositionRightHands = new Vector2(PositionCurrentFrameRightHand.x, PositionCurrentFrameRightHand.z);
        var horizontalDistanceHands = Vector2.Distance(horizontalPositionLeftHands, horizontalPositionRightHands);


        //XDistanceHands = xDistanceHands;
        YDistanceHands = yDistanceHands;
        IsGrounded = isGrounded;


        if (!IsGrounded) { 
            if (Time.timeSinceLevelLoad > 1f)
            {
                transform.position += SidewayDirection.transform.right * yDistanceHands * SidewardSpeed * Time.deltaTime;
                if (horizontalDistanceHands > RefXDistance + 0.1)
                {
                    transform.position += ForwardDirection.transform.forward * (float)(horizontalDistanceHands - RefXDistance) * ForwardSpeed * Time.deltaTime;
                }
                if (horizontalDistanceHands < RefXDistance - 0.1)
                {
                    transform.position += ForwardDirection.transform.forward * (float)(horizontalDistanceHands - RefXDistance) * BackwardSpeed * Time.deltaTime;
                }

            //
            }
        }
        // set previous position of hands for next frame
        PositionPreviousFrameLeftHand = PositionCurrentFrameLeftHand;
        PositionPreviousFrameRightHand = PositionCurrentFrameRightHand;
        // set player position previous frame
        PlayerPositionPreviousFrame = PlayerPositionCurrentFrame;
    }
}

