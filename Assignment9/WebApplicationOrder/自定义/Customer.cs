using System;
using System.Collections.Generic;

namespace WebApplicationOrder.自定义;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public Customer()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Customer(string name) : this()
    {
        Name = name;
    }

    public override bool Equals(object obj)
    {
        var customer = obj as Customer;
        return customer != null &&
               Id == customer.Id &&
               Name == customer.Name;
    }

    public override int GetHashCode()
    {
        var hashCode = 1479869798;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        return hashCode;
    }
}
