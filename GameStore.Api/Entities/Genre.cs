namespace GameStore.Api.Entities;

public class Genre

{
    public int Id { get; set; }

    // required: have to provide a value for name
    public required string Name { get; set; }

}