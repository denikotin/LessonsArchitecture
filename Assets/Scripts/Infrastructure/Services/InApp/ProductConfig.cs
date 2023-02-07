using System;
using UnityEngine.Purchasing;

[Serializable]
public class ProductConfig
{
    public string Id;
    public string Price;
    public string icon;
    public int MaxPurchaseCount;
    public int Quantity;
    public ItemType ItemType;
    public ProductType ProductType;
}
