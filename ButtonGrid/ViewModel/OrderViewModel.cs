using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ButtonGrid.Model;

namespace ButtonGrid.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly Order _order;

        public int Id => _order.Id;
        public string Side => _order.Side;
        public int Quantity => _order.Quantity;
        public bool CanCancel => _order.Quantity > 0;
        public ICommand CancelCommand { get; }

        public OrderViewModel(Order order)
        {
            _order = order;
            _order.PropertyChanged += (_, e) =>
            {
                switch (e.PropertyName)
                {
                    case "Quantity":
                        NotifyOfPropertyChange(e.PropertyName);
                        NotifyOfPropertyChange(nameof(CanCancel));
                        break;

                    default:
                        NotifyOfPropertyChange(e.PropertyName);
                        break;
                }
            };

            CancelCommand = new MyCommand(this);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private class MyCommand : ICommand
        {
            private readonly OrderViewModel _order;

            public MyCommand(OrderViewModel order)
            {
                _order = order;
            }

            public bool CanExecute(object parameter)
            {
                return _order.CanCancel;
            }

            public void Execute(object parameter)
            {
                _order._order.Cancel();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
