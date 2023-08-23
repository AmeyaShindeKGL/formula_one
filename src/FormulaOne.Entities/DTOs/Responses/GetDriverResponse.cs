namespace FormulaOne.Entities.DTOs.Responses;

public class GetDriverResponse
{
    public Guid DriverId { get; set; }

    public string FullName { get; set; } = string.Empty;
    
    public int DriverNumber { get; set; }

    public DateTime DateOfBirth { get; set; }
}