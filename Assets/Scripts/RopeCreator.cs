using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreator : MonoBehaviour
{
    [SerializeField] private GameObject p_RopePrefab;
    private int p_NumberArrowLayer = 10;
    private bool p_ArrowFromTree = false;
    private Transform p_TreeTransform;
    private Transform p_ArrowTrasform;
    private Vector3 p_RopePosition;

    void Update()
    {
        if (p_ArrowFromTree)
        {
            RopeCreate();
            p_ArrowFromTree = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == p_NumberArrowLayer)
        {
            p_ArrowFromTree = collision.gameObject.GetComponent<Arrow>().arrowFromTree;
            p_ArrowTrasform = collision.gameObject.GetComponent<Arrow>().transform;
            p_TreeTransform = collision.gameObject.GetComponent<Arrow>().treeTransform;
        }
    }

    private void RopeCreate()
    {
        p_RopePosition = RopePosition();
        float lenghtRope = (p_ArrowTrasform.position - p_TreeTransform.position).magnitude + 1;
        GameObject newRope = Instantiate(p_RopePrefab) as GameObject;
        newRope.transform.position = p_RopePosition;
        Vector3 scaleRope = newRope.transform.localScale;
        scaleRope.x = lenghtRope;
        newRope.transform.localScale = scaleRope;

        float angleRad = Mathf.Atan2(p_ArrowTrasform.position.y - p_TreeTransform.position.y, p_ArrowTrasform.position.x - p_TreeTransform.position.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        Quaternion angleRotation = new Quaternion();
        angleRotation.eulerAngles = new Vector3(0f,0f, angleDeg);
        newRope.transform.rotation = angleRotation;
        // Quaternion rotationRope = Quaternion.LookRotation(p_ArrowTrasform.position - p_TreeTransform.position);
    }

    private Vector3 RopePosition()
    {
        float x = (p_TreeTransform.position.x + p_ArrowTrasform.position.x) / 2 + 0.5f;
        float y = (p_TreeTransform.position.y + p_ArrowTrasform.position.y) / 2;
        float z = (p_TreeTransform.position.z + p_ArrowTrasform.position.z) / 2;
        return p_RopePosition = new Vector3(x, y, z);
    }
}
