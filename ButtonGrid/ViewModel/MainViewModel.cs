using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using ButtonGrid.Model;

namespace ButtonGrid.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private class MyOrders : ObservableCollection<OrderViewModel>
        {
            private readonly Dispatcher _dispatcher;

            public MyOrders(Dispatcher dispatcher)
            {
                _dispatcher = dispatcher;
            }
            
            public void AddOrder(Order o)
            {
                _dispatcher.Invoke(() => InsertItem(0, new OrderViewModel(o)));
            }
        }


        private readonly MyOrders _orders;
        private readonly Manager _manager;

        public ObservableCollection<OrderViewModel> Orders => _orders;

        public MainViewModel()
        {
            _orders = new MyOrders(Dispatcher.CurrentDispatcher);
            _manager = new Manager(o => _orders.AddOrder(o));

            _orders.AddOrder(new Order(123, "Sell", 12400));
            _orders.AddOrder(new Order(124, "Sell", 0));

            _manager.Start();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
