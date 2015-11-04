using System;
using AigisCapture.ViewModel;
using NUnit.Framework;

namespace AigisCapture_Test
{
    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void Xのテスト()
        {
            MainViewModel mainVM = new MainViewModel();
            mainVM.X = "111";
            Assert.AreNotEqual(mainVM.X, "121");
            Assert.AreEqual(mainVM.X, "111");
        }
    }
}
