using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo2 : MonoBehaviour
{
    public Image m_image;

    void Start()
    {
        Texture2D t2d = Resources.Load<Texture2D>("img1");
        Sprite sp = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
        m_image.sprite = sp;

        //Debug.Log(Demo3.Instance.name);
    }
}
