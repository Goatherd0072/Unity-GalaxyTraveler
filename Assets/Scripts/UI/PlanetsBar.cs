using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsBar : MonoBehaviour
{
    private GameObject m_TextObj;
    private GameObject uiUnit;
    private Text m_PlanetName;
    private Text m_Distance;
    public Vector3 mOffset;
    // Start is called before the first frame update
    void Start()
    {
        mOffset = new Vector3(0f, 150f, 0f);
        uiUnit = Resources.Load<GameObject>("UI/PlanetsBar");


        m_TextObj = Instantiate<GameObject>(uiUnit);
        m_TextObj.transform.SetParent(GameObject.Find("PlanetsBarRoot").transform);
        uiUnit.SetActive(false);
        m_PlanetName = m_TextObj.transform.Find("PlanetName").GetComponent<Text>();
        m_Distance = m_TextObj.transform.Find("Distance").GetComponent<Text>();

        m_PlanetName.text = this.transform.parent.name;
        m_Distance.text = "100km";
        uiUnit.SetActive(true);
    }

    void LateUpdate()
    {
        m_TextObj.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + mOffset);

        string D = (SpaceShipController.instance.EntryOrbit()).ToString("0.0");
        m_Distance.text = D + " km";

        if (transform.gameObject == SpaceShipController.instance.mLockedObject)
        {
            return;
        }
        Destroy(this.GetComponent<PlanetsBar>());
    }
}
