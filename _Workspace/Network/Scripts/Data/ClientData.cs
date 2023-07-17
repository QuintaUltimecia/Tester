[System.Serializable]
public class ClientData
{
    public string client_id = "null";
    public string install_referrer = "null";
    public string carrier_code = "null";
    public string app_device_type = "null";
    public string app_locale = "null";
    public string app_device_name = "null";
    public string app_client_id = "null";
    public string app_advertising_id = "null";
    public string package = "null";

    public ClientData(string clientID, string installerName, string carrierCode, string appDeviceType, string appLocale, string appDeviceName, string appClientID, string appAdvertisingID, string package)
    {
        client_id = clientID;
        install_referrer = installerName;
        carrier_code = carrierCode;
        app_device_type = appDeviceType;
        app_locale = appLocale;
        app_device_name = appDeviceName;
        app_client_id = appClientID;
        app_advertising_id = appAdvertisingID;
        this.package = package;
    }
}
