using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public static Tips instance;
    private Text t;
    public bool isItr;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
    }
    public void SetTips(string tips)
    {
        if (isItr)
        {
            return;
        }
        t = this.GetComponent<Text>();
        t.text = tips;
        Invoke("init", 1.5f);
    }

    private void init()
    {
        Debug.Log(t.text);
        t.text = null;
    }
}
