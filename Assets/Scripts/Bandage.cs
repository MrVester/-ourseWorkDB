using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandage : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public bool isCooldown;

    public Image bandageImage;
    private Player player;


    void Start()
    {
        bandageImage = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<Player>();
        isCooldown = false;
    }


    void Update()
    {
        if (isCooldown)
        {
            bandageImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (bandageImage.fillAmount <= 0)
            {
                bandageImage.fillAmount = 1;
                isCooldown = false;
            }
        }
    }

    public static bool isCoolDown
    {
        get { return isCoolDown; }
        set
        {
            isCoolDown = value;
        }
    }
}
