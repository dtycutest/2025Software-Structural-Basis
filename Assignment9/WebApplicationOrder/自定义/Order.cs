using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationOrder.自定义;

public partial class Order : IComparable<Order>
{
    public string OrderId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual List<Orderdetail> Orderdetails { get; set; }

    public string CustomerName { get => (Customer != null) ? Customer.Name : ""; }

    public Order()
    {
        OrderId = Guid.NewGuid().ToString();
        Orderdetails = new List<Orderdetail>();
        CreateTime = DateTime.Now;
    }

    public Order(string orderId, Customer customer, List<Orderdetail> items) : this()
    {
        this.OrderId = orderId;
        this.Customer = customer;
        this.Orderdetails = items;
    }

    public double TotalPrice
    {
        get => Orderdetails.Sum(item => item.TotalPrice);
    }

    public void AddDetail(Orderdetail orderItem)
    {
        if (Orderdetails.Contains(orderItem))
            throw new ApplicationException($"添加错误：订单项{orderItem.ProductName} 已经存在!");
        Orderdetails.Add(orderItem);
    }

    public void RemoveDetail(Orderdetail orderItem)
    {
        Orderdetails.Remove(orderItem);
    }

    public override string ToString()
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Append($"Id:{OrderId}, customer:{Customer},orderTime:{CreateTime},totalPrice：{TotalPrice}");
        Orderdetails.ForEach(od => strBuilder.Append("\n\t" + od));
        return strBuilder.ToString();
    }

    public override bool Equals(object obj)
    {
        var order = obj as Order;
        return order != null &&
               OrderId == order.OrderId;
    }

    public override int GetHashCode()
    {
        var hashCode = -531220479;
        hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerName);
        hashCode = hashCode * -1521134295 + CreateTime.GetHashCode();
        return hashCode;
    }

    public int CompareTo(Order other)
    {
        if (other == null) return 1;
        return this.OrderId.CompareTo(other.OrderId);
    }


}
