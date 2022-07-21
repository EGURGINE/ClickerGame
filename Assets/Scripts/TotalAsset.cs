using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalAsset : MonoBehaviour
{
    public List<ClassData> datas = new List<ClassData>();

    public ulong totalAssets;
    [SerializeField]
    private TextMeshProUGUI totalAssetsText;

    private void Update()
    {
        totalAssets = datas[0].currentCost
                    + datas[1].currentCost
                    + datas[2].currentCost
                    + datas[3].currentCost
                    + datas[4].currentCost;
        totalAssetsText.text = StringFormat.ToString(totalAssets);
    }
}
