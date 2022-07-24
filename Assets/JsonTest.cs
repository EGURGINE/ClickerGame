using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class JsonTest : MonoBehaviour
{
    public Button saveBtn;
    public Button loadBtn;
    void Start()
    {
        Debug.Log(JsonUtility.ToJson(new student
        {
            name = "야발",
            age = 1,
        }));
        saveBtn.onClick.AddListener(Save);
        loadBtn.onClick.AddListener(Load);
    }
    public void Save()
    {
#if UNITY_EDITOR

        File.WriteAllText($"{Application.dataPath}/Json.txt", JsonUtility.ToJson(new student
        {
            name = "권준호개새",
            age = 38,
        }));
        //Application.persistentDataPath;
        File.WriteAllText($"{Application.persistentDataPath}/MobileJson.txt", JsonUtility.ToJson(new StatusSave
        {
            statData.effort = gameManager.Effort;
        statData.studentPresidentLevel = president.Level;
        for (int i = 0; i < studentdata.Length; i++)
        {
            statData.studentLevel[i] = studentdata[i].studentData.level;
        }
        for (int i = 0; i < classroom.Length; i++)
        {
            statData.classBoolean[i] = classroom[i].IsBought;
            statData.classCurCost[i] = classroom[i].classData.currentCost;
        }
        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < STOCKYPOSCOUNT; j++)
            {
                statData.dotYPos[i, j] = stock[i].posY[j];
            }
            statData.quitTime[i] = stock[i].quitTime;
            statData.cycleTime[i] = stock[i].CycleDelay;
            statData.stockHave[i] = stock[i].Have;
        }
    }));
//#else

#endif
    }
    public void Load()
    {
        string json = File.ReadAllText($"{Application.dataPath}/Json.txt");
        student loaddata = JsonUtility.FromJson<student>(json);
        print(loaddata.name);
        print(loaddata.age);
    }

    private void OnDestroy()
    {
        saveBtn.onClick.RemoveAllListeners();
        loadBtn.onClick.RemoveAllListeners();
    }
}
[System.Serializable]
public class student
{
    public string name;
    public int age;
}
