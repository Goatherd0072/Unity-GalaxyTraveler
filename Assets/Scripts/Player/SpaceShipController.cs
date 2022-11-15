using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField]
    public enum MoveType
    {
        True,
        Play
    }
    public static SpaceShipController instance;
    public MoveType mMoveType;
    //通过键盘输入给力的方向
    public float mForceFactor_f;
    public float mForceFactor_h;
    public float mForceFactor_v;
    private float m_RollInput;
    //力的大小
    public float mVelocityRate;
    public Vector3 mMaxVelocity;
    public Rigidbody mRigidBody;
    //跟随鼠标旋转速度
    public float mLookRoteSpeed = 90f;
    //鼠标参数
    private Vector2 m_ScreenCenter;
    //锁定状态
    public bool mLocked;
    //是否进入轨道
    public bool isEntry;
    public GameObject mLockedObject;
    //滚动速度,精确度
    public float mRollSpeed = 90f, mRollAcceleration = 3.5f;

    void Awake()
    {
        mMoveType = MoveType.True;
        instance = this;
        mLocked = false;
        isEntry = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = transform.gameObject.GetComponent<Rigidbody>();
        //获取屏幕中心点
        m_ScreenCenter.x = Screen.width * 0.5f;
        m_ScreenCenter.y = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mForceFactor_f = Input.GetAxis("Forward");
        mForceFactor_h = Input.GetAxis("Horizontal");
        mForceFactor_v = Input.GetAxis("Roll");
        m_RollInput = Mathf.Lerp(m_RollInput, Input.GetAxis("Roll"), mRollAcceleration * Time.deltaTime);
        //Debug.Log(mForceFactor);
        //Debug.Log(mRigidBody.velocity);
        LockedPlanet();
        EntryOrbit();

        switch (mMoveType)
        {
            case MoveType.True:
                Move_Forward();
                Move_Horizontal();
                break;
            case MoveType.Play:
                Move_Simple();
                break;
        }
        Move_Roll();

    }

    public Vector3 CalculateRelativeVelocity()
    {
        if (mLocked)
        {
            //飞船指向星球的方向
            Vector3 dirToPlanet = (mLockedObject.transform.position - transform.position).normalized;
            //世界坐标下的相对速度
            Vector3 relativeVelocityWorldSpace = mRigidBody.velocity - mLockedObject.GetComponent<Rigidbody>().velocity;

            //水平方向=飞船指向星球的方向和飞船y轴的叉积的法向量
            //垂直方向则是x轴
            Vector3 hor = Vector3.Cross(dirToPlanet, transform.up).normalized;
            //点积确保hor和x轴在同一方向， Sign（）函数进行归于1化处理
            hor *= Mathf.Sign(Vector3.Dot(hor, transform.right));
            //垂直
            Vector3 vec = Vector3.Cross(dirToPlanet, hor).normalized;
            vec *= Mathf.Sign(Vector3.Dot(vec, transform.up));

            //把相对速度投影到对应轴上
            float Vx = Vector3.Dot(relativeVelocityWorldSpace, hor);
            float Vy = Vector3.Dot(relativeVelocityWorldSpace, vec);
            float Vz = Vector3.Dot(relativeVelocityWorldSpace, dirToPlanet);

            Vector3 relativeV = new Vector3(Vx, Vy, Vz);

            return relativeV;
        }
        else
            return Vector3.zero;
    }
    private void Move_Forward()
    {
        mRigidBody.AddRelativeForce(Vector3.forward * mForceFactor_f * mVelocityRate, ForceMode.Impulse);
    }
    private void Move_Horizontal()
    {
        mRigidBody.AddRelativeForce(Vector3.right * mForceFactor_h * mVelocityRate, ForceMode.Impulse);
    }
    private void Move_Simple()
    {
        float ForwardSpeed = 0f, HorSpeed = 0f, VecSpeed = 0f;
        ForwardSpeed = Mathf.Lerp(ForwardSpeed, mForceFactor_f * 20f, 2.5f);
        HorSpeed = Mathf.Lerp(HorSpeed, mForceFactor_h * 7.5f, 2f);
        VecSpeed = Mathf.Lerp(VecSpeed, mForceFactor_v * 5f, 2f);

        transform.position += transform.forward * ForwardSpeed;
        transform.position += (transform.right * HorSpeed) + (transform.up * VecSpeed);

    }
    private void Move_Roll()
    {
        Vector2 m_LookInput;
        Vector2 m_MouseDistance;
        m_LookInput.x = Input.mousePosition.x;
        m_LookInput.y = Input.mousePosition.y;

        m_MouseDistance.x = (m_LookInput.x - m_ScreenCenter.x) / m_ScreenCenter.y;
        m_MouseDistance.y = (m_LookInput.y - m_ScreenCenter.y) / m_ScreenCenter.y;

        m_MouseDistance = Vector2.ClampMagnitude(m_MouseDistance, 1f);

        transform.Rotate(-m_MouseDistance.y * mLookRoteSpeed * Time.deltaTime,
                        m_MouseDistance.x * mLookRoteSpeed * Time.deltaTime,
                        m_RollInput * mRollSpeed * Time.deltaTime, Space.Self);
    }
    private void LockedPlanet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(m_ScreenCenter);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Celestial")
                {
                    Debug.Log("碰撞物：" + hit.collider.name);
                    mLocked = true;
                    mLockedObject = hit.collider.gameObject;
                    if (mLockedObject.GetComponent<PlanetsBar>() == null)
                        mLockedObject.AddComponent<PlanetsBar>();
                    Tips.instance.SetTips("锁定星球：" + hit.collider.transform.parent.name);
                }
                else
                {
                    mLocked = false;
                    Tips.instance.SetTips("当前没有目标");
                }
            }
        }
    }
    public float EntryOrbit()
    {
        if (mLocked)
        {
            //星球和飞船的距离
            float D = Vector3.Distance(mRigidBody.ClosestPointOnBounds(mLockedObject.transform.position),
            mLockedObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position));

            if (D <= 400f)
            {
                Tips.instance.SetTips("按下 空格 进入轨道");
                if (Input.GetKey(KeyCode.Space))
                {
                    isEntry = true;
                    Material m_Material = mLockedObject.GetComponent<Renderer>().material;
                    ModeChnage.instance.ChangeToIntro(m_Material, mLockedObject.name);
                }
            }

            return D;
        }
        return 0;
    }
    public void SetMode()
    {
        mRigidBody.velocity = Vector3.zero;
        switch (mMoveType)
        {
            case MoveType.True:
                mMoveType = MoveType.Play;
                Tips.instance.SetTips("已经切换到简单模式");
                break;
            case MoveType.Play:
                mMoveType = MoveType.True;
                Tips.instance.SetTips("已经切换到真实模式");
                break;
        }
    }
}
