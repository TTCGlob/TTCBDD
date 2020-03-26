using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using TTCBDD.BaseClasses;

namespace TTCBDD.PageObject
{
    public class CartPage
    {
        //Region WebElement

        private By pageTitle = By.XPath("//h1[contains(normalize-space(),'Shopping cart')]");

        //Region Action
        public bool IsTitled(string title)
        {
            //var clickCart = new PageBase();

            try
            { 
                return ObjectRepository.Driver.FindElement(pageTitle).Text.Contains(title);                
            }
            catch (Exception)
            {
                clickCart.NavigateToCart();
                return ObjectRepository.Driver.FindElement(pageTitle).Text.Contains(title);
               
            }
        }


        //Region Navigation

    }
}
