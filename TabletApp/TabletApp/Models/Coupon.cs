using System;
using System.Collections.Generic;
using System.Text;

using Realms;
using System.Linq;

namespace TabletApp.Models
{
    public class Coupon : RealmObject
    {
        [PrimaryKey]
        public string _id { get; set; }

        public string description { get; set; }

        public IList<string> requiredItems { get; }

        public IList<string> appliedItems { get; }

        public string couponType { get; set; }

        public double discount { get; set; }

        public bool repeatable { get; set; }

        public bool active { get; set; }

        public bool selected { get; set; } // Used for Customer coupons. Indicates that the customer wants to apply these to their order

        public Coupon() { }

        public Coupon(Coupon c)
        {
            _id = c._id;
            description = c.description;

            for (int i = 0; i < c.requiredItems.Count(); i++)
                requiredItems.Add(c.requiredItems[i]);

            for (int i = 0; i < c.appliedItems.Count(); i++)
                appliedItems.Add(c.appliedItems[i]);

            couponType = c.couponType;
            discount = c.discount;
            repeatable = c.repeatable;
            active = c.active;
            selected = c.selected;
        }
    }
}