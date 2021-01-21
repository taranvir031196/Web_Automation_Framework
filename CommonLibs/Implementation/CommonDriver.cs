
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;

namespace CommonLibs.Implementation
{
    public class CommonDriver
    {
        public IWebDriver Driver{
            get;
            private set;
        }

        public int PageLoadTimeout { get => pageLoadTimeout; set => pageLoadTimeout = value; }
        public int ElementDetectionTimeout { get => elementDetectionTimeout; set => elementDetectionTimeout = value; }

        private int pageLoadTimeout;

        private int elementDetectionTimeout;

        public CommonDriver(string browserType){

            PageLoadTimeout = 60;
            ElementDetectionTimeout = 10;

            if(browserType.Equals("chrome")){
                Driver = new ChromeDriver();
            } else if(browserType.Equals("edge")){
                Driver = new EdgeDriver();
            }
            else {
                throw new System.Exception("Invalid Browser Type "+ browserType);
            }
            
        }

        public void NavigateToFirstURL(string url){
          url = url.Trim();

        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeout);

        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ElementDetectionTimeout);

        Driver.Url = url;
        
        }

        public void CloseBrowser(){
            if(Driver != null){
                Driver.Close();
            }
        }

        public void CloseAllBrowser(){
            if(Driver != null){
                Driver.Quit();
            }
        }

        public string GetPageTitle(IWebDriver driver){
            Driver = driver;
            return Driver.Title;

        } 
    }
}