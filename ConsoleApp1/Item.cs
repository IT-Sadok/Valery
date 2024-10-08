﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Item(int id, string type, int quantity) : IEquatable<Item>
    {
        Random rnd = new Random();
        public int Id { get { return id; } set { if (value >= 0) id = value; 
                else throw new ArgumentException(String.Format("Id cannot be <0"), "id"); } }
        public string Type { get { return type; } set { type = value; } }
        public int Quantity { get { return quantity; } set { if (value > 0) quantity = value; 
                else throw new ArgumentException(String.Format("Quantity cannot be <=0"), "quantity"); } }
        public override string ToString()
        {
            return "ID: " + Id + "   Type: " + Type + "   Quantity: " + Quantity;
        }
        public bool Equals(Item other)
        {
            if (other == null)
                return false;

            if (this.Id == other.Id)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }



    }
}