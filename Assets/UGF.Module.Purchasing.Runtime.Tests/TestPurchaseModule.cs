using System.Threading.Tasks;
using NUnit.Framework;
using UGF.Application.Runtime;

namespace UGF.Module.Purchasing.Runtime.Tests
{
    public class TestPurchaseModule
    {
        [Test]
        public async Task PurchaseAsync()
        {
            var launcher = new ApplicationLauncherResources("Launcher");

            await launcher.CreateAsync();

            var module = launcher.Application.GetModule<IPurchaseModule>();

            bool result = await module.PurchaseAsync("047297d90e59e2c4c848667d9968f8a0");

            Assert.True(result);

            launcher.Destroy();
        }
    }
}
