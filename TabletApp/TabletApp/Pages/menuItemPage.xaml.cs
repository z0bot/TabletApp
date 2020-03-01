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
	public partial class menuItemPage : ContentPage
	{
        public menuItemPage()
        {
            InitializeComponent();
        }

		public menuItemPage (string itemTitle, string itemDescription)
		{
			InitializeComponent ();

            miItemTitle.Text = itemTitle;
            miItemDescription.Text = itemDescription;
		}
	}
}