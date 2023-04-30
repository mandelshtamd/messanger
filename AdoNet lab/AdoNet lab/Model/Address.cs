namespace AdoNet_lab.Model;

public class Address
{
    public int Appartment { get; private set; }
    public int Building { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }    
    public string Street { get; private set; }

    public Address(string country, string city, string street, int building, int appartment)
    {
        Appartment = appartment;
        Building = building;
        Country = country;
        City = city;
        Street = street;
    }
}