using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static ParticlePool instance;

    public static ParticlePool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ParticlePool>();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] GameObject pcPoolObj;
    [SerializeField] ParticleSystem pc;
    [SerializeField] int pcPoolCnt;
    private Stack<ParticleSystem> pcStack = new Stack<ParticleSystem>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < pcPoolCnt; i++)
        {
            var kingA = Instantiate(pc);
            kingA.transform.parent = pcPoolObj.transform;
            kingA.gameObject.SetActive(false);  
            pcStack.Push(kingA);
        }
    }

    public void Push(ParticleSystem _pc)
    {
        _pc.transform.parent = pcPoolObj.transform;
        pcStack.Push(_pc);
        _pc.gameObject.SetActive(false);
    }
    public void Pop(Vector2 _pos)
    {   
        if(pcStack.Count <= 0)
        {
            var kingA = Instantiate(pc);
            kingA.transform.parent = pcPoolObj.transform;
            kingA.gameObject.SetActive(false);
            pcStack.Push(kingA);
        }
        GameObject pcPop = pcStack.Pop().gameObject;
        pcPop.transform.parent = null;
        pcPop.gameObject.SetActive(true);
        pcPop.transform.position = _pos;
        pcPop.GetComponent<ThouchPc>().Play();
    }
}
