﻿using HomeAssignment.Utils;
using Microsoft.Playwright;

namespace HomeAssignment.Pages
{
    internal class ResultPage
    {
        private readonly IPage _page;
        private readonly ILocator _nextButton;

        public ResultPage(IPage page)
        {
            _page = page;
            //REFACTOR
            _nextButton = _page.Locator("//*[@id=\"site-content\"]/div/div[3]/div/div/div/nav/div/a[5]");
        }

        public async Task ClickNextButton()
        {
            // Next button in result page
            await _nextButton.ClickAsync();
        }

        public async Task<bool> ScanPrices(int priceLimit = 500)
        {
            // Scan for prices with price limit and click on first relevant apartament
            bool findFlag = false;
            int price;
            var el = _page.GetByTestId("price-availability-row").Nth(0);
            for (int i = 1; i < 18; i++)
            {
                var text = await el.TextContentAsync();
                Console.WriteLine($"[{i}] {text}");
                if (text != null)
                    price = HomeAssignmentUtils.GetPriceFromString(text);
                else
                    price = priceLimit + 1;
                Console.WriteLine(price);
                
                if (price < priceLimit)
                {
                    findFlag = true;
                    await _page.GetByTestId("card-container").Filter(new() { HasText = price.ToString() }).ClickAsync();
                    //await _page.GetByTestId("card-container").Filter(new() { Has = el }).ClickAsync();
                    break;
                }

                el = _page.GetByTestId("price-availability-row").Nth(i);
            }
            return findFlag;
        }

        public async Task<ILocator?> ScanPricesAndGetLocator(int priceLimit = 500)
        {
            // Scan for prices with price limit and click on first relevant apartament
            var el = _page.GetByTestId("price-availability-row").Nth(0);
            int price;
            for (int i = 1; i < 18; i++)
            {
                var text = await el.TextContentAsync();
                if (text != null)
                    price = HomeAssignmentUtils.GetPriceFromString(text);
                else
                    price = priceLimit + 1;

                if (price < priceLimit)
                {
                    return _page.GetByTestId("card-container").Filter(new() { HasText = price.ToString() });
                    //return  _page.GetByTestId("card-container").Filter(new() { Has = el });                    
                }

                el = _page.GetByTestId("price-availability-row").Nth(i);
            }
            return null;
        }

        //TODO handle with POPUP Window
    }
}
