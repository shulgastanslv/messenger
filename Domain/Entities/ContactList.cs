using System.Collections.ObjectModel;

namespace Domain.Entities;

public class ContactList
{
    public string Id { get; set; }
    public User Owner { get; set; }
    public List<Contact> Contacts { get; set; }
}