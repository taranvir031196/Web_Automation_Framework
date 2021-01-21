
using NUnit.Framework;
using CommonLibs.Implementation;
using Guru99Application.Pages;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using CommonLibs.Utils;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Guru99Tests.tests{

    public class BaseTests{

        public CommonDriver cmnDriver;
        public Guru99LoginPage loginPage;

        private IConfigurationRoot _configuration;

        public ExtentReportUtils extentReportUtils;

        string url;

        ScreenshotUtils screenshot;

        string currentProjectDirectory;

        string currentSolutionDirectory;

        string reportFilename;

        [OneTimeSetUp]
        public void preSetup(){
            string workingDirectory = Environment.CurrentDirectory;

            currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            currentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
           
            reportFilename = currentSolutionDirectory + "/reports/guru99TestReport.html";
            extentReportUtils = new ExtentReportUtils(reportFilename);
            _configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();

        }

        [SetUp]
        public void Setup(){

            extentReportUtils.createATestCaseName("Setup");
            string browserType = _configuration["browserType"]; 
            CommonDriver cmnDriver = new CommonDriver(browserType);
            extentReportUtils.addTestLog(Status.Info, "Browser Type - "+ browserType);


            url = _configuration["baseUrl"];
            extentReportUtils.addTestLog(Status.Info, "Browser url - "+ url);

            cmnDriver.NavigateToFirstURL(url);
            loginPage = new Guru99LoginPage(cmnDriver.Driver);

            screenshot = new ScreenshotUtils(cmnDriver.Driver);
        }

        [TearDown]
        public void TearDown(){

        string currentExecutionTime =DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
        string screenshotFileName = $"{currentSolutionDirectory}/screenshots/test-{currentExecutionTime}.jpeg";


          if(TestContext.CurrentContext.Result.Outcome == ResultState.Failure){

           extentReportUtils.addTestLog(Status.Fail, "One or more steps failed");
           screenshot.CaptureAndSaveScreenshot(screenshotFileName);
           
           extentReportUtils.addScreenshot(screenshotFileName);
           
           } 
           loginPage._driver.Quit();            
        }

        [OneTimeTearDown]
        public void PostCleanUp(){
            extentReportUtils.flushReport();
        }
    }
}