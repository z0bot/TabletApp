using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabletApp.Models;
using TabletApp.Models.ServiceRequests;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckoutPage : ContentPage
	{
        private double totalPrice = 0;

        private double tip = 0;

        private Order order;

        private IList<Coupon> coupons = new List<Coupon>();
        private IList<string> toggledIDs = new List<string>();

        private OrderList checkOutOrder = new OrderList();

		public CheckoutPage()
		{
			InitializeComponent();

            updateItems();

            cpAddCouponButton.Clicked += cpAddCouponButton_Clicked;
            cpCheckoutButton.Clicked += cpCheckoutButton_Clicked;
        }

        private async void getRestrauntCoupons()
        {
            await GetCouponsRequest.SendGetCouponsRequest();

            for (int i = 0; i < RealmManager.All<CouponsList>().First().Coupons.Count(); i++)
            {
                if (RealmManager.All<CouponsList>().First().Coupons[i].couponType == "Restaurant")
                {
                    coupons.Add(new Coupon(RealmManager.All<CouponsList>().First().Coupons[i]));
                }
            }
        }

        private async void cpAddCouponButton_Clicked(object sender, EventArgs e)
        {
            string input = "";
            input = await DisplayPromptAsync("Coupon Code", "You agknowledge by entering this coupon code that the coupon will be applied whether or not the items in question are being payed for or not.", "OK", "Cancel", null, -1, null, "");

            RealmManager.RemoveAll<Coupon>();
            await GetCouponsByIDRequest.SendGetCouponsByIDRequest(input);

            if (RealmManager.All<Coupon>().Count() == 0) await DisplayAlert("Coupon Code Invalid", "", "OK");
            else
            {
                await DisplayAlert("Your coupon has been activated", "To see it take effect toggle or re-toggle the items in question", "OK");

                Coupon coupon = RealmManager.All<Coupon>().First();

                coupons.Add(coupon);
            }
        }

        private async void cpCheckoutButton_Clicked(object sender, EventArgs e)
        {
            if (checkOutOrder.orderItems.Count() == 0)
            {
                await DisplayAlert("No Items Selected", "Please select items to checkout", "OK");
                return;
            }

            await UpdateOrderMenuItemsRequest.SendUpdateOrderMenuItemsRequest(order._id, order.menuItems);

            tip = (cpTip.Text == "") ? 0f : Double.Parse(cpTip.Text);

            updateItems();

            MainMenu.OnReturn();

            await PostTipRequest.SendPostTipRequest(order.employee_id, tip);

            for (int i = 0; i < coupons.Count(); i++)
                if (coupons[i].couponType == "Customer")
                    await DeactivateCouponRequest.SendDeactivateCouponRequest(coupons[i]._id);

            bool orderComplete = true;

            await GetTableRequest.SendGetTableRequest(RealmManager.All<Table>().First().table_number);

            for (int i = 0; i < RealmManager.All<Table>().First().order_id.menuItems.Count(); i++)
            {
                if (!RealmManager.All<Table>().First().order_id.menuItems[i].paid && !RealmManager.All<Table>().First().order_id.menuItems[i].prepared)
                {
                    orderComplete = false;
                    break;
                }
            }

            if (orderComplete) await FinishOrderRequest.SendFinishOrderRequest(RealmManager.All<Table>().First()._id);

            await Navigation.PushAsync(new PaymentPage(totalPrice, tip));
        }

        private void updateItems()
        {
            if (cpOrderScroll.Children.Count() > 1)
            {
                cpOrderScroll.Children.Clear();

                cpOrderScroll.Children.Add(new Label()
                {
                    Text = "FULL ORDER",
                    Margin = new Thickness(30, 30, 30, 30),
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Xamarin.Forms.Color.Black
                });
            }

            Order oldOrder = RealmManager.All<Table>().First().order_id;

            order = new Order();

            order._id = oldOrder._id;
            order.employee_id = oldOrder.employee_id;
            order.send_to_kitchen = oldOrder.send_to_kitchen;

            for (int i = 0; i < oldOrder.menuItems.Count(); i++)
            {
                order.menuItems.Add(new OrderItem(oldOrder.menuItems[i]));
            }

            for (int i = 0; i < order.menuItems.Count(); i++)
            {
                OrderItem x = order.menuItems[i];

                if (!x.paid)
                {
                    Grid newGrid = new Grid();
                    newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    Label newLabel = new Label()
                    {
                        Text = x.name + " --- " + x.price.ToString("C"),
                        Margin = new Thickness(30, 0, 30, 15),
                        FontSize = 20,
                        WidthRequest = 100,
                        TextColor = Color.Black,
                    };

                    newGrid.Children.Add(newLabel, 0, 0);

                    Switch newSwitch = new Switch() { };

                    newSwitch.Toggled += (sender, e) =>
                    {
                        if (newSwitch.IsToggled) toggledIDs.Add(x._id);
                        else toggledIDs.Remove(x._id);

                        double price = x.price;
                        double discount = GetDiscount(x);

                        price -= (price * (discount / (double)100));

                        if (newSwitch.IsToggled)
                        {
                            checkOutOrder.orderItems.Add(x);

                            totalPrice += price;

                            x.paid = true;

                            newLabel.TextColor = Xamarin.Forms.Color.Green;
                        }
                        else
                        {
                            checkOutOrder.orderItems.Remove(x);

                            totalPrice -= price;

                            x.paid = false;

                            newLabel.TextColor = Xamarin.Forms.Color.Black;
                        }

                        if (discount > 0)
                        {
                            newSwitch.IsVisible = false;
                        }

                        updatePrice();
                    };

                    newGrid.Children.Add(newSwitch, 1, 0);

                    cpOrderScroll.Children.Add(newGrid);
                }
            }
        }

        private void updatePrice()
        {
            cpFullPrice.Text = "Total: " + totalPrice.ToString("C");
        }

        private double GetDiscount(OrderItem x)
        {
            bool applies = false;
            Coupon coup = null;

            if (coupons == null) return 0;

            for (int i = 0; i < coupons.Count(); i++)
            {
                for (int j = 0; j < coupons[i].appliedItems.Count(); j++)
                {
                    if (coupons[i].appliedItems[j] == x._id)
                    {
                        applies = true;
                        coup = coupons[i];
                        break;
                    }
                }

                if (applies) break;
            }

            if (!applies) return 0;

            int reqNum = coup.requiredItems.Count();
            int actNum = 0;

            for (int i = 0; i < toggledIDs.Count(); i++)
            {
                for (int j = 0; j < coup.requiredItems.Count(); j++)
                {
                    if (toggledIDs[i] == coup.requiredItems[j])
                    {
                        actNum++;
                    }
                }
            }

            if (actNum == reqNum) return coup.discount;
            else return 0;
        }
    }
}