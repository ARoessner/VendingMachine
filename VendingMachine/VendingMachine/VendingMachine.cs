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
            string coinInput = "";
            Console.WriteLine("Welcome to the Vending Machine!!!");
            Console.WriteLine("This Machine only accepts Nickles, Dimes, and Quarters");
            Console.WriteLine("To insert change just type out the name of the coin you wish to enter");
            VendingMachine vm = new VendingMachine();
            do
            {
                Console.WriteLine("Type \"vend\" to begin vending process or \"exit\" to leave");
                Console.WriteLine(vm.CheckDisplay());
                coinInput = Console.ReadLine();
                if(coinInput.ToLower() == "vend")
                {
                    Console.WriteLine("Please enter a number 0 - " + (vm.products.Length-1));
                    int prodID = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(vm.Dispense(prodID));
                }
                else
                {
                    Console.WriteLine(vm.InsertCoin(coinInput));
                }

            } while (coinInput != "exit");
        }
    }
    class VendingMachine
    {
        //Product Names
        public string[] products;
        //Product Amounts
        int[] numProducts;
        //Price Products
        decimal[] price;
        //Current Balance
        decimal changeInserted = 0;
        // V CHANGE variables V
        int numQuarters = 0;//number of Quarters
        int numNickels = 0; //number of Nickles
        int numDimes = 0;   //number of Dimes
        // String to be displayed on vending machine
        string onDisplay = "";

        //Default Constructor
        public VendingMachine()
        {
            this.products = new string[] {"cola","chips","candy" }; //Initialize Product Names
            this.numProducts = new int[] { 3, 3, 3 };               //Initialize Product Quantities
            this.price = new decimal[] { 1m, .5m, .65m };               //Initialize Product Prices
        }

        //Constructor
        public VendingMachine(string[] products,int[] numProducts, decimal[] price)
        {
            this.products = products;       //Initialize Product Names
            this.numProducts = numProducts; //Initialize Product Quantities
            this.price = price;             //Initialize Product Prices
        }

        //Check Display Method
        public string CheckDisplay()
        {
            if ((onDisplay == "INSERT COIN") || (onDisplay == "EXACT CHANGE ONLY"))//Alternate Display
                onDisplay = changeInserted.ToString("C");
            else if (HasChange())//Display when Vending Machine can make change
                onDisplay = "INSERT COIN";
            else//Display when Vending Machine cannot make change
                onDisplay = "EXACT CHANGE ONLY";
            return onDisplay;//return onDisplay
        }
        //Insert Coins Method
        public string InsertCoin(string coin)
        {
            if ((coin == "nickel") || (coin == "Nickel"))//Nickel
            {
                changeInserted += .05m;  //Increment changeInserted
                numNickels++;           //Increment numNickles
            }
            else if ((coin == "dime") || (coin == "Dime"))//Dime
            {
                changeInserted += .10m;  //Increment changeInserted
                numDimes++;             //Increment numDimes
            }
            else if ((coin == "quarter") || (coin == "Quarter"))//Quarter
            {
                changeInserted += .25m;  //Increment changeInserted
                numQuarters++;          //Increment numQuarters
            }
            onDisplay = changeInserted.ToString("C");//set onDisplay to changeInserted formatted as Currency
            return onDisplay; //return onDisplay
        }

        //Dispense Method
        public string Dispense(int product)
        {
            if ((product < products.Length) && (product >= 0))//product number is valid
            {
                if (numProducts[product] > 0)//Product is in stock
                {
                    if (changeInserted >= price[product])//inserted more than enough change
                    {
                        if (changeInserted == price[product])
                        {
                            numProducts[product]--;//decrease product quantity
                            onDisplay = "Thank You!!!";//Send Thank You!!! Message
                        }
                        else
                        {
                            if (HasChange())
                            {
                                numProducts[product]--;//decrease product quantity
                                Console.WriteLine(DispenseChange(changeInserted - price[product]));//dispense proper change
                                onDisplay = "Thank You!!!";//Send Thank You!!! Message
                            }
                            else
                            {
                                Console.WriteLine(DispenseChange(changeInserted));//dispense input change
                                onDisplay = "EXACT CHANGE ONLY";//send EXACT CHANGE message
                            }
                        }
                        changeInserted = 0;//reset change inserted
                    }
                    else//not enough change inserted
                    {
                        onDisplay = "PRICE: " + price[product].ToString("C");//Send Product Price Message
                    }
                }
                else//Product out of stock
                {
                    onDisplay = "SOLD OUT";//Send SOLD OUT message
                }
            }
            else//Invalid Input
            {
                onDisplay = "INVALID PRODUCT NUMBER";//Send INVALID PRODUCT message
            }
            return onDisplay;//return onDisplay
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
            StringBuilder value = new StringBuilder("");  //Output string
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
                    value.Append(numQuarters + " Quarters");//Add Quarters to output string
                }
                if (numDimes != 0)//There are Dimes to be dispensed
                {
                    if (value.ToString() !="" )//Quarters were added
                    {
                        value.Append(", ");//Add comma between quarters and dimes
                    }
                    value.Append(numDimes + " Dimes");//Add Dimes to output string
                }
                if (numNickels != 0)//There are Nickels to be dispensed
                {
                    if (value.ToString() != "")//Quarters and/or Dimes were added
                    {
                        value.Append(" and ");//add "and" between quarters/dimes and nickles
                    }
                    value.Append(numNickels + " Nickels");//Add Nickles to output string
                }
                value.Append(" were dispensed");//finished building output string
                return value.ToString();//return Output String
            }
            return "";//return empty string
        }
    }
}
