using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dagger : MonoBehaviour
{
    public float cooldown;
    [HideInInspector]public bool isCooldown;

    public Image daggerImage;
    private Player player;


    void Start()
    {
        daggerImage = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<Player>();
        isCooldown = false;
    }


    void Update()
    {
        if (isCooldown)
        {
            daggerImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (daggerImage.fillAmount <= 0)
            {
                daggerImage.fillAmount = 1;
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
    