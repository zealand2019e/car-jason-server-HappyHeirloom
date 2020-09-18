using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ModelLib
{
    public class Car
    {
        //public Car(string model, string color, string registrationNumber)
        //{
        //    Model = model;
        //    Color = color;
        //    RegistrationNumber = registrationNumber;
        //}

        public Car()
        {
        }


        public string Model { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }


        public override string ToString()
        {
            return $"Your car is a {Color} {Model} with reg.nr {RegistrationNumber}";
        }
    }
}
