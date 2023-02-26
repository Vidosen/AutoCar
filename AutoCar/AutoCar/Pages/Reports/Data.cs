using System.Data;

namespace AutoCar.Pages.Reports;

public class ClientData
{
    public int Id { get; }
    public string FullName { get; }
    public ClientData(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
    public static IEnumerable<ClientData> ExtractClientsDataFromTable(DataTable clientsTable)
    {
        var clientsData = new List<ClientData>();
        foreach (DataRow clientRow in clientsTable.Rows)
        {
            var id = Convert.ToInt32(clientRow["Id"]);
            var fullName = clientRow["FullName"].ToString();
            clientsData.Add(new ClientData(id, fullName));
        }
        return clientsData;
    }
}

public record MultiClientCarData(string CarNumber, IEnumerable<ClientData> Clients);
