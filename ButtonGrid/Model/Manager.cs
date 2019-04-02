using System;
using System.Threading.Tasks;

namespace ButtonGrid.Model
{
    public class Manager
    {
        private readonly Action<Order> _orderAdded;

        public Manager(Action<Order> orderAdded)
        {
            _orderAdded = orderAdded;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                var id = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                var rand = new Random();
                while (true)
                {
                    var order = new Order(id++, "Buy", 200 + 100 * rand.Next(140));
                    _orderAdded?.Invoke(order);
                    await Task.Delay(TimeSpan.FromSeconds(10.0));
                }
            });
        }

    }
}
