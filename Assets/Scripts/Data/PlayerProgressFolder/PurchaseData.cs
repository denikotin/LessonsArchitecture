using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data.PlayerProgressFolder
{
    [Serializable]
    public class PurchaseData
    {
        public List<BoughtIAP> boughtIAPs = new List<BoughtIAP>();
        public Action OnChangedEvent;

        public void AddPurchase(string id)
        {
            BoughtIAP boughtIAP= boughtIAPs.Find(x=> x.id==id);

            if (boughtIAP != null)
            {
                boughtIAP.count++;

            }
            else
            {
                 boughtIAPs.Add(new BoughtIAP { id = id, count = 1 });
            }
            OnChangedEvent?.Invoke();
        }
    }
}
