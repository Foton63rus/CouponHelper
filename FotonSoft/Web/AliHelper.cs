using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using FotonSoft.Entities;

namespace FotonSoft.Web
{
    public class AliHelper
    {
        private IWebDriver WB;
        private string URL_AliStartPage     => "https://ru.aliexpress.com";
        private string URL_AliHomePage      => "https://home.aliexpress.com/index.htm";
        private string URL_AliMyOrdersPage  => "https://trade.aliexpress.com/orderList.htm";
        private string URL_AliLoginPage     => "https://login.aliexpress.com/?flag=1&return_url=http%3A%2F%2Fhome.aliexpress.com%2Findex.htm%3Fspm%3D2114.11020108.1000002.6.UUq7GF%26tracelog%3Dws_topbar";
        private string URL_AliLogoutPage    => "https://login.aliexpress.com/xman/xlogout.htm?return_url=https%3A%2F%2Fhome.aliexpress.com%2Findex.htm";
        Random rnd;
        public AliHelper()
        {
            if (WB == null) WB = new OpenQA.Selenium.Chrome.ChromeDriver();
            //Logins(accessDict());
            //WB.Manage().Window.Maximize();
            rnd = new Random();
        }
        ~AliHelper()
        {
            Quit();
        }
        /// <summary>
        /// Закрывает браузер Google Chrome
        /// </summary>
        public void Quit()
        {
            WB.Quit();
        }
        /// <summary>
        /// Алгоритм извлечения данных
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        public void dataGetter(string login, string pass)
        {
            authorize(login, pass);
            wait(2, 4);
            closeBoxAfterLogin();
            wait(2, 4);
            goToAliMyOrdersPage();
            wait(2, 4);
            //
            // этот метод еще не рабочий
            getOrderData();
            //
            wait(2, 4);
            logout();
            
        }
        /// <summary>
        /// авто - вход на али
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        public void authorize(string login, string pass)
        {
            WB.Navigate().GoToUrl(URL_AliLoginPage);
            IWebElement frame_login = getIWebElement(By.TagName("iframe"));
            WB.SwitchTo().Frame(frame_login);
            IWebElement txt_login = getIWebElement(By.Name("loginId"));
            txt_login.Clear();
            txt_login.SendKeys(login);
            IWebElement txt_pass = getIWebElement(By.Name("password"));
            txt_pass.SendKeys(pass);
            txt_pass.SendKeys(OpenQA.Selenium.Keys.Return);
        }

        /// <summary>
        /// Закрытие окна приветсвия после удачного логина
        /// </summary>
        public void closeBoxAfterLogin()
        {
            IWebElement btnBoxClose = getIWebElement(By.Id("neverMind"));
            if(btnBoxClose!=null)
                btnBoxClose.Click();
        }
        /// <summary>
        /// переход в мои заказы
        /// </summary>
        public void goToAliMyOrdersPage()
        {
            WB.Navigate().GoToUrl(URL_AliMyOrdersPage);
        }
        /// <summary>
        /// собирание данных о товарах
        /// </summary>
        private List<OrderInfo> getOrderData()
        {
            List<OrderInfo> OrderInfoList = new List<OrderInfo>() { };
            List<IWebElement> orderheads = WB.FindElements(By.CssSelector("tbody > tr.order-head")).ToList();
            foreach (IWebElement i in orderheads)
            {
                IWebElement storeName = getIWebElement(i, By.CssSelector("td.store-info > p.first-row > span.info-body"));
                IWebElement storeLink = getIWebElement(i, By.CssSelector("td.store-info > p.second-row > a"));

                StoreInfo si = new StoreInfo(storeName.Text, storeLink.GetAttribute("href"));

                string oid = (getIWebElement(i, By.CssSelector("td.order-info > p.first-row > span.info-body"))).Text;
                IWebElement orderTime = getIWebElement(i, By.CssSelector("td.order-info > p.second-row > span.info-body"));
                string oc = (getIWebElement(i, By.CssSelector("td.order-amount > div.amount-body > p.amount-num"))).Text;

                OrderInfoList.Add(new OrderInfo(oid, orderTime.Text, oc, si));
            }
            return OrderInfoList;
        }
        private void getProductData(List<StoreInfo> StoreInfoList)
        {
            List<ProductInfo> ProductInfoList = new List<ProductInfo>() { };
            List<IWebElement> productTBodies = WB.FindElements(By.CssSelector("tbody > tr.order-body")).ToList();
            foreach (IWebElement i in productTBodies)
            {
                IWebElement info = getIWebElement(i, By.CssSelector("td.product-sets > div.product-right > p.product-snapshot > a"));
                string productTitle = info.GetAttribute("title");
                string productid = info.GetAttribute("productid");
                string productSnapshotLink = info.GetAttribute("href");

                List<IWebElement> productAmount = i.FindElements(By.CssSelector("td.product-sets > div.product-right > p.product-amount > span")).ToList();
                string amount = productAmount[0].Text;
                string count = productAmount[1].Text;

                List<IWebElement> productProperty = i.FindElements(By.CssSelector("td.product-sets > div.product-right > p.product-proprty > span > span.val")).ToList();
                string property = productProperty[0].Text +" + "+ productProperty[1].Text;

                IWebElement order_action = getIWebElement(i, By.CssSelector("td.order-action"));
                string orderid = order_action.GetAttribute("orderid");

                //StoreInfo si = new StoreInfo(storeName.Text, storeLink.GetAttribute("href"));
            }
        }
        /// <summary>
        /// Открыть домашнюю страницу и закрыть окно приветствия
        /// </summary>
        public void openHomePage()
        {
            WB.Navigate().GoToUrl(URL_AliHomePage);
            if (getIWebElement(By.ClassName("ui-window-close")) != null) closeBoxAfterLogin();
        }
        /// <summary>
        /// Закрыть текущую учетную запись
        /// </summary>
        public void logout()
        {
            WB.Navigate().GoToUrl(URL_AliLogoutPage);
        }

        private void wait(int min, int max)
        {
            Thread.Sleep(1000 * rnd.Next(min, max));
        }
        public Dictionary<string, string> accessDict()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\access.txt";
            if (File.Exists(path))
            {
                Dictionary<string, string> tmpDict = new Dictionary<string, string>();
                try
                {
                    string[] lpPairs = File.ReadAllLines(path);
                    foreach (string str in lpPairs)
                    {
                        string[] lp = str.Split("\t".ToCharArray());
                        tmpDict.Add(lp[0], lp[1]);
                    }
                }
                catch (Exception ex)
                {
                    //return null;
                }
                return tmpDict;
            }
            else
            {
                try
                {
                    File.Create(path);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(@"Class: AliAutoLoginer => public Dictionary<string, string> accessDict() => File.Create('path');  " + ex.Message);
                }
                return null;
            }

        }
        /// <summary>
        /// Получение IWebElement
        /// Костыль, избавляющий от вылетов при прямом запросе на IWebElement, 
        /// которого нет или еще не загружен
        /// (перехват исключения тут бесполезен)
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        private IWebElement getIWebElement( By by )
        {
            List<IWebElement> founded = WB.FindElements(by).ToList();
            return founded.Count > 0 ? founded[0] : null;
        }
        private IWebElement getIWebElement(IWebElement element, By by)
        {
            List<IWebElement> founded = element.FindElements(by).ToList();
            return founded.Count > 0 ? founded[0] : null;
        }
    }
}
