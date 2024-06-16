using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    public Image cooldownImage;
    public TMP_Text cooldownText;
    public static float cooldownTime = 5;
    public static float cooldownTimer;
    public static bool isCooldown;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    public void ApplyCooldown()
    {
        if (cooldownTimer == cooldownTime)
        {
            cooldownText.gameObject.SetActive(true);
            cooldownImage.gameObject.SetActive(true);
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
        {
            isCooldown = false;
            cooldownText.gameObject.SetActive(false);
            cooldownImage.gameObject.SetActive(false);
            AimJoystick.canvasGroup.blocksRaycasts = true;
        }

        else
        {
            cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString();
            cooldownImage.fillAmount = cooldownTimer / cooldownTime;
        }
    }
}
