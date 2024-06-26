﻿using HomeAssignment.Utils;
using Microsoft.Playwright;

namespace HomeAssignment.Pages
{
    internal class ApartmentPage
    {
        private readonly IPage _page;
        private readonly ILocator _cleaningFee;

        public ApartmentPage (IPage page)
        {
            _page = page;
            _cleaningFee = _page.Locator("div._tr4owt").Filter(new() { HasText = "Cleaning fee"});
        }

        public async Task<int?> CheckCleaningFee()
        {
            //Return cleaning fee
            var tmpText = await _cleaningFee.TextContentAsync();
            if (tmpText != null)
            {
                return HomeAssignmentUtils.GetNumberFromString(tmpText);
            }
            return null;
        }
    }
}
