using PhoneNumbers;
using System.Text.RegularExpressions;

namespace CRMEngSystem.Services.Formatter
{
    public class PhoneNumberFormatter
    {
        private static string CleanPhoneNumber(string phoneNumber)
        {
            string cleanedNumber = Regex.Replace(phoneNumber, @"[\s\-\(\)\+]", "");
            return cleanedNumber;
        }
        public static bool IsValidPhoneNumber(string searchPhoneNumber, string currentPhoneNumber) 
        {
            if(searchPhoneNumber.Length < 6)
                return false;

            searchPhoneNumber = CleanPhoneNumber(searchPhoneNumber);
            currentPhoneNumber = CleanPhoneNumber(currentPhoneNumber);

            return currentPhoneNumber.Contains(searchPhoneNumber) || currentPhoneNumber.Contains(searchPhoneNumber[1..]) || currentPhoneNumber.Contains(searchPhoneNumber[2..]) || currentPhoneNumber.Contains(searchPhoneNumber[3..]);
        }
    }
}
