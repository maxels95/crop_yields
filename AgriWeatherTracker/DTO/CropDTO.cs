public class CropDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<GrowthCycleDTO> GrowthCycles { get; set; }
    public List<LocationDTO> Locations { get; set; }
}