using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Sw2 : MonoBehaviour
{
    public Button bt;
    // Start is called before the first frame update
    void Start()
    {
        bt.onClick.AddListener(Scenesw2);
    }

    void Scenesw2()
    {
        SceneManager.LoadScene("start");
    }
}