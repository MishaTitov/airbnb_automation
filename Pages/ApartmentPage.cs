using Microsoft.Playwright;

namespace HomeAssignment.Pages
{
    internal class ApartmentPage
    {
        private readonly IPage _page;
        private readonly ILocator _cleaningFee;

        public ApartmentPage (IPage page)
        {
            Console.WriteLine("constructor");
            _page = page;
            _cleaningFee = _page.Locator("//*[@id=\"site-content\"]/div/div[1]/div[3]/div/div[2]/div/div/div[1]/div/div/div/div/div/div/div/div[3]/div/section/div[1]/div[2]");
        }

        public async Task CheckCleaningFee(int limitFee = 500)
        {
            //Check cleaning fee according to limit on page
            Console.WriteLine(await _cleaningFee.TextContentAsync());
            var el = _page.Locator("//*[@id=\"site-content\"]/div/div[1]/div[3]/div/div[2]/div/div/div[1]/div/div/div/div/div/div/div/div[3]/div/section/div[1]/div[2]");
            Console.WriteLine(await el.TextContentAsync());
        }
    }
}
