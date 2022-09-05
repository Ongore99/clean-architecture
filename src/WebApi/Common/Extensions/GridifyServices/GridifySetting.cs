namespace WebApi.Common.Extensions.GridifyServices;

public class GridifySetting
{
    public int DefaultPageSize { get; set; } = 25;
    
    public bool AllowNullSearch { get; set; } = true;
}