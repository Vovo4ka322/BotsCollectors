public class ResourceDataBaseCell
{
    public ResourceDataBaseCell(Resource resource)
    {
        Resource = resource;
    }

    public Resource Resource {  get; private set; }

    public bool IsBaseTarget {  get; private set; }

    public void Reservate() => IsBaseTarget = true;
}
