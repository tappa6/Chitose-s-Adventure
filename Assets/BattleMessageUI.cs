using TMPro;
using UnityEngine;

public class BattleMessageUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    void Start()
    {
        
    }

    // メッセージを表示
    public void ShowMessage(string message)
    {
        ClearMessage();
        messageText.text = message;
    }

    // メッセージを消す
    public void ClearMessage()
    {
        messageText.text = "";
    }
}
