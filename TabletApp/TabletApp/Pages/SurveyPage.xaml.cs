using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabletApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveyPage : ContentPage
	{
        string QOneAnswer = "5";
        string QTwoAnswer = "5";
        string QThreeAnswer = "5";
        string QFourAnswer = "5";

        public SurveyPage ()
		{
			InitializeComponent();

            for(int i = 1; i <= 5; i++)
            {
                spQOnePicker.Items.Add(i.ToString());
                spQTwoPicker.Items.Add(i.ToString());
                spQThreePicker.Items.Add(i.ToString());
                spQFourPicker.Items.Add(i.ToString());
            }

            spQOnePicker.SelectedIndexChanged += (sender, args) => { QOneAnswer = spQOnePicker.Items[spQOnePicker.SelectedIndex]; };
            spQTwoPicker.SelectedIndexChanged += (sender, args) => { QTwoAnswer = spQTwoPicker.Items[spQTwoPicker.SelectedIndex]; };
            spQThreePicker.SelectedIndexChanged += (sender, args) => { QThreeAnswer = spQThreePicker.Items[spQThreePicker.SelectedIndex]; };
            spQFourPicker.SelectedIndexChanged += (sender, args) => { QFourAnswer = spQFourPicker.Items[spQFourPicker.SelectedIndex]; };
        }
	}
}