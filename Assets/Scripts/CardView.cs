using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] Text Textname;
    [SerializeField] Text Texthp;
    [SerializeField] Text Textat;
    [SerializeField] Text Textcost;
    [SerializeField] Image Imageicon;

    public void Show(CardModel cardModel)
    {
        this.Textname.text = cardModel.name;
        this.Texthp.text = cardModel.hp.ToString();
        this.Textat.text = cardModel.at.ToString();
        this.Textcost.text = cardModel.cost.ToString();
        this.Imageicon.sprite = cardModel.icon;
    }
}
