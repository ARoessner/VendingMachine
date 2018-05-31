using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    [TestFixture]
    class VendingMachineTest
    {
        //TEST InsertCoin Method
        [TestCase("penny", "$0.00")]//test insert penny --NOT ALLOWED
        [TestCase("nickel", "$0.05")]//test insert nickel
        [TestCase("dime", "$0.10")]//test insert dime
        [TestCase("quarter", "$0.25")]//test insert quarter
        [TestCase("Penny", "$0.00")]//test insert Penny --NOT ALLOWED
        [TestCase("Nickel", "$0.05")]//test insert Nickel
        [TestCase("Dime", "$0.10")]//test insert Dime
        [TestCase("Quarter", "$0.25")]//test insert Quarter
        [TestCase("asdfjk", "$0.00")]//test insert invalid string --NOT ALLOWED
        public void InsertCoin(string coin, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            Assert.AreEqual(expected, vm.InsertCoin(coin));//TEST
        }

        //TEST Dispense Method --Not Enough Money Inserted but Product exists
        [TestCase(0, "PRICE: $1.00")] //test dispense item 0
        [TestCase(1, "PRICE: $0.50")] //test dispense item 1
        [TestCase(2, "PRICE: $0.65")] //test dispense item 2
        public void NotEnoughMoneyDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            Assert.AreEqual(expected, vm.Dispense(p));//TEST
        }
        //TEST Dispense Method --Product is sold out
        [TestCase(0, "SOLD OUT")] //test dispense item 0
        [TestCase(1, "SOLD OUT")] //test dispense item 1
        [TestCase(2, "SOLD OUT")] //test dispense item 2
        public void NoProductsDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            //Empty Vending Machine
                //Insert Coins
                vm.InsertCoin("Quarter");//$0.25
                vm.InsertCoin("Quarter");//$0.50
                vm.InsertCoin("Quarter");//$0.75
                vm.InsertCoin("Quarter");//$1.00
                vm.Dispense(p);//Dispense product "Thank You!!!"
                //Insert Coins
                vm.InsertCoin("Quarter");//$0.25
                vm.InsertCoin("Quarter");//$0.50
                vm.InsertCoin("Quarter");//$0.75
                vm.InsertCoin("Quarter");//$1.00
                vm.Dispense(p);//Dispense product "Thank You!!!"
                //Insert Coins
                vm.InsertCoin("Quarter");//$0.25
                vm.InsertCoin("Quarter");//$0.50
                vm.InsertCoin("Quarter");//$0.75
                vm.InsertCoin("Quarter");//$1.00
                vm.Dispense(p);//Dispense product "Thank You!!!"
            //End Empty Vending Machine
            Assert.AreEqual(expected, vm.Dispense(p));//TEST
        }

        //TEST Dispense --Product is there and enough money is inserted
        [TestCase(0, "Thank You!!!")] //test dispense item 0
        [TestCase(1, "Thank You!!!")] //test dispense item 1
        [TestCase(2, "Thank You!!!")] //test dispense item 2
        public void ValidDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            //Insert Change
            vm.InsertCoin("Quarter");//$0.25
            vm.InsertCoin("Quarter");//$0.50
            vm.InsertCoin("Quarter");//$0.75
            vm.InsertCoin("Quarter");//$1.00
            Assert.AreEqual(expected, vm.Dispense(p));//TEST
        }
        
        //TEST DispenseChange Method --All Coins Available
        [TestCase(.4, "1 Quarters, 1 Dimes and 1 Nickels were dispensed")]//test .40 cents
        public void returnChangeAll(decimal change, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            vm.InsertCoin("Quarter");//Insert Quarter
            vm.InsertCoin("Dime");//Insert Dime
            vm.InsertCoin("Nickel");//Insert Nickel
            Assert.AreEqual(expected, vm.DispenseChange(change));//TEST
        }

        //Test DispenseChange Method --No Quarters Available
        [TestCase(.4, "3 Dimes and 2 Nickels were dispensed")]//test .40 cents
        public void returnChangeNoQuarters(decimal change, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            vm.InsertCoin("Dime");//Insert Dime
            vm.InsertCoin("Dime");//Insert Dime
            vm.InsertCoin("Dime");//Insert Dime
            vm.InsertCoin("Nickel");//Insert Nickel
            vm.InsertCoin("Nickel");//Insert Nickel
            Assert.AreEqual(expected, vm.DispenseChange(change));//TEST
        }

        //Test DispenseChange Method --No Dimes Available
        [TestCase(.4, "1 Quarters and 3 Nickels were dispensed")]//test .40 cents
        public void returnChangeNoDimes(decimal change, string expected)
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            vm.InsertCoin("Quarter");//Insert Quarter
            vm.InsertCoin("Nickel");//Insert Nickel
            vm.InsertCoin("Nickel");//Insert Nickel
            vm.InsertCoin("Nickel");//Insert Nickel
            Assert.AreEqual(expected, vm.DispenseChange(change));//TEST
        }

        //TEST hasChange method --Vending Machine has change available
        [Test]
        public void hasChangeTrue()
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            vm.InsertCoin("Nickel");//Insert Nickel
            vm.InsertCoin("Dime");//Insert Dime
            vm.InsertCoin("Dime");//Insert Dime
            Assert.AreEqual(true, vm.HasChange());//TEST
        }

        //TEST hasChange method --Vending Machine does not have change available
        [Test]
        public void hasChangeFalse()
        {
            VendingMachine vm = new VendingMachine();//Stage Vending Machine
            Assert.AreEqual(false, vm.HasChange());//TEST
        }
    }
}
