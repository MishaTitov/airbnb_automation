using HomeAssignment.Pages;
using Microsoft.Playwright;

namespace HomeAssignment.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class HomeAssignmentTests
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private IBrowserContext _browserContext;
        private IPage _page;

        [OneTimeSetUp]
        public async Task Setup()
        {
            //Setup for playwright, browser, context and page
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            _browserContext = await _browser.NewContextAsync();
            _page = await _browser.NewPageAsync();
        }

        [Test]
        public async Task TestMainPage()
        {
            //Main Page
            MainPage mainPage = new MainPage(_page);
            await mainPage.GotoAsync();
            //Inputs for search, TODO add args
            await mainPage.InputDestination();            
            await mainPage.ClickCheckIn();
            await mainPage.ClickCheckOut();
            await mainPage.ClickGuestsInput();
            await mainPage.ClicSearchButton();
            //Result Page
            ResultPage resultPage = new ResultPage(_page);
            //bool elem = await resultPage.ScanPrices(700);
            ILocator elem = await resultPage.ScanPricesAndGetLocator(700);
            if (elem == null)
                Assert.Pass("NO Available Rooms");
            else
            {
                await elem.ClearAsync();
                //TODO switch pages and handle popup window

                // Get page after a specific action (e.g. clicking a link)
                //var newPage = await _browserContext.RunAndWaitForPageAsync(async () =>
                //{
                //    await elem.ClickAsync();
                //    Console.WriteLine(_page.TitleAsync());
                //    Console.WriteLine(_browserContext.Pages.Count);
                //});
                //await newPage.BringToFrontAsync();
                // Interact with the new page normally
                //Console.WriteLine(await newPage.TitleAsync());

                // Get popup after a specific action (e.g., click)
                //var popup = await newPage.RunAndWaitForPopupAsync(async () =>
                //{
                //    Console.WriteLine("before Clicked popup");
                //    //await newPage.GetByRole(AriaRole.Button).ClickAsync();
                //    Console.WriteLine(newPage.TitleAsync());
                //});
                //// Interact with the popup normally
                //await popup.GetByRole(AriaRole.Button).ClickAsync();
                //Console.WriteLine(await popup.TitleAsync());

                //await _browserContext.WaitForPageAsync();
                //var allPages = _browserContext.Pages.Last();
                //Console.WriteLine(allPages);

                //ApartmentPage apartmentPage = new ApartmentPage(_page);
                //await apartmentPage.CheckCleaningFee();
            }

            await _page.WaitForLoadStateAsync();
            await _browserContext.CloseAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}