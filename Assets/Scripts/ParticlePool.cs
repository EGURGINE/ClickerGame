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
        var pc = pcStack.Pop();
        pc.transform.parent = null;
        pc.gameObject.SetActive(true);
        pc.transform.position = _pos;
    }
}
