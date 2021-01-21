using AventStack.ExtentReports;
using NUnit.Framework;

namespace Guru99Tests.tests
{
    public class LoginPageTests : BaseTests
    {
       
        [Test]
        public void VerifyLoginTest()
        {
           
           extentReportUtils.createATestCaseName("Verify Login Test");
           extentReportUtils.addTestLog(Status.Info, "Performing Login");

           loginPage.LoginToApplication("mngr304276", "vasAran");

            string sExpectedTitle="Guru99 Bank Manager HomePage";

            string sActualTitle=cmnDriver.GetPageTitle(loginPage._driver);

           Assert.AreEqual(sExpectedTitle, sActualTitle);
        }
    }
}