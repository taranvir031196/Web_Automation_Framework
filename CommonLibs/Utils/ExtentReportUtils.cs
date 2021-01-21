
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CommonLibs.Utils{

    public class ExtentReportUtils{

        private ExtentHtmlReporter extentHtmlReporter;
        
        private ExtentReports extentReports;

        private ExtentTest extentTest;

        public ExtentReportUtils(string htmlReportFileName){

            extentHtmlReporter = new ExtentHtmlReporter(htmlReportFileName);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(extentHtmlReporter);
        }

        public void createATestCaseName(string testcaseName){

            extentTest = extentReports.CreateTest(testcaseName);

        }

        public void addTestLog(Status status, string comment){
            extentTest.Log(status, comment);
        }

        public void flushReport(){
            extentReports.Flush();
        }

        public void addScreenshot(string screenshotFileName){

            extentTest.AddScreenCaptureFromPath(screenshotFileName);
        }



    }

}