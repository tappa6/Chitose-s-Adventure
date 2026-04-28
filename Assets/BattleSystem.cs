using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public CharacterStats player;
    public CharacterStats enemy;

    void Update()
    {

    }

    public void Attack1(CharacterStats attacker, CharacterStats target)
    {
        float damage = Random.Range(20, 51);
        target.TakeDamage(damage);
    }

    public void Attack2(CharacterStats attacker, CharacterStats target)
    {
        float damage = Random.Range(45, 61);
        target.TakeDamage(damage);
    }
    public void Skill(CharacterStats attacker, CharacterStats target)
    {
        float damage = Random.Range(61, 81);
        target.TakeDamage(damage);
    }

    public void Defence(CharacterStats defender)
    {
        defender.ProtectDamage();
    }
}
