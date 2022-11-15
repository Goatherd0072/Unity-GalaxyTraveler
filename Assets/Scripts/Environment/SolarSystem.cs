using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public static SolarSystem instance;
    //定义一个天体物理常量G = 6.67*10e-11 
    //适当放大
    public readonly float G = 200f;
    //储存天体的数组
    public GameObject[] mCelestials;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mCelestials = GameObject.FindGameObjectsWithTag("Celestial");
        OrbitalSpeed();
    }


    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        Gravity();
    }

    //                       m1 * m2
    //万有引力定律： F = G * —————————— 
    //                        r * r
    public void Gravity()
    {
        foreach (GameObject PlantsA in mCelestials)
        {
            foreach (GameObject PlantsB in mCelestials)
            {
                if (!PlantsA.Equals(PlantsB))
                {
                    float m1 = PlantsA.GetComponent<Rigidbody>().mass;
                    float m2 = PlantsB.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(PlantsA.transform.position, PlantsB.transform.position);

                    PlantsA.GetComponent<Rigidbody>().AddForce((PlantsB.transform.position - PlantsA.transform.position).normalized *
                                                                (G * (m1 * m2) / (r * r)));
                }
            }
        }
    }

    //                    m2 * m2 * G
    // 轨道速度 v = sqrt（——————————————） 
    ///                   (m1 + m2)*r

    public void OrbitalSpeed()
    {
        foreach (GameObject PlantsA in mCelestials)
        {
            foreach (GameObject PlantsB in mCelestials)
            {
                if (!PlantsA.Equals(PlantsB))
                {
                    float m1 = PlantsA.GetComponent<Rigidbody>().mass;
                    float m2 = PlantsB.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(PlantsA.transform.position, PlantsB.transform.position);
                    PlantsA.transform.LookAt(PlantsB.transform);

                    PlantsA.GetComponent<Rigidbody>().velocity += PlantsA.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
}
