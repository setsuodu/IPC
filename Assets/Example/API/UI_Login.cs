using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Windows;
using System.Runtime.InteropServices;

public class UI_Login : MonoBehaviour
{
    [SerializeField] Button m_LoginBtn;
    [SerializeField] Button m_SetNameBtn;
    public bool loop = false;
    public int age = 1;
    public string appName = "noname";
    public string appRename = "改名";

    void Awake()
    {
        m_LoginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        m_LoginBtn.onClick.AddListener(OnLoginBtnClick);
        m_SetNameBtn = transform.Find("SetNameBtn").GetComponent<Button>();
        m_SetNameBtn.onClick.AddListener(SetWindowText);
    }

    void OnLoginBtnClick()
    {
        //Debug.Log("登陆");

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
        //IntPtr m_hWnd = Demo1.FindWindow(null, appName); //根据名字找，改名后会丢
        IntPtr m_hWnd = Demo1.FindWindow("UnityWndClass", null); //改标题不会丢
        //Demo3.SetWindowText(m_hWnd, appRename);
    }

    void Update()
    {
        if (loop)
            OnLoginBtnClick();
    }
}