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
        private IBrowserContext _context;
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
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [Test]
        public async Task TestMainPage()
        {
            //Main Page
            MainPage mainPage = new MainPage(_page);
            await mainPage.GotoAsync();
            await _page.WaitForLoadStateAsync();

            //Handle popup if it appear
            if (await _page.GetByRole(AriaRole.Button, new() { Name = "Close" }).IsVisibleAsync())
                await _page.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();


            //Inputs for search, TODO add args
            await mainPage.InputDestination("Amsterdam");            
            await mainPage.ClickCheckIn();
            await mainPage.ClickCheckOut();
            await mainPage.ClickGuestsInput(3,1);
            await mainPage.ClicSearchButton();

            //Result Page
            ResultPage resultPage = new ResultPage(_page);
            ILocator? elem = await resultPage.ScanPricesAndGetLocator(700);
            if (elem == null)
                Assert.Pass("NO Available Rooms");
            else
            {
                // Get page after a specific action (e.g. clicking a link)
                var newPage = await _context.RunAndWaitForPageAsync(async () =>
                {
                    await elem.ClickAsync();
                });
                await newPage.BringToFrontAsync();             

                //Apartment page
                ApartmentPage apartmentPage = new ApartmentPage(newPage);
                var cleaningFee = await apartmentPage.CheckCleaningFee();

                //Check if cleaning fee less than 500
                Assert.That(cleaningFee, Is.AtMost(500), "Cleaning Fee is greater than 500");

                //Screenshots
                await newPage.ScreenshotAsync(new()
                {
                    Path = "./screenshots/screenshot.png",
                    FullPage = true,
                });
                var bytes = await newPage.ScreenshotAsync();
                Console.WriteLine(Convert.ToBase64String(bytes));

                await newPage.WaitForLoadStateAsync();
            }

            await _context.CloseAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
