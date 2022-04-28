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
    public string appRename = "����";

    void Awake()
    {
        m_LoginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        m_LoginBtn.onClick.AddListener(OnLoginBtnClick);
        m_SetNameBtn = transform.Find("SetNameBtn").GetComponent<Button>();
        m_SetNameBtn.onClick.AddListener(SetWindowText);
    }

    void OnLoginBtnClick()
    {
        //Debug.Log("��½");

        //�����û�׼������Ϣ
        JsonModel model = new JsonModel();
        model.name = "mike";
        model.age = age;
        model.area = "NY";
        string json = JsonUtility.ToJson(model);
        Demo1.SendJson(json);
    }

    // ���ô�����
    public void SetWindowText()
    {
        //IntPtr m_hWnd = Demo1.FindWindow(null, appName); //���������ң�������ᶪ
        IntPtr m_hWnd = Demo1.FindWindow("UnityWndClass", null); //�ı��ⲻ�ᶪ
        //Demo3.SetWindowText(m_hWnd, appRename);
    }

    void Update()
    {
        if (loop)
            OnLoginBtnClick();
    }
}