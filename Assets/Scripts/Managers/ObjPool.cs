using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPoolType 
{
    Money,
}
/// <summary>
/// 다중 오브젝트 풀링(그냥 해보고 
/// </summary>
public class ObjPool : MonoBehaviour
{
    #region 싱글톤
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
    #endregion

    public GameObject[] originObject;
    private Dictionary<EPoolType, Queue<GameObject>> pool = new Dictionary<EPoolType, Queue<GameObject>>();

    public GameObject GetObj(EPoolType type, Vector3 pos)
    {
        GameObject obj = null;

        if (pool.ContainsKey(type) == false)
        {
            pool.Add(type, new Queue<GameObject>());
        }

        Queue<GameObject> queue = pool[type];
        GameObject origin = originObject[(int)type];

        if(queue.Count > 0)
        {
            queue.Dequeue();
        }
        else
        {
            obj = Instantiate(origin);
        }
        obj.transform.position = pos;
        obj.gameObject.SetActive(true);


        return obj;
    }

    public T Get<T>(EPoolType type, Vector3 pos) where T : MonoBehaviour
    {
        return GetObj(type, pos).GetComponent<T>();
    }

    public void Return(EPoolType Type, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        pool[Type].Enqueue(obj);
    }

 

}
