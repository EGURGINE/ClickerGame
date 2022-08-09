using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThouchPc : MonoBehaviour
{
    ParticleSystem pc => GetComponent<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {
        pc.Stop();
    }
    public void Play()
    {
        pc.Play();
        StartCoroutine(PushPc());
    }
    private IEnumerator PushPc()
    {
        yield return new WaitForSeconds(0.5f);
        pc.Stop();
        ParticlePool.Instance.Push(pc);
    }
}
