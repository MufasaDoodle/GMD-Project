using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    bool attacking = false;

    public void SetMouseLookingBool(int isAttacking)
    {
        attacking = Convert.ToBoolean(isAttacking);
    }

    // Start is called before the first frame update
    void Start()
    {

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
    }
}
