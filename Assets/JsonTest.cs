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

        File.WriteAllText($"{Application.dataPath}/Json.txt", JsonUtility.ToJson(new student
        {
            name = "권준호개새",
            age = 38,
        }));
        //Application.persistentDataPath;
        File.WriteAllText($"{Application.persistentDataPath}/MobileJson.txt", JsonUtility.ToJson(new StatusSave
        {

        }));
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
