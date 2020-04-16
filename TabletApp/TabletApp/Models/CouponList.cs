using System;
using System.Collections.Generic;
using System.Text;

using Realms;

namespace TabletApp.Models
{
    class CouponsList : RealmObject
    {
        public IList<Coupon> Coupons { get; }

        public CouponsList() { }


        public CouponsList(List<Coupon> c)
        {
            Coupons = c;
        }
    }
}