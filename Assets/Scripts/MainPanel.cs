using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainPanel : MonoBehaviour
{
    public virtual void OpenPage()
    {
        gameObject.SetActive(true);
    }

    public virtual void ClosePage()
    {
        gameObject.SetActive(false);
    }
    public virtual void Refresh()
    {

    }
}
