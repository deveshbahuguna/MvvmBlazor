using System.ComponentModel;

namespace SampleProject.ViewModel
{
    public class CounterViewModel : INotifyPropertyChanged
    {
        private int counter;

        public event PropertyChangedEventHandler? PropertyChanged;
        public string eventnn;

        public int Counter { get => counter; set { 
                counter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Counter"));
            } }

    }
}
