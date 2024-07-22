namespace api.Helpers;

public class QueryObject
{
    public string? Sympol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public string? orderby {get; set;}
    public bool IsDescending { get; set; }

    public int PageNumber {get; set;} = 1;

    public int PageSize { get; set; } = 20;
}
