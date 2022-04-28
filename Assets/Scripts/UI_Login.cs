using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Login : MonoBehaviour
{
    [SerializeField] Button m_LoginBtn;
    [SerializeField] Button m_SetNameBtn;
    public bool loop;
    public int age;
    public string title;

    void Awake()
    {
        m_LoginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        m_LoginBtn.onClick.AddListener(OnLoginBtnClick);
        m_SetNameBtn = transform.Find("SetNameBtn").GetComponent<Button>();
        m_SetNameBtn.onClick.AddListener(SetWindowText);
    }

    void OnLoginBtnClick()
    {
        Debug.Log("登陆");

        //发送用户准备好消息
        JsonModel model = new JsonModel();
        model.name = "mike";
        model.age = age;
        model.area = "NY";
        string json = JsonUtility.ToJson(model);
        Demo1.SendJson(json);
    }

    // 设置窗口名
    public void SetWindowText()
    {
        IntPtr hWndPalaz = Demo1.FindWindow(null, "tata");
        //Debug.Log($"获得游戏本身句柄：{(int)hWndPalaz}");
        IntPtr m_hWnd = Demo1.FindWindow("UnityWndClass", null);
        //Demo3.SetWindowText(m_hWnd, title);
        Demo3.SendMessage(hWndPalaz, 0x0005, (IntPtr)1024, (IntPtr)720);
    }

    void Update()
    {
        if (loop)
            OnLoginBtnClick();
    }
}