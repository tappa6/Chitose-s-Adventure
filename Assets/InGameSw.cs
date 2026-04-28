using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections;
using System.Runtime.InteropServices;

public class InGameSw : MonoBehaviour
{
    //Stats
    public CharacterStats player;
    public CharacterStats enemy;

    //BattleSystem
    public BattleSystem battleSystem;

    //Button
    public Button attack;
    public Button defence;
    public Button skill;
    public Button retry;

    //Canvas
    public Canvas dialogUI;

    //Animator
    Animator animator;

    //fields
    public int AttackPoint;

    //BattleMessageUI
    public BattleMessageUI battleMessageUI; // メッセージUIへの参照

    //Audio
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    //Coroutine
    private Coroutine routine;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        dialogUI.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        attack.onClick.AddListener(Kougeki);
        defence.onClick.AddListener(Mamori);
        skill.onClick.AddListener(HissatuwazaManager);
    }

    private void Kougeki()
    {
        ObjectOff();
        dialogUI.gameObject.SetActive(true);
        animator.SetTrigger("Attack1");
        audioSource.PlayOneShot(sound1);
        battleSystem.Attack(player, enemy);
        if(routine != null) 
        {
            routine = null;
        }
        routine = StartCoroutine(ObjectOn());
    }

    private void Mamori()
    {
        ObjectOff();
        dialogUI.gameObject.SetActive(true);
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
        battleSystem.Defence(player);
        if(routine != null) 
        {
            routine = null;
        }
        routine = StartCoroutine(ObjectOn());
    }

    private void HissatuwazaManager()
    {
        ObjectOff();
        dialogUI.gameObject.SetActive(true);

        int successRate = 400;

        if(routine != null) 
        {
            routine = null;
        }
        routine = StartCoroutine(Hissatuwaza(successRate));
    }

    private IEnumerator Hissatuwaza(int successRate)
    {
        bool skillHit = false;
        if(successRate >= Random.Range(0,1001))
        {
            skillHit = true;
        }
        
        if(skillHit)
        {
            string msg = "必殺技の発動に成功！大ダメージだ！";
            UnityEngine.Debug.Log(msg);

            if (battleMessageUI != null)
            {
                battleMessageUI.ShowMessage(msg);
            }
            animator.SetTrigger("Attack2");
            audioSource.PlayOneShot(sound2);
            yield return new WaitForSeconds(1f);
            battleSystem.Skill(player, enemy);
        }
        else
        {
            string msg = "必殺技の発動に失敗した。";
            UnityEngine.Debug.Log(msg);

            if (battleMessageUI != null)
            {
                battleMessageUI.ShowMessage(msg);
            }
        }
        if(routine != null) 
        {
            routine = null;
        }
        routine = StartCoroutine(ObjectOn());
    }

    public void MamoriBreak()
    {
        player.DefencePoint = false;
        animator.SetBool("IdleBlock", false);
        string msg = "ガードをはがされた！";
        UnityEngine.Debug.Log(msg);

        if (battleMessageUI != null)
        {
            battleMessageUI.ShowMessage(msg);
        }
    }

    private void ObjectOff()
    {
        attack.gameObject.SetActive(false);
        defence.gameObject.SetActive(false);
        skill.gameObject.SetActive(false);
    }

    private IEnumerator ObjectOn()
    {
        yield return new WaitForSeconds(2f);
        if (enemy.currentHP <= 0f)
        {
            SceneManager.LoadScene("goal");
            yield return null;
        }

        if (player.DefencePoint)
        {
            AttackPoint = 2;
            yield return new WaitForSeconds(0.5f);
            battleSystem.Skill(enemy, player);
        }
        else
        {
            AttackPoint = 1;
            battleSystem.Attack(enemy, player);
        }

        if (player.DefencePoint == false)
        {
            animator.SetTrigger("Hurt");
        }
        player.DefencePoint = false;

        if (player.currentHP <= 0f)
        {
            gameObject.SetActive(true);
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(1f);
            string msg = "ゲームオーバー";
            UnityEngine.Debug.Log(msg);

            if (battleMessageUI != null)
            {
                battleMessageUI.ShowMessage(msg);
            }
            retry.gameObject.SetActive(true);
            retry.onClick.AddListener(ReStart);
            yield break;
        }
        yield return new WaitForSeconds(1f);
        dialogUI.gameObject.SetActive(false);
        animator.SetBool("IdleBlock", false);
        attack.gameObject.SetActive(true);
        defence.gameObject.SetActive(true);
        skill.gameObject.SetActive(true);
    }
    

    // Update is called once per frame
    private void Update()
    {

    }

    private void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
