using PhoneNumbers;

namespace CRMEngSystem.Services.Formatter
{
    public class PhoneNumberFormatter
    {
        public static string FormatPhoneNumber(string phoneNumber)
        {
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var number = phoneNumberUtil.Parse(phoneNumber, null);
                if (!phoneNumberUtil.IsValidNumber(number))
                    return FormatAsLocalNumber(phoneNumber);
                var hasCountryCode = number.HasCountryCode;
                string formattedNumber = phoneNumberUtil.Format(number, PhoneNumberFormat.INTERNATIONAL);
                formattedNumber = ConvertToDesiredFormat(formattedNumber, hasCountryCode);

                return formattedNumber;
            }
            catch (NumberParseException)
            {
                return FormatAsLocalNumber(phoneNumber);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ConvertToDesiredFormat(string formattedNumber, bool hasCountryCode)
        {
            var digitsOnly = new string(formattedNumber.Where(char.IsDigit).ToArray());

            if (hasCountryCode)
            {
                var countryCode = digitsOnly[..^10];
                var nationalNumber = digitsOnly[^10..];

                return $"+{countryCode} ({nationalNumber[..3]}) {nationalNumber.Substring(3, 3)}-{nationalNumber.Substring(6, 2)}-{nationalNumber.Substring(8, 2)}";
            }
            else
            {
                var nationalNumber = digitsOnly.Length > 10 ? digitsOnly.Substring(digitsOnly.Length - 10) : digitsOnly;

                return $"({nationalNumber[..3]}) {nationalNumber.Substring(3, 3)}-{nationalNumber.Substring(6, 2)}-{nationalNumber.Substring(8, 2)}";
            }
        }

        private static string FormatAsLocalNumber(string phoneNumber)
        {
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length == 10)
            {
                return $"({digitsOnly[..3]}) {digitsOnly.Substring(3, 3)}-{digitsOnly.Substring(6, 2)}-{digitsOnly.Substring(8, 2)}";
            }
            return phoneNumber;
        }
    }
}
