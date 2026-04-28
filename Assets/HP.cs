using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Slider PlayerhpGauge;   //player残りHPゲージ
    public Slider EnemyhpGauge;   //enemy残りHPゲージ

    public CharacterStats Player;
    public CharacterStats Enemy;

    void Start()
    {
        // HPゲージの初期値を設定
        PlayerhpGauge.value = 1f;
        EnemyhpGauge.value = 1f;
    }

    void Update()
    {
        if (Enemy.currentHP < Enemy.previousHP)
        {
            Debug.Log("がいこつHP " + Enemy.currentHP);
            EnemyhpGauge.value = Enemy.currentHP / Enemy.maxHP;  // ゲージの値を更新
        }

        if (Player.currentHP != Player.previousHP)
        {
            Debug.Log("千歳君HP " + Player.currentHP);
            PlayerhpGauge.value = Player.currentHP / Player.maxHP;  // ゲージの値を更新
            Player.previousHP = Player.currentHP;
        }
    }
}