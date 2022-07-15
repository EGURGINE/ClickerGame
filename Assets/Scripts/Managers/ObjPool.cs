using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    private static ObjPool instance;

    public static ObjPool Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ObjPool>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(Instance == null)
        {
            instance = FindObjectOfType<ObjPool>();
        }
        else
        {
            Destroy(gameObject);
        }
    }



}
