using Microsoft.Playwright;

namespace HomeAssignment.Pages
{
    internal class MainPage
    {
        private readonly IPage _page;
        private readonly ILocator _destinationInput;
        private readonly ILocator _checkInInput;
        private readonly ILocator _checkOutInput;
        private readonly ILocator _guestsInput;
        private readonly ILocator _searchButton;

        public MainPage(IPage page)
        {
            _page = page;
            _destinationInput = _page.GetByTestId("structured-search-input-field-query");
            _searchButton = _page.GetByTestId("structured-search-input-search-button");
            _checkInInput = _page.GetByTestId("structured-search-input-field-split-dates-0");
            _checkOutInput = _page.GetByTestId("structured-search-input-field-split-dates-1");
            _guestsInput = _page.GetByTestId("structured-search-input-field-guests-button");
        }

        public async Task GotoAsync()
        {
            //Go to URL
            await _page.GotoAsync("https://www.airbnb.com/");
        }

        public async Task InputDestination(string destination = "Tel Aviv")
        {
            //Input destination
            await _destinationInput.FillAsync(destination);
        }

        public async Task ClicSearchButton()
        {
            //Click on Search Button
            await _searchButton.ClickAsync();
            await _page.Locator("site-content").IsVisibleAsync();
        }


        public async Task ClickCheckIn(string checkInDate = "calendar-day-05/30/2024")
        {
            //Click on relevent CheckIn date,TODO search for dates
            await _checkInInput.ClickAsync();
            await _page.GetByTestId(checkInDate).WaitForAsync();
            await _page.GetByTestId(checkInDate).ClickAsync();
        }

        public async Task ClickCheckOut(string checkOutDate = "calendar-day-06/10/2024")
        {
            //Click on relevent CheckOut date,TODO search for dates
            await _checkOutInput.ClickAsync();
            await _page.GetByTestId(checkOutDate).WaitForAsync();
            await _page.GetByTestId(checkOutDate).ClickAsync();
        }

        public async Task ClickGuestsInput(int numAdults = 2, int numChildren = 2)
        {
            //Click on on relevant count of guest: children and adults
            await _guestsInput.ClickAsync();

            for (int i = 0; i < numAdults; i++) 
                await _page.GetByTestId("stepper-adults-increase-button").ClickAsync();
            
            for (int i = 0;i < numChildren; i++)
                await _page.GetByTestId("stepper-children-increase-button").ClickAsync();
        }

        //TODO handle with POPUP Window
    }
}
