using System.Text;

namespace MC.eSIS.Core.Utilities
{
    public enum AddressFormat
    {
        OneLine,
        WithLineBreaks
    }

    public enum MapProvider
    {
        Bing,
        Google
    }

    public static class AddressDisplay
    {
        public static string FormatAddressWithGoogleMaps(string addressLine1, string addressLine2, string city, string state, string zip, string zipFour, AddressFormat addressFormat = AddressFormat.WithLineBreaks, MapProvider mapProvider = MapProvider.Bing)
        {
            var formattedAddress = FormatAddress(addressLine1, addressLine2, city, state, zip, zipFour, addressFormat);

            var parameters = addressFormat == AddressFormat.OneLine
                ? formattedAddress
                : FormatAddress(addressLine1, addressLine2, city, state, zip, zipFour, AddressFormat.OneLine);

            return mapProvider == MapProvider.Bing ?
                $"<a href=\"http://bing.com/maps?q={parameters}\" target=\"_blank\">{formattedAddress}</a>"
                : $"<a href=\"https://maps.google.com?q={parameters}\" target=\"_blank\">{formattedAddress}</a>";
        }

        public static string FormatAddress(string addressLine1, string addressLine2, string city, string state, string zip, string zipFour, AddressFormat addressFormat = AddressFormat.WithLineBreaks)
        {
            var sb = new StringBuilder();
            var delimiter = addressFormat == AddressFormat.OneLine ? ", " : Constants.HtmlLineBreak;

            if (!string.IsNullOrWhiteSpace(addressLine1))
            {
                sb.Append(addressLine1);
            }

            if (!string.IsNullOrWhiteSpace(addressLine2))
            {
                if (sb.Length > 0)
                {
                    sb.Append(delimiter);
                }
                sb.Append(addressLine2);
            }

            if (!string.IsNullOrWhiteSpace(city) || !string.IsNullOrWhiteSpace(state) ||
                !string.IsNullOrWhiteSpace(zip))
            {
                if (sb.Length > 0)
                {
                    sb.Append(delimiter);
                }
                if (!string.IsNullOrWhiteSpace(city))
                {
                    sb.Append(city);
                    if (!string.IsNullOrWhiteSpace(state) || !string.IsNullOrWhiteSpace(zip))
                    {
                        sb.Append(", ");
                    }
                }
                if (!string.IsNullOrWhiteSpace(state))
                {
                    sb.Append(state);
                }
                if (!string.IsNullOrWhiteSpace(zip))
                {
                    if (!string.IsNullOrWhiteSpace(state))
                    {
                        sb.Append(" ");
                    }
                    if (zip.Length == 9 && string.IsNullOrWhiteSpace(zipFour))
                    {
                        zipFour = zip.Substring(5, 4);
                        zip = zip.Substring(0, 5);
                    }
                    sb.Append(zip);

                    if (!string.IsNullOrWhiteSpace(zipFour))
                    {
                        sb.Append("-");
                        sb.Append(zipFour);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
