using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform [] rayStartPoint;

    private GameManager gameManager;

    /******************************************************************************/


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

    }


    void Update()
    {
        if (!gameManager.GetLevelFinish)
        {
            TakeInput();

        }

    }

    private void TakeInput()
    {
        if (Input.GetKey("d"))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime,0f,5f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,0f,0f), turnSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        }
        else if (Input.GetKey("a"))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 100) * Time.deltaTime,-5f,0f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0f,179.99f,0f), turnSpeed*Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }   
        else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }
        
        if (Input.GetKeyDown("space") && GroundCheck())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,Mathf.Clamp((jumpPower*100)*Time.deltaTime,0f,15f), 0f);
        }

    }

    private bool GroundCheck()
    {
        bool hit = false;

        for (int i = 0; i < rayStartPoint.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.6f);
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up * 0.6f, Color.red);

        }

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
