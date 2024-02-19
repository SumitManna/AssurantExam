using Microsoft.Extensions.Configuration;

namespace AssurantTest.Unit.Tests.BaseSetup
{
    public abstract class UnitTestBaseSetup
    {
        protected IConfiguration configuration { get; set; }
        protected IConfigurationBuilder configurationBuilder { get; set; }
        protected UnitTestBaseSetup()
        {
            var path = Directory.GetCurrentDirectory() + "//unitTestSetting.json";
            configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(path);
            configuration = configurationBuilder.Build();
        }
    }
}
