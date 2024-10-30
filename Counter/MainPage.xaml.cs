using Counter.Data;
using Counter.Models;
using System.Xml.Linq;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        List<CounterModel> counters = new();
        DataService dataService;

        

        public MainPage()
        {
            dataService = new DataService();


            InitializeComponent();
            LoadCounters();
            
            ErrorMsg.Text = "";
        }

        private async void LoadCounters()
        {
            counters = await dataService.LoadCountersAsync();
            CounterCollectionView.ItemsSource = counters;
        }

        private async void AddCounter(object sender, EventArgs e)
        {
            try
            {

            string name = Name.Text;
            int initialValue = int.Parse(InitialValue.Text);
            Color color = Color.FromArgb(CounterColor.Text);

            if(String.IsNullOrEmpty(CounterColor.Text) || !CounterColor.Text.Contains("#") || CounterColor.Text.Length != 7 ){
                throw new Exception("Wrong color");
            }

            CounterModel newCounter = new(name, color, initialValue);

            counters.Add(newCounter);

            

            Name.Text = "";
            InitialValue.Text = "";
            CounterColor.Text = "";

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;

            ErrorMsg.Text = "";

            }catch
            {
                ErrorMsg.Text = "Enter proper values!";
            }
        }

        private async void OnIncrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value++;

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }

        private async void OnDecrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value--;

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Reset();

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counters.Remove(counter);

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }
    }

}
