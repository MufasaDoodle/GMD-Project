using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    float raycastDepth = 100.0f;
    LayerMask floorLayerMask;
    string rayCastLayerName = "RayCastLayer";

    bool attacking = false;

    public void SetMouseLookingBool(int isAttacking)
    {
        Debug.Log($"Setting attacking to: {Convert.ToBoolean(isAttacking)}");
        attacking = Convert.ToBoolean(isAttacking);
    }

    // Start is called before the first frame update
    void Start()
    {
        floorLayerMask = LayerMask.GetMask(rayCastLayerName);
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == false)
            LookAtMousePosition();
    }

    private void LookAtMousePosition()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 285f, Vector3.forward);

        /*
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.01f;

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit[] mouseHits = Physics.RaycastAll(mouseRay, raycastDepth, floorLayerMask);

        if(mouseHits.Length == 1)
        {
            Debug.Log("hit!");
            Vector3 hitPosition = mouseHits[0].point;

            Vector3 localHitPosition = transform.InverseTransformPoint(hitPosition);
            localHitPosition.y = 0;
            Vector3 lookPosition = transform.TransformPoint(localHitPosition);

            transform.LookAt(lookPosition, transform.up);
        }
        */
    }
}
