using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItroRatation : MonoBehaviour
{
    public static ItroRatation instance;
    //旋转方向 -1逆时针，1顺时针
    public int RotateDir = -1;
    public float mRotationSpeed;
    //旋转方式 true为自动，false为手动
    private bool isAuto;
    public Text buttonText;

    void Start()
    {
        isAuto = true;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            buttonText.text = "手动旋转";
            transform.Rotate(Vector3.up * mRotationSpeed * RotateDir, Space.World);
        }
        else
        {
            buttonText.text = "自动旋转";
            if (Input.GetMouseButton(0))
            {
                float speed = 2.5f;//旋转跟随速度
                float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
                float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
                transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);//旋转物体
            }
        }
    }
    public void AutoChange()
    {
        isAuto = !isAuto;
    }
}
