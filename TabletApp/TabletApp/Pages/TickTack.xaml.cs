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
    public partial class TickTack : ContentPage
    {
        public string[,] table = new string[3, 3];
        public string Active = "X";
        public int count = 0;
        public TickTack()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    table[i, j] = i+" "+j;
                }
            }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++)
                {
                    Button temp = new Button()
                    {
                        Text = "",
                        FontSize = 40,
                        TextColor = Xamarin.Forms.Color.White,
                        BackgroundColor = Xamarin.Forms.Color.FromHex("#24BF87")
                    };

                    temp.SetValue(Grid.RowProperty, i);
                    temp.SetValue(Grid.ColumnProperty, j);
                    temp.Clicked += async (sender, args) => TextChange(temp);
                    GMaster.Children.Add(temp);
                }
            }
        }
        private async void TextChange(Button temp) {
            int x=Grid.GetRow(temp);
            int y = Grid.GetColumn(temp);
            temp.Text = Active;
            table[x, y] = Active;
            bool win = CheckWin();
            if (win) {
                await Navigation.PushAsync(new Uwin());
            }
            if (Active == "X")
            {
                Active = "O";
            }
            else {
                Active = "X";
            }
            count++;
            if (count == 9) {
                await Navigation.PushAsync(new Uwin());
            }
        }
        private bool CheckWin() {
            string current;
            for (int i = 0; i < 3; i++)
            {
                current = table[i, 0];
                if ((current == table[i, 1]) && (current == table[i, 2]))
                {
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                current = table[0, i];
                if ((current == table[1, i]) && (current == table[2, i])) {
                    return true;
                }
            }
            current = table[0, 0];
            if ((current == table[1, 1]) && (current == table[2, 2])) {
                return true;
            }
            current = table[0, 2];
            if ((current == table[1, 1]) && (current == table[2, 0]))
            {
                return true;
            }
            return false;
        }

    }
}