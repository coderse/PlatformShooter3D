using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Healt Settings")]
    public bool healthPowerUp=false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] Vector3 turnVector = Vector3.zero;
    [Header("Sclae Settings")]
    [SerializeField] private float period = 2f;
    [SerializeField] Vector3 scaleVector;
    [SerializeField] private AudioClip clipToPlay;

    private float scaleFactor;

    private Vector3 startScale;


    /******************************************************************************/



    private void Awake()
    {
        startScale = transform.localScale;

    }

    void Start()
    {

        if (healthPowerUp && ammoPowerUp)
        {
            healthPowerUp = false;
            ammoPowerUp = false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp = false;
        }
    }

    void Update()
    {
        transform.Rotate(turnVector);
        SinusWawe();
    }

    private void SinusWawe()
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }

        float cycles = Time.timeSinceLevelLoad / period;

        const float piX2 = Mathf.PI * 2;
        float sinusWawe = Mathf.Sin(cycles * piX2);
        scaleFactor = sinusWawe / 2 + 0.5f;
        Vector3 offset = scaleFactor * scaleVector;

        transform.localScale = startScale + offset;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        AudioSource.PlayClipAtPoint(clipToPlay, transform.position);        

        if (healthPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
        }
        else if (ammoPowerUp)
        {
            other.gameObject.GetComponent<Atak>().GetAmmo += ammoAmount;
        }

        Destroy(gameObject);
    }

}
