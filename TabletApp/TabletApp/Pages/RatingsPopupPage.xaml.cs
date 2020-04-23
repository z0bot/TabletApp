using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TabletApp.Models;
using TabletApp.Models.ServiceRequests;
using TabletApp.Pages;

using Realms;

using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingsPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        int Q1Rating = 0, Q2Rating = 0, Q3Rating = 0;
        public RatingsPopupPage()
        {
            InitializeComponent();
        }

        //inidicates no review was submitted so just pop back 
        private async void uxDeclineBtn_Clicked(object sender, EventArgs e)
        {
            //special type of pop out of page due to plugin
            await PopupNavigation.Instance.PopAllAsync();
        }

        //review was written. send review service request here
        private async void uxSubmitBtn_Clicked(object sender, EventArgs e)
        {
            string empID = RealmManager.All<Order>().FirstOrDefault().employee_id;
            string orderID = RealmManager.All<Order>().FirstOrDefault()._id;
            await PostReviewRequest.SendPostReviewRequest(orderID, empID, Q1Rating, uxQ1UserReview.Text, Q2Rating, uxQ2UserReview.Text, Q3Rating, uxQ3UserReview.Text);


            await DisplayAlert("Sucess!", "Thanks for your review", "OK");
            //special type of pop out of page due to plugin
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void uxQ1RatingButton1_Clicked(object sender, EventArgs e)
        {
            uxQ1RatingButton1.Source = "goldStar";
            uxQ1RatingButton2.Source = "greyStar";
            uxQ1RatingButton3.Source = "greyStar";
            uxQ1RatingButton4.Source = "greyStar";
            uxQ1RatingButton5.Source = "greyStar";
            Q1Rating = 1;
        }

        private void uxQ1RatingButton2_Clicked(object sender, EventArgs e)
        {
            uxQ1RatingButton1.Source = "goldStar";
            uxQ1RatingButton2.Source = "goldStar";
            uxQ1RatingButton3.Source = "greyStar";
            uxQ1RatingButton4.Source = "greyStar";
            uxQ1RatingButton5.Source = "greyStar";
            Q1Rating = 2;
        }

        //btn3
        private void uxQ1RatingButton3_Clicked(object sender, EventArgs e)
        {
            uxQ1RatingButton1.Source = "goldStar";
            uxQ1RatingButton2.Source = "goldStar";
            uxQ1RatingButton3.Source = "goldStar";
            uxQ1RatingButton4.Source = "greyStar";
            uxQ1RatingButton5.Source = "greyStar";
            Q1Rating = 3;
        }

        //btn4
        private void uxQ1RatingButton4_Clicked(object sender, EventArgs e)
        {
            uxQ1RatingButton1.Source = "goldStar";
            uxQ1RatingButton2.Source = "goldStar";
            uxQ1RatingButton3.Source = "goldStar";
            uxQ1RatingButton4.Source = "goldStar";
            uxQ1RatingButton5.Source = "greyStar";
            Q1Rating = 4;
        }
        //btn5
        private void uxQ1RatingButton5_Clicked(object sender, EventArgs e)
        {
            uxQ1RatingButton1.Source = "goldStar";
            uxQ1RatingButton2.Source = "goldStar";
            uxQ1RatingButton3.Source = "goldStar";
            uxQ1RatingButton4.Source = "goldStar";
            uxQ1RatingButton5.Source = "goldStar";
            Q1Rating = 5;
        }

        private void uxQ2RatingButton1_Clicked(object sender, EventArgs e)
        {
            uxQ2RatingButton1.Source = "goldStar";
            uxQ2RatingButton2.Source = "greyStar";
            uxQ2RatingButton3.Source = "greyStar";
            uxQ2RatingButton4.Source = "greyStar";
            uxQ2RatingButton5.Source = "greyStar";
            Q2Rating = 1;
        }

        private void uxQ2RatingButton2_Clicked(object sender, EventArgs e)
        {
            uxQ2RatingButton1.Source = "goldStar";
            uxQ2RatingButton2.Source = "goldStar";
            uxQ2RatingButton3.Source = "greyStar";
            uxQ2RatingButton4.Source = "greyStar";
            uxQ2RatingButton5.Source = "greyStar";
            Q2Rating = 2;
        }

        //btn3
        private void uxQ2RatingButton3_Clicked(object sender, EventArgs e)
        {
            uxQ2RatingButton1.Source = "goldStar";
            uxQ2RatingButton2.Source = "goldStar";
            uxQ2RatingButton3.Source = "goldStar";
            uxQ2RatingButton4.Source = "greyStar";
            uxQ2RatingButton5.Source = "greyStar";
            Q2Rating = 3;
        }

        //btn4
        private void uxQ2RatingButton4_Clicked(object sender, EventArgs e)
        {
            uxQ2RatingButton1.Source = "goldStar";
            uxQ2RatingButton2.Source = "goldStar";
            uxQ2RatingButton3.Source = "goldStar";
            uxQ2RatingButton4.Source = "goldStar";
            uxQ2RatingButton5.Source = "greyStar";
            Q2Rating = 4;
        }
        //btn5
        private void uxQ2RatingButton5_Clicked(object sender, EventArgs e)
        {
            uxQ2RatingButton1.Source = "goldStar";
            uxQ2RatingButton2.Source = "goldStar";
            uxQ2RatingButton3.Source = "goldStar";
            uxQ2RatingButton4.Source = "goldStar";
            uxQ2RatingButton5.Source = "goldStar";
            Q2Rating = 5;
        }

        private void uxQ3RatingButton1_Clicked(object sender, EventArgs e)
        {
            uxQ3RatingButton1.Source = "goldStar";
            uxQ3RatingButton2.Source = "greyStar";
            uxQ3RatingButton3.Source = "greyStar";
            uxQ3RatingButton4.Source = "greyStar";
            uxQ3RatingButton5.Source = "greyStar";
            Q3Rating = 1;
        }

        private void uxQ3RatingButton2_Clicked(object sender, EventArgs e)
        {
            uxQ3RatingButton1.Source = "goldStar";
            uxQ3RatingButton2.Source = "goldStar";
            uxQ3RatingButton3.Source = "greyStar";
            uxQ3RatingButton4.Source = "greyStar";
            uxQ3RatingButton5.Source = "greyStar";
            Q3Rating = 2;
        }

        //btn3
        private void uxQ3RatingButton3_Clicked(object sender, EventArgs e)
        {
            uxQ3RatingButton1.Source = "goldStar";
            uxQ3RatingButton2.Source = "goldStar";
            uxQ3RatingButton3.Source = "goldStar";
            uxQ3RatingButton4.Source = "greyStar";
            uxQ3RatingButton5.Source = "greyStar";
            Q3Rating = 3;
        }

        //btn4
        private void uxQ3RatingButton4_Clicked(object sender, EventArgs e)
        {
            uxQ3RatingButton1.Source = "goldStar";
            uxQ3RatingButton2.Source = "goldStar";
            uxQ3RatingButton3.Source = "goldStar";
            uxQ3RatingButton4.Source = "goldStar";
            uxQ3RatingButton5.Source = "greyStar";
            Q3Rating = 4;
        }
        //btn5
        private void uxQ3RatingButton5_Clicked(object sender, EventArgs e)
        {
            uxQ3RatingButton1.Source = "goldStar";
            uxQ3RatingButton2.Source = "goldStar";
            uxQ3RatingButton3.Source = "goldStar";
            uxQ3RatingButton4.Source = "goldStar";
            uxQ3RatingButton5.Source = "goldStar";
            Q3Rating = 5;
        }
    }
}