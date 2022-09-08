
using System.Text;

var Domain = "http://60.249.7.166";

var FilePath = "data.csv";

var AllLine = File.ReadAllLines(FilePath);
using var Client = new HttpClient();
var Url = $"{Domain}/CRIMS/api/Auth/Register_Contractor";

var c = 0;
foreach (var Line in AllLine.Skip(1))
{
    var Cols = Line.Split(",");
    var CompanyName = Cols[0];
    var UserName = Cols[1];
    var Password = "123456";
    var StaffName = Cols[3];
    var StaffGender = true;
    var StaffPhone = Cols[5];
    var Data = new
    {
        CompanyName,
        StaffNo = UserName,
        StaffTypeId = 1,
        StaffName,
        StaffPhone,
        StaffGender,
        Password = Password,
        RePassword = Password
    };

    var JsonString = System.Text.Json.JsonSerializer.Serialize(Data);
    var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
    var ApiRet = await Client.PostAsync(Url, Content);

    Console.WriteLine(await ApiRet.Content.ReadAsStringAsync());
    c++;
    if (c > 2)
        break;

}
