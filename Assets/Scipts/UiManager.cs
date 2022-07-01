using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{

    [SerializeField] private Text _ammunition, _coin, _sayGetCoin, _buySuccess, _sayBuy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAmmunition(float Ammunition)
    {
        _ammunition.text = "Ammunition: " + (int)Ammunition;
    }
    
    public void ShowCoin(int coin)
    {
        _coin.text = "Coin: " + coin;
    }

    public void ShowSayGetCoin()
    {
        _sayGetCoin.text = "you need get coin!! your coin: 0";
    }
    
    public void ShowBuySuccess()
    {
        _buySuccess.text = "Buy Success";
    }

    public IEnumerator ShowSayBuy()
    {
        _sayBuy.text = "buy now!!";
        yield return new WaitForSeconds(5);

    }
    
    public void DisableStatusBuy()
    {
        _buySuccess.text = "";
        _sayBuy.text = "";
        _sayGetCoin.text = "";
    }
}
