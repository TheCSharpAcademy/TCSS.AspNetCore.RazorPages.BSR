namespace BSR.Models;

public class StatesResponse
{
    public bool Error { get; set; }
    public string Msg { get; set; }
    public Data Data { get; set; }
}

public class Data
{
    public List<StateData> States { get; set; } 
}

public class StateData
{
    public string Name { get; set; }
}

public class CitiesResponse
{
    public bool Error { get; set; }
    public string Msg { get; set; }
    public CityData Data { get; set; }
}

public class CityData
{
    public List<string> Cities { get; set; }
}


