namespace Companies.Application.Entities;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
    public RestaurantStatus Status { get; set; }
    public List<Collaborator> Collaborators { get; set; } = [];
    public List<Document> Documents { get; set; } = [];
}

public enum RestaurantStatus
{
    Pending,
    Approved,
    Rejected
}

public class Collaborator
{
    public Guid Id { get; set; }
    public CollaboratorType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool IsOperational => EmailConfirmed == true;
}

public enum CollaboratorType
{
    Administrator,
    Manager,
    Attendant
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
public class Document
{
    public Guid Id { get; set; }
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
}

public enum DocumentType
{
    CPF,
    CNPJ,
    AddressProof
}

public enum DocumentStatus
{
    Pending,
    Approved,
    Rejected
}