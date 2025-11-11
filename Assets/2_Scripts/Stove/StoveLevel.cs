using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoveLevel : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private StoveLevelSystem stoveToWatch;
    private int currentDisplayedLevel = -1;

    void Start()
    {
        if (levelText == null)
        {
            levelText = GetComponent<TextMeshProUGUI>();
        }
        if (StoveManager.Instance != null)
        {
            stoveToWatch = StoveManager.Instance.GetReferenceStove();
        }

    }

    void Update()
    {
        if (stoveToWatch != null)
        {
            int stoveLevel = stoveToWatch._data._level;

            if (stoveLevel != currentDisplayedLevel)
            {
                levelText.text = "Lv : " + stoveLevel.ToString();
                currentDisplayedLevel = stoveLevel;
            }
        }
    }
}