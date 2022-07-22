using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : Singleton<StatusManager>
{
    
}
[Serializable]
public class StatusSave
{
    public ulong effort;
    public int studentPresidentLevel;
    public int[] studentLevel;
}
