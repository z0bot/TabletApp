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

    //This is the Fisher-Yates shuffle method
    public class Randomizer
    {
        public static void Randomize<T>(T[] items)
        {
            Random rand = new Random();

            
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
    public partial class MemoryGame : ContentPage
    {
        public String Normal ="#24BF87";
        public String[] Colors= new string[16];
        public int Active = 0;
        public int found = 0;
        public Button holder;
        public Button holder2;
        public MemoryGame()
        {
            InitializeComponent();

            int count = 0;
            //adds all the colors to a list of colors
            Colors[0]="#FF0000";//red
            Colors[1]="#FF0000";
            Colors[2]="#0000FF";//blue
            Colors[3]="#0000FF";
            Colors[4]="#008080";//teal
            Colors[5]="#008080";
            Colors[6]="#800080";//purple
            Colors[7]="#800080";
            Colors[8]="#FFA500";//orange
            Colors[9]="#FFA500";
            Colors[10]="#FFFF00";//yellow
            Colors[11]="#FFFF00";
            Colors[12]="#A52A2A";//brown
            Colors[13]="#A52A2A";
            Colors[14]="#FFB6C1";//pink
            Colors[15]="#FFB6C1";
          

            Randomizer.Randomize(Colors);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button temp = new Button()
                    {
                        Text = Colors[count],
                        TextColor= Xamarin.Forms.Color.Transparent,
                        BackgroundColor = Xamarin.Forms.Color.FromHex(Normal)
                    };
                    
                    temp.SetValue(Grid.RowProperty, i);
                    temp.SetValue(Grid.ColumnProperty, j);
                    temp.Clicked += async (sender, args) => ColorChange(temp);
                    GMaster.Children.Add(temp);
                    count++;
                }
            }
        }

        private async void ColorChange(Button temp) {
            
            temp.BackgroundColor= Xamarin.Forms.Color.FromHex(temp.Text);
            if (Active == 0)
            {
                holder = temp;
                Active++;
            }
            else if (Active == 1) {
                holder2 = temp;
                if (temp.BackgroundColor == holder.BackgroundColor)
                {
                    temp.BackgroundColor = Xamarin.Forms.Color.Transparent;
                    temp.IsEnabled = false;
                    temp.Text = "";
                    holder.BackgroundColor = Xamarin.Forms.Color.Transparent;
                    holder.Text = "";
                    holder.IsEnabled = false;
                    found++;
                    Active = 0;
                }
                else {
                    Active++;
                }
                
            }
            else {
                Active = 1;
                holder.BackgroundColor = Xamarin.Forms.Color.FromHex(Normal);
                holder2.BackgroundColor = Xamarin.Forms.Color.FromHex(Normal);
                holder = temp;
              

            }
            if (found == 8)
            {
                await Navigation.PushAsync(new Uwin());
            }

        }
      
    }
}