using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health ph;
    [SerializeField] private Image totalHealthImage;
    [SerializeField] private Image currentHealthImage;
    void Start()
    {
        totalHealthImage.fillAmount=ph.currentHealth/10;
    }

    
    void Update()
    {
        currentHealthImage.fillAmount=ph.currentHealth/10;
    }
}
