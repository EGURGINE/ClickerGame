using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacement : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static CharacterPlacement instance;

    public static CharacterPlacement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPlacement>();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] private StudentData[] studentsData;
    [SerializeField] private GameObject[] studentsObjs;
    private Dictionary<StudentData,GameObject> students = new Dictionary<StudentData,GameObject>();

    private int i;

    private void Awake()
    {
        instance=this;
    }
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            students.Add(studentsData[i], studentsObjs[i]);
        }

        foreach (var item in studentsData)
        {
            if (item.level > 0) studentsObjs[i].SetActive(true);
            i++;
        }
    }

    public void studentLook(StudentData data)
    {
        students[data].SetActive(true);
    }
}
