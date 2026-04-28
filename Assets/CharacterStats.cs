using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float previousHP;
    public BattleMessageUI battleMessageUI; // メッセージUIへの参照
    public bool DefencePoint;

    // Start is called before the first frame update
    void Start()
    {
        DefencePoint = false;
        gameObject.SetActive(true);
        currentHP = maxHP;
        previousHP = currentHP;
    }

    void Update()
    {

    }

    public void ProtectDamage()
    {
        DefencePoint = true;
        string msg = $"{gameObject.name}はガードしている！";
        UnityEngine.Debug.Log(msg);

        if (battleMessageUI != null)
        {
            battleMessageUI.ShowMessage(msg);
        }
    }

    public void TakeDamage(float damage)
    {
        string msg = $"{gameObject.name} は {damage} ダメージを 受けた！";
        if (DefencePoint == false)
        {
            currentHP -= damage;
            currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
            UnityEngine.Debug.Log(msg);
        }
        else if (DefencePoint == true)
        {
            currentHP += 10;
            currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
            msg = $"{gameObject.name}はダメージを防いだ！10回復！";
            UnityEngine.Debug.Log(msg);
        }
        else
        {
            UnityEngine.Debug.Log("Error");
        }

        if (battleMessageUI != null)
        {
            battleMessageUI.ShowMessage(msg);
        }

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        string msg = $"{gameObject.name}は倒された！";
        UnityEngine.Debug.Log(msg);

        if (battleMessageUI != null)
        {
            battleMessageUI.ShowMessage(msg);
        }
    }
}
