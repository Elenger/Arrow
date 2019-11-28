using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool arrowFromTree = false;
    public CharacterController characterController;
    private int p_RockLayerNumber = 9;
    public Transform treeTransform;

    private void Start()
    {
        arrowFromTree = characterController.playerOnTheTree;
        treeTransform = characterController.treeTransform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.layer == p_RockLayerNumber)
        {
            var sticky = gameObject.AddComponent<FixedJoint2D>();
            sticky.connectedBody = collision.rigidbody;
        }
    }
}
