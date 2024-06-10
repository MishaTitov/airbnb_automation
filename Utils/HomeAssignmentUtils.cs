namespace HomeAssignment.Utils
{
    internal class HomeAssignmentUtils
    {
        //Utils for Home Assigment
        public static int GetPriceFromString(string text)
        {
            //Get string from result page of prices and return price per night
            string tmpPrice = string.Join("", text.Split("night")[1].Where(Char.IsDigit));
            return Int32.Parse(tmpPrice);
        }

        public static int GetNumberFromString(string text)
        {
            return Int32.Parse(text.Where(Char.IsDigit).ToArray());
        }
    }
}
