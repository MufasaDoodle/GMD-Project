using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject slashGameObject;

    public float swingCooldown = 1.5f;

    public AudioClip[] swingClips;

    // Update is called once per frame
    void Update()
    {
        swingCooldown -= Time.deltaTime;
        swingCooldown = Mathf.Clamp(swingCooldown, 0f, 2f);

        if (Input.GetMouseButtonDown(0) && swingCooldown <= 0)
        {
            Attack();
            swingCooldown = 1.5f;
        }
    }

    void Attack()
    {
        slashGameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        PlaySwingSound();
    }

    public void SetAttackingBool(bool state)
    {
        GetComponent<Animator>().SetBool("Attacking", state);
    }

    public void PlaySwingSound()
    {
        int random = Random.Range(0, swingClips.Length-1);
        GetComponent<AudioSource>().PlayOneShot(swingClips[random]);
    }

    void SetAnimationRotation()
    {

    }
}
