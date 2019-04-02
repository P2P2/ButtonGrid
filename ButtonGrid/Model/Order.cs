using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ButtonGrid.Model
{
    public class Order : INotifyPropertyChanged
    {
        public int Id { get; }
        public string Side { get; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                NotifyOfPropertyChange();
            }
        }

        public Order(int id, string side, int quantity)
        {
            Id = id;
            Side = side;
            _quantity = quantity;
        }

        public void Cancel()
        {
            Quantity = 0;
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
