using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This makes an object move randomly in a set of directions, with some random time delay in between each decision
/// </summary>
public class Wanderer : MonoBehaviour
{
    internal Transform thisTransform;
    public Animator animator;

    // The movement speed of the object
    public float moveSpeed = 0.2f;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] directionChoices = new[] { Vector3.right, 
                                                Vector3.left, 
                                                Vector3.up, 
                                                Vector3.down, 
                                                Vector3.zero}; // Stay in place

    internal int randomlySelectedDirection;

    // Use this for initialization
    private void Start()
    {
        // Cache the transform for quicker access
        thisTransform = transform;
        
        // Set a random time delay for taking a decision ( changing direction,or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    // Update is called once per frame
    private void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        randomlySelectedDirection = Mathf.FloorToInt(Random.Range(0, directionChoices.Length));
    }

    private void FixedUpdate()
    {
        // Move the object in the chosen direction at the set speed
        var direction = directionChoices[randomlySelectedDirection];
        var xDir = direction.x;
        var yDir = direction.y;

        thisTransform.position += direction * Time.deltaTime * moveSpeed;

        if (animator)
        {
            animator.SetFloat("MoveX", xDir);
            animator.SetFloat("MoveY", yDir);
        }

        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }
}