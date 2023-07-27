using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static Transform GlobalUITransform { get; private set; }

    void Awake()
    {
        GlobalUITransform = transform;
    }

    /// <summary>
    /// Adds UI element to the global UI manager.
    /// </summary>
    /// <param name="LocalUITranform">Transform of the UI gameobject to add.</param>
    public static void AddToUI(Transform LocalUITranform)
    {
        //LocalUITranform.SetParent(GlobalUITransform);
    }
}