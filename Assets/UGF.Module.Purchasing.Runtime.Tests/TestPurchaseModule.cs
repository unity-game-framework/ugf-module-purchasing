using System.Threading.Tasks;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

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

            bool result = await module.PurchaseAsync(new GlobalId("047297d90e59e2c4c848667d9968f8a0"));
            bool result2 = await module.PurchaseAsync(new GlobalId("8df1cccce236a854493bd229b36511c1"));

            Assert.True(result);
            Assert.True(result2);

            launcher.Destroy();
        }
    }
}
