namespace GNB.IBM.Core.Configuration
{
    public class ExternalServicesSettings
    {
        public const string ExternalServices = "ExternalServices";
        public string ConversionRatesURI { get; set; } = string.Empty;
        public string ProductTransactionsURI { get; set; } = string.Empty;
    }
}
