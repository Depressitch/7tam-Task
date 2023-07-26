using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocalManager : MonoBehaviour
{
    void Awake()
    {
        UIManager.AddToUI(transform);
    }
}
