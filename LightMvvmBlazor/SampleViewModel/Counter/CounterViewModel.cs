using System.ComponentModel;
using System.Timers;

namespace SampleViewModel.Counter
{
    public class CounterViewModel : INotifyPropertyChanged
    {
        private int counter;
        private System.Timers.Timer timer;

        public event PropertyChangedEventHandler? PropertyChanged;
       
        public int Counter { get => counter; set { 
                counter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Counter"));
            } }

        public CounterViewModel()
        {
            this.timer = new System.Timers.Timer(1000);
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Counter++;
        }
    }
}
