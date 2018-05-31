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
        // V CHANGE variables V
        int numQuarters = 0;//number of Quarters
        int numNickels = 0; //number of Nickles
        int numDimes = 0;   //number of Dimes

        //Default Constructor
        public VendingMachine()
        {
            this.products = new string[] {"cola","chips","candy" }; //Initialize Product Names
            this.numProducts = new int[] { 3, 3, 3 };               //Initialize Product Quantities
            this.price = new double[] { 1, .5, .65 };               //Initialize Product Prices
        }

        //Constructor
        public VendingMachine(string[] products,int[] numProducts, double[] price)
        {
            this.products = products;       //Initialize Product Names
            this.numProducts = numProducts; //Initialize Product Quantities
            this.price = price;             //Initialize Product Prices
        }
        //Insert Coins Method
        public string InsertCoin(string coin)
        {
            if ((coin == "nickel") || (coin == "Nickel"))//Nickel
            {
                changeInserted += .05;  //Increment changeInserted
                numNickels++;           //Increment numNickles
            }
            else if ((coin == "dime") || (coin == "Dime"))//Dime
            {
                changeInserted += .10;  //Increment changeInserted
                numDimes++;             //Increment numDimes
            }
            else if ((coin == "quarter") || (coin == "Quarter"))//Quarter
            {
                changeInserted += .25;  //Increment changeInserted
                numQuarters++;          //Increment numQuarters
            }
            return changeInserted.ToString("C");//return changeInserted formatted as Currency
        }

        //Dispense Method
        public string Dispense(int product)
        {
            if(numProducts[product]>0)//Product is in stock
            {
                if (changeInserted >= price[product])//inserted enough change
                {
                    numProducts[product]--;//decrease product quantity
                    //DispenseChange(changeInserted - price[product]);//dispense proper change
                    changeInserted = 0;//reset change inserted
                    return "Thank You!!!";//Send Thank You!!! Message
                }
                else//not enough change inserted
                {
                    return "PRICE: " + price[product].ToString("C");//Send Product Price Message
                }
            }
            else//Product out of stock
            {
                return "SOLD OUT";//Send SOLD OUT message
            }
        }

        //HasChange Method
        /*****Assumptions*****
         * Vending Machine does not accept pennies 
         * Products cost a multiple of 5 cents
         * Vending Machine Does not accept dollar bills
         * Largest valid payment option is a Quarter
         * Customer can only overpay by 20 cents
         */ 
        public bool HasChange()
        {
            if ((numDimes >= 2) && (numNickels >= 1))//Vending Machine contains at least 1 nickel and 2 dimes
                return true;
            else//Vending Machine does not contain at least 1 nickel and 2 dimes
                return false;
        }

        //DispenseChange Method
        public string DispenseChange(decimal change)
        {
            int numQuarters = 0;//numQuarters to be dispensed
            int numNickels = 0; //numNickles to be dispensed
            int numDimes = 0;   //numDimes to be dispensed
            string value = "";  //Output string
            if (change > 0) //Vending Machine owes change
            {
                while (change > 0) //while change owed > 0
                {
                    if ((change >= .25m) && (this.numQuarters > 0))//change owed >$0.25
                    {
                        this.numQuarters--; //remove a quarter from vending machine
                        numQuarters++;      //add a quarter to be dispensed
                        change -= 0.25m;    //decrease change owed
                    }
                    else if ((change >= .10m) && (this.numDimes > 0))//change owed >$0.10
                    {
                        this.numDimes--;//remove a dime from vending machine
                        numDimes++;     //add a dime to be dispensed
                        change -= .10m; //decrease change owed
                    }
                    else if ((change >= .05m) && (this.numNickels > 0))//change owed >$0.05
                    {
                        this.numNickels--;  //remove a nickel from vending machine
                        numNickels++;       //add a nickel to be dispensed
                        change -= .05m;     //decrease change owed
                    }
                }
                //Begin Building Output string
                if (numQuarters != 0)//There are Quarters to be dispensed
                {
                    value += numQuarters + " Quarters";//Add Quarters to output string
                }
                if (numDimes != 0)//There are Dimes to be dispensed
                {
                    if (value != "")//Quarters were added
                    {
                        value += ", ";//Add comma between quarters and dimes
                    }
                    value += numDimes + " Dimes";//Add Dimes to output string
                }
                if (numNickels != 0)//There are Nickels to be dispensed
                {
                    if (value != "")//Quarters and/or Dimes were added
                    {
                        value += " and ";//add "and" between quarters/dimes and nickles
                    }
                    value += numNickels + " Nickels";//Add Nickles to output string
                }
                value += " were dispensed";//finished building output string
                return value;//return Output String
            }
            return "";//return empty string
        }
    }
}
