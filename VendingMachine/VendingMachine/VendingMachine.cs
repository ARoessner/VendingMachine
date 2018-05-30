using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class main
    {
        public static void Main(String[] args)
        {

        }
    }
    class VendingMachine
    {
        //Product Names
        string[] products;
        //Product Amounts
        int[] numProducts;
        //Price Products
        double[] price;
        //Current Balance
        double changeInserted = 0;

        public VendingMachine()
        {
            this.products = new string[] {"cola","chips","candy" };
            this.numProducts = new int[] { 3, 3, 3 };
            this.price = new double[] { 1, .5, .65 };
        }
        public VendingMachine(string[] products,int[] numProducts, double[] price)
        {
            this.products = products;
            this.numProducts = numProducts;
            this.price = price;
        }
        public string InsertCoin(string coin)
        {
            if ((coin == "nickel") || (coin == "Nickel"))
            {
                changeInserted += .05;
            }
            else if ((coin == "dime") || (coin == "Dime"))
            {
                changeInserted += .10;
            }
            else if ((coin == "quarter") || (coin == "Quarter"))
            {
                changeInserted += .25;
            }
            return changeInserted.ToString("C");
        }

        public string Dispense(int product)
        {
            if(numProducts[product]>0)
            {
                if (changeInserted >= price[product])
                {
                    numProducts[product]--;
                    changeInserted = 0;
                    return "Thank You!!!";
                }
                else
                {
                    return "PRICE: " + price[product].ToString("C");
                }
            }
            else
            {
                return "SOLD OUT";
            }
        }
    }
}
