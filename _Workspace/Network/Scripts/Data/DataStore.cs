public class DataStore
{
    private string _fileName = "response.json";
    private string _folderName = "/_Workspace/Network/Response/";

    public ServerFeatures ServerRequest { get; private set; }

    public DataStore(string requestResult)
    {
        ServerRequest = new ServerFeatures();
        SerializeManager response = new SerializeManager(_fileName, _folderName);

        if (requestResult == null)
        {
            ServerRequest = null;
        }
        else if (response.DataAvailable() == true)
        {
            try { ServerRequest = response.LoadData(new ServerFeatures()); }
            catch
            {
                response.SaveData(requestResult);

                try { ServerRequest = response.LoadData(new ServerFeatures()); }
                catch { ServerRequest = new ServerFeatures(); }
            }

            if (ServerRequest.error == true)
            {
                response.SaveData(requestResult);

                try { ServerRequest = response.LoadData(new ServerFeatures()); }
                catch { ServerRequest = new ServerFeatures(); }
            }
        }
        else if (response.DataAvailable() == false)
        {
            response.SaveData(requestResult);

            try { ServerRequest = response.LoadData(new ServerFeatures()); }
            catch { ServerRequest = new ServerFeatures(); }
        }
    }
}
