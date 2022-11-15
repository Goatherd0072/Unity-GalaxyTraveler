using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadFile : MonoBehaviour
{
    public Text Ito_Text;
    public string[] str;
    // Start is called before the first frame update
    void Start()
    {
        //localPath = UnityEngine.Application.dataPath + "/" + "Celestia.txt";
        // 将test01 中的内容加载进txt文本中
        TextAsset txt = Resources.Load("Celestia") as TextAsset;
        // 输出该文本的内容
        Debug.Log(txt);

        // 以换行符作为分割点，将该文本分割成若干行字符串，并以数组的形式来保存每行字符串的内容
        str = txt.text.Split('\n');
        Debug.Log(str);
        // 将该文本中的字符串输出
        // Debug.Log("str[0]= " + str[0]);
        // Debug.Log("str[1]= " + str[1]);
    }
    public void mFunLoadFile(string Pname)
    {
        Debug.Log(Pname);
        Debug.Log(str);
        // 将每行字符串的内容以逗号作为分割点，并将每个|分隔的字符串内容遍历输出
        foreach (string strs in str)
        {
            if (strs.Contains(Pname))
            {
                string[] ss = strs.Split('|');
                Debug.Log(ss[0]);
                Debug.Log(ss[1]);
                Debug.Log(ss[2]);
                Ito_Text.text = ss[1];
                ItroRatation.instance.RotateDir = int.Parse(ss[2]);
            }
        }
    }
}
