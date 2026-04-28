using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GaikotuMotion : MonoBehaviour
{
    private Animator anim;
    public CharacterStats gaikotu;
    public InGameSw ingame;
    public AudioClip sound1;
    AudioSource audioSource;
    private int BreakPoint;
    private int ShieldCount;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        ingame.AttackPoint = 0;
        anim.SetBool("isDeath", false);
        BreakPoint = 0;
        ShieldCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gaikotu.currentHP < gaikotu.previousHP)
        {
            if(gaikotu.currentHP <= 0f)
            {
                anim.SetTrigger("4_Death");
                anim.SetBool("isDeath", true);
            }
            else
            {
                anim.SetTrigger("3_Damaged");
            }
            gaikotu.previousHP = gaikotu.currentHP;
        }

        if (ingame.AttackPoint == 1)
        {
            anim.SetTrigger("2_Attack");
            audioSource.PlayOneShot(sound1);
            ingame.AttackPoint = 0;
        }
        else if(ingame.AttackPoint == 2)
        {
            anim.SetTrigger("2_Attack");
            audioSource.PlayOneShot(sound1);
            ShieldCount += 1;
            if(BreakPoint >= Random.Range(1, 6))
            {
                ingame.MamoriBreak();
                BreakPoint = 0;
            }

            if(ShieldCount >= 2)
            {
                BreakPoint += 1;
            }
            ingame.AttackPoint = 0;
        }
    }
}
