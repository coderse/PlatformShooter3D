using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthFill;
    public Image ammoFill;

    private Atak playerAmmo;
    private Target playerHealth;

    /******************************************************************************/


    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Atak>();
        playerHealth = playerAmmo.GetComponent<Target>();
    }

    void Update()
    {
        UpdateHealthFill();
        UpdateAmmoFill();
    }

    private void UpdateAmmoFill()
    {
        ammoFill.fillAmount = (float)playerAmmo.GetAmmo / playerAmmo.GetClipSize;
    }

    private void UpdateHealthFill()
    {
        healthFill.fillAmount = (float)playerHealth.GetHealth / playerHealth.GetMaxHealt;
    }
}
