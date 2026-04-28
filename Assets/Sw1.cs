using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Sw1 : MonoBehaviour
{
    public Button bt;
    // Start is called before the first frame update
    void Start()
    {
        bt.onClick.AddListener(Scenesw1);
    }

    void Scenesw1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
