using System;
using System.Collections.Generic;

namespace WebApplicationOrder.自定义;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public Product()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Product(string name, double price) : this()
    {
        Name = name;
        Price = price;
    }

    public override bool Equals(object obj)
    {
        var product = obj as Product;
        return product != null &&
               Id == product.Id &&
               Name == product.Name;
    }

    public override int GetHashCode()
    {
        var hashCode = 1479869798;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        return hashCode;
    }
}
