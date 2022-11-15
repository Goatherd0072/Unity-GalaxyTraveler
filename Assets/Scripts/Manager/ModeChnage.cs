using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChnage : MonoBehaviour
{
    public static ModeChnage instance;
    public GameObject ShipCam;
    public GameObject ItroCam;
    public GameObject ShipUI;
    public GameObject ItroUI;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Planet;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToIntro(Material m, string name)
    {
        Time.timeScale = 0;
        ShipCam.SetActive(false);
        ItroCam.SetActive(true);
        ShipUI.SetActive(false);
        ItroUI.SetActive(true);
        Planet.SetActive(true);
        Light1.SetActive(true);
        Light2.SetActive(false);
        Planet.GetComponent<Renderer>().material = m;
        if (transform.GetComponent<LoadFile>() == null)
        {
            transform.gameObject.AddComponent<LoadFile>();
        }
        transform.GetComponent<LoadFile>().mFunLoadFile(name);
        Tips.instance.SetTips("");
        Tips.instance.isItr = true;
    }
    public void ChangeToShip()
    {
        Time.timeScale = 1;
        ShipCam.SetActive(true);
        ItroCam.SetActive(false);
        ShipUI.SetActive(true);
        ItroUI.SetActive(false);
        Planet.SetActive(false);
        Light1.SetActive(false);
        Light2.SetActive(true);
        Tips.instance.isItr = false;

    }
}
