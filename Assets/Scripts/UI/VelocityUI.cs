using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityUI : MonoBehaviour
{
    public GameObject Velocity_H_P;
    public GameObject Velocity_H_N;

    public GameObject Velocity_V_P;
    public GameObject Velocity_V_N;

    private Vector3 ShipV;
    // Start is called before the first frame update
    void Start()
    {
        Velocity_H_P.SetActive(false);
        Velocity_H_N.SetActive(false);
        Velocity_V_P.SetActive(false);
        Velocity_V_N.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ShipV = SpaceShipController.instance.CalculateRelativeVelocity();
        Speed_UI();
    }

    private void Speed_UI()
    {
        if (ShipV.x > 0)
        {
            Velocity_H_P.SetActive(true);
            Velocity_H_N.SetActive(false);
        }
        else if (ShipV.x < 0)
        {
            Velocity_H_P.SetActive(false);
            Velocity_H_N.SetActive(true);
        }
        else
        {
            Velocity_H_P.SetActive(false);
            Velocity_H_N.SetActive(false);
        }

        if (ShipV.y > 0)
        {
            Velocity_V_P.SetActive(true);
            Velocity_V_N.SetActive(false);
        }
        else if (ShipV.y < 0)
        {
            Velocity_V_P.SetActive(false);
            Velocity_V_N.SetActive(true);
        }
        else
        {
            Velocity_V_P.SetActive(false);
            Velocity_V_N.SetActive(false);
        }

    }
}
