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
            
        }

        private async void LoadCounters()
        {
            counters = await dataService.LoadCountersAsync();
            CounterCollectionView.ItemsSource = counters;
        }

        private async void AddCounter(object sender, EventArgs e)
        {
            string name = Name.Text;
            int initialValue = int.Parse(InitialValue.Text);
            Color color = Color.FromArgb(CounterColor.Text);

            CounterModel newCounter = new(name, color, initialValue);

            counters.Add(newCounter);

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }

        private async void OnIncrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value++;

            await dataService.SaveCountersAsync(counters);

            CounterCollectionView.ItemsSource = null;
            CounterCollectionView.ItemsSource = counters;
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            CounterModel counter = (CounterModel)((Button)sender).CommandParameter;
            counter.Value--;

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
