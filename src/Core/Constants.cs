// ReSharper disable InconsistentNaming

namespace MC.eSIS.Core
{
    public static class Constants
    {
        public const string ApiRequestKeyHeaderName = "X-API-Key";
        public const string ApiRequestClientNameHeaderName = "X-API-ClientName";
        public const string ApiRequestClientInProductionHeaderName = "X-API-ClientInProduction";
        public const string ApiGenericErrorMessage = "An unexpected error has occurred and been logged. If this problem persists, please contact your system administrator.";

        public const string UnitedStatesZipCodeRegEx = @"^[0-9]{5}([- /]?[0-9]{4})?$";                 //matches 43223, 43223-3827, 432233827
        public const string CanadaPostalCodeRegEx = @"^[A-Za-z0-9]{3}[- /]?[A-Za-z0-9]{3}?$";    //matches T2X 1V4, T2X-1V4, T2X1V4
        public const string PhoneNumberRegEx = @"^(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";

        public const string HtmlLineBreak = "<br/>";
        public const string HtmlBeginBold = "<b>";
        public const string HtmlEndBold = "</b>";
    }
}