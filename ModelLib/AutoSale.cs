using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib
{
    public class AutoSale
    {
        public AutoSale(string name, string address, List<Car> cars)
        {
            Name = name;
            Address = address;
            CarList = cars;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public List<Car> CarList { get; set; }


        public override string ToString()
        {
            return $"Hi i'm {Name}, i'm located at {Address}, and i got {CarList.Count} cars for sale";
        }
    }
}
