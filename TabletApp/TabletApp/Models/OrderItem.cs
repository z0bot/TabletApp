using System;
using System.Collections.Generic;
using System.Text;

namespace TabletApp.Models
{
    public class OrderItem
    {
        public enum orderType {NONE, ENTREE, DRINK, SIDE };

        public string itemName = "";
        public string specialInstructions = "";
        public float price = -1;
        public orderType itemType = orderType.NONE;
    }
}
