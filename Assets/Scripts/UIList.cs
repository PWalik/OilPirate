using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIList : MonoBehaviour
{
    public List<Transform> items;
    [SerializeField]
    Color normalCol, blackCol;
    int itemsBlack;

    public void BlackItem()
    {
        Debug.Log(items.Count);
        if (items.Count - itemsBlack == 0)
            return;
        items[items.Count - itemsBlack - 1].GetComponent<Image>().color = blackCol;
        itemsBlack++;
    }

    public void DeBlackItem()
    {
        if (itemsBlack == 0)
            return;
        items[items.Count - itemsBlack].GetComponent<Image>().color = normalCol;
        itemsBlack--;
    }
}
