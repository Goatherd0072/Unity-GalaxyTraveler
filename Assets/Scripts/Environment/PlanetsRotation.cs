using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetsRotation : MonoBehaviour
{

    //自转方向
    public enum RotationDirection
    {
        //顺时针
        Positive,
        //逆时针
        Negative
    }
    public RotationDirection mRDirection = RotationDirection.Positive;
    //自转速度
    public float RotationVelocity;

    // Start is called before the first frame update
    void Start()
    {
        RoVelocityCalculate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Rotate();
    }

    public void Rotate()
    {
        int i = 0;
        switch (mRDirection)
        {
            case RotationDirection.Positive:
                i = 1;
                break;
            case RotationDirection.Negative:
                i = -1;
                break;
        }

        this.transform.Rotate(Vector3.up * RotationVelocity * i);
    }

    //计算星球自转的线速度
    //            G * M
    //V = sqrt（ ————————）
    //              r
    public void RoVelocityCalculate()
    {
        float G = 0.0667f;
        //float G = SolarSystem.instance.G;
        float M = this.GetComponent<Rigidbody>().mass;
        float r = this.transform.localScale.x;

        RotationVelocity = Mathf.Sqrt(G * M / r);
    }
}
