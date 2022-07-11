using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealt = 2;
    [SerializeField] private int maxAmmo= 5;
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject deadFx;
    [SerializeField] private AudioClip clipToPlay;

    private int currentHealth;

    /******************************************************************************/


    public int GetMaxHealt
    {
        get { return maxHealt; }
    }
  
    public int GetHealth
    {
        get
        {
            return currentHealth;
        }
        set 
        { 
            currentHealth = value;
            if (currentHealth > maxHealt)
            {
                currentHealth = maxHealt;
            }
        }

    }

    private void Awake()
    {
        currentHealth = maxHealt;

    }


    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)
        {

            if (bullet && bullet.owner != gameObject)
            {
                currentHealth--;
                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);

                if (hitFx != null && currentHealth > 0)
                {
                    Instantiate(hitFx, transform.position, Quaternion.identity);
                }

                if (currentHealth <= 0)
                {
                    Die();
                }
                Destroy(other.gameObject);
            }
        }
    }

    private void Die()
    {
        if (deadFx != null)
        {
            Instantiate(deadFx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
