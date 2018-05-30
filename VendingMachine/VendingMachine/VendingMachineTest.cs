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
        [TestCase("penny", "$0.00")]
        [TestCase("nickel", "$0.05")]
        [TestCase("dime", "$0.10")]
        [TestCase("quarter", "$0.25")]
        [TestCase("Penny", "$0.00")]
        [TestCase("Nickel", "$0.05")]
        [TestCase("Dime", "$0.10")]
        [TestCase("Quarter", "$0.25")]
        [TestCase("asdfjk", "$0.00")]
        public void InsertCoin(string coin, string expected)
        {
            VendingMachine vm = new VendingMachine();
            Assert.AreEqual(expected, vm.InsertCoin(coin));
        }
        [TestCase(0, "PRICE: $1.00")]
        [TestCase(1, "PRICE: $0.50")]
        [TestCase(2, "PRICE: $0.65")]
        public void NotEnoughMoneyDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();
            Assert.AreEqual(expected, vm.Dispense(p));
        }
        [TestCase(0, "SOLD OUT")]
        [TestCase(1, "SOLD OUT")]
        [TestCase(2, "SOLD OUT")]
        public void NoProductsDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.Dispense(p);
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.Dispense(p);
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.Dispense(p);
            Assert.AreEqual(expected, vm.Dispense(p));
        }
        [TestCase(0, "Thank You!!!")]
        [TestCase(1, "Thank You!!!")]
        [TestCase(2, "Thank You!!!")]
        public void ValidDispense(int p, string expected)
        {
            VendingMachine vm = new VendingMachine();
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            vm.InsertCoin("Quarter");
            Assert.AreEqual(expected, vm.Dispense(p));
        }
    }
}
