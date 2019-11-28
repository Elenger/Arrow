using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
   
    [SerializeField] private GameObject p_ArrowPrefab;
    [SerializeField] private float p_ForcePower = 15f;
    private CharacterController p_characterController;
    private void Start()
    {
        p_characterController = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject newArrow = Instantiate(p_ArrowPrefab) as GameObject;
            newArrow.GetComponent<Arrow>().characterController = p_characterController;
            newArrow.transform.position = transform.position;
            newArrow.transform.rotation = transform.rotation;
       
            Shot(newArrow);
        }
    }


    private void Shot(GameObject newArrow)
    {
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        if (p_characterController.facingRight)
        {
            rb.AddForce(newArrow.transform.right * p_ForcePower, ForceMode2D.Impulse);
        }
        else
        {
            Vector3 theScale = newArrow.transform.localScale;
            theScale.x *= -1;
            newArrow.transform.localScale = theScale;
            rb.AddForce(newArrow.transform.right * -p_ForcePower, ForceMode2D.Impulse);
        }
    }
}
