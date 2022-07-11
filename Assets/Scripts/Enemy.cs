using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;

    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private LayerMask shootLayer;
    private Transform aimTransform;

    private bool canMoveRight = false;
    private bool isReloaded = false;
    private Atak attack;

    /******************************************************************************/


    private void Awake()
    {
        attack = GetComponent<Atak>();
        aimTransform = attack.GetFireTransform;
    }

    void Update()
    {
        EnemyAttack();

        CheckCanMoveRight();

        MoveToward();

        Aim();

    }

    private void Reload()
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }

    private void EnemyAttack()
    {
        if (attack.GetAmmo <= 0 && isReloaded == false)
        {
            Invoke(nameof(Reload), 5f);
            isReloaded = true;
        }


        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }


    private void MoveToward()
    {
        if (Aim() && attack.GetAmmo > 0)
        {
            return;
        }

        if (!canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTargetL(movePoints[0].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTargetR(movePoints[1].position);
        }

    }

    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if (Vector3.Distance(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z)) <=0.1f)
        {
            canMoveRight= false;
        }
    }


    private bool Aim()
    {
        if (aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;

        }

        bool hit = Physics.Raycast(aimTransform.position, transform.right, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, transform.right * shootRange, Color.blue);
        return hit;
    }


    private void LookAtTheTargetL(Vector3 newTarget)
    {
        /*Vector3 newLookPosition = new Vector3(newTarget.x, transform.position.y, newTarget.z);

        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition - transform.position);*/
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 179.99f, 0f), speed * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
    private void LookAtTheTargetR(Vector3 newTarget)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed * Time.deltaTime);
    }

}
