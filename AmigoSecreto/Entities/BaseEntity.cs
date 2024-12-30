namespace AmigoSecreto.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateTimeCreation { get; set; }
    public DateTime DateTimeUpdate { get; set; }
}
