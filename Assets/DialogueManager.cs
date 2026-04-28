using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueEvent
    {
        public string[] sentences;  // セリフ
        public Sprite[] characterSprites; // 変更するキャラ画像
        public bool[] isChCharacter; // 主人公キャラかどうか (true:左, false:右)
    }

    public TMP_Text dialogueText;  // テキスト表示
    public Image ChCharacterImage;  // 主人公キャラの画像
    public Image EnCharacterImage; // 敵キャラの画像
    public Button startButton; // 開始ボタン
    public Button nextButton; // 次のセリフボタン
    public GameObject dialogueUI; // UI全体

    public DialogueEvent[] dialogueEvents; // 会話データ
    private int currentDialogueIndex = 0; // 現在の会話イベント
    private int currentSentenceIndex = 0; // 現在のセリフ
    private bool isTyping = false; // 文字表示中フラグ

    void Start()
    {
        dialogueUI.SetActive(false);
        startButton.onClick.AddListener(StartDialogue);
        nextButton.onClick.AddListener(NextSentence);
    }

    void StartDialogue()
    {
        dialogueUI.SetActive(true);
        startButton.gameObject.SetActive(false);
        currentDialogueIndex = 0;
        currentSentenceIndex = 0;
        nextButton.gameObject.SetActive(true);
        StartCoroutine(TypeSentence(dialogueEvents[currentDialogueIndex].sentences[currentSentenceIndex]));
        UpdateCharacterImage();
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        // **\n を正しく改行に変換**
        sentence = sentence.Replace("\\n", "\n");

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 文字送り速度
        }

        isTyping = false;
    }

    void NextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueEvents[currentDialogueIndex].sentences[currentSentenceIndex].Replace("\\n", "\n");
            isTyping = false;
        }
        else
        {
            currentSentenceIndex++;

            if (currentSentenceIndex < dialogueEvents[currentDialogueIndex].sentences.Length)
            {
                StartCoroutine(TypeSentence(dialogueEvents[currentDialogueIndex].sentences[currentSentenceIndex]));
                UpdateCharacterImage();
            }
            else
            {
                currentSentenceIndex = 0;
                currentDialogueIndex++;

                if (currentDialogueIndex < dialogueEvents.Length)
                {
                    StartCoroutine(TypeSentence(dialogueEvents[currentDialogueIndex].sentences[currentSentenceIndex]));
                    UpdateCharacterImage();
                }
                else
                {
                    CloseDialogue();
                }
            }
        }
    }

    void UpdateCharacterImage()
    {
        // インデックスが範囲内かチェック
        if (currentDialogueIndex < dialogueEvents.Length && currentSentenceIndex < dialogueEvents[currentDialogueIndex].isChCharacter.Length)
        {
            bool isLeft = dialogueEvents[currentDialogueIndex].isChCharacter[currentSentenceIndex];
            Sprite charSprite = dialogueEvents[currentDialogueIndex].characterSprites[currentSentenceIndex];

            if (isLeft)
            {
                ChCharacterImage.sprite = charSprite;
                ChCharacterImage.color = new Color(1f, 1f, 1f, 1f); // 主人公を明るく
                EnCharacterImage.color = new Color(1f, 1f, 1f, 0.5f); // 敵を薄く
            }
            else
            {
                EnCharacterImage.sprite = charSprite;
                EnCharacterImage.color = new Color(1f, 1f, 1f, 1f); // 敵を明るく
                ChCharacterImage.color = new Color(1f, 1f, 1f, 0.5f); // 主人公を薄く
            }
        }
    }

    void CloseDialogue()
    {
        dialogueUI.SetActive(false);
        startButton.gameObject.SetActive(true);
    }
}