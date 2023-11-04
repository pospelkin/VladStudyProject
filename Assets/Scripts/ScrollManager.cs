using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public Transform bestTimeTextContainer;
    public GameObject bestTimeTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
        float[] bestTimes = new float[10];

        for (int i = 0; i < 10; i++)
        {
            bestTimes[i] = PlayerPrefs.GetFloat("BestTime" + i, 0);

            GameObject bestTimeTextObject = Instantiate(bestTimeTextPrefab, bestTimeTextContainer);
            TextMeshProUGUI bestTimeTMP = bestTimeTextObject.GetComponent<TextMeshProUGUI>();

            int bestMinutes = Mathf.FloorToInt(bestTimes[i] / 60);
            int bestSeconds = Mathf.FloorToInt(bestTimes[i] % 60);
            string bestTimeString = string.Format("{0:00}:{1:00}", bestMinutes, bestSeconds);
            bestTimeTMP.text = "" + (i + 1) + ". " + bestTimeString;
        }
    }
}
