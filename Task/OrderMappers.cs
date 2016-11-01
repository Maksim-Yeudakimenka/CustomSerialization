using System.Linq;
using Task.DB;

namespace Task
{
    public static class OrderMappers
    {
        public static Order ToOrderPoco(this Order orderProxy)
        {
            return new Order
            {
                OrderID = orderProxy.OrderID,
                CustomerID = orderProxy.CustomerID,
                EmployeeID = orderProxy.EmployeeID,
                OrderDate = orderProxy.OrderDate,
                RequiredDate = orderProxy.RequiredDate,
                ShippedDate = orderProxy.ShippedDate,
                ShipVia = orderProxy.ShipVia,
                Freight = orderProxy.Freight,
                ShipName = orderProxy.ShipName,
                ShipAddress = orderProxy.ShipAddress,
                ShipCity = orderProxy.ShipCity,
                ShipRegion = orderProxy.ShipRegion,
                ShipPostalCode = orderProxy.ShipPostalCode,
                ShipCountry = orderProxy.ShipCountry,
                Customer = orderProxy.Customer.ToCustomerPoco(),
                Employee = orderProxy.Employee.ToEmployeePoco(),
                Order_Details = orderProxy.Order_Details.Select(ToOrderDetailPoco).ToList(),
                Shipper = orderProxy.Shipper.ToShipperPoco()
            };
        }

        private static Customer ToCustomerPoco(this Customer customerProxy)
        {
            return new Customer
            {
                CustomerID = customerProxy.CustomerID,
                CompanyName = customerProxy.CompanyName,
                ContactName = customerProxy.ContactName,
                ContactTitle = customerProxy.ContactTitle,
                Address = customerProxy.Address,
                City = customerProxy.City,
                Region = customerProxy.Region,
                PostalCode = customerProxy.PostalCode,
                Country = customerProxy.Country,
                Phone = customerProxy.Phone,
                Fax = customerProxy.Fax
            };
        }

        private static Employee ToEmployeePoco(this Employee employeeProxy)
        {
            return new Employee
            {
                EmployeeID = employeeProxy.EmployeeID,
                LastName = employeeProxy.LastName,
                FirstName = employeeProxy.FirstName,
                Title = employeeProxy.Title,
                TitleOfCourtesy = employeeProxy.TitleOfCourtesy,
                BirthDate = employeeProxy.BirthDate,
                HireDate = employeeProxy.HireDate,
                Address = employeeProxy.Address,
                City = employeeProxy.City,
                Region = employeeProxy.Region,
                PostalCode = employeeProxy.PostalCode,
                Country = employeeProxy.Country,
                HomePhone = employeeProxy.HomePhone,
                Extension = employeeProxy.Extension,
                Photo = employeeProxy.Photo,
                Notes = employeeProxy.Notes,
                ReportsTo = employeeProxy.ReportsTo,
                PhotoPath = employeeProxy.PhotoPath
            };
        }

        private static Order_Detail ToOrderDetailPoco(this Order_Detail orderDetailProxy)
        {
            return new Order_Detail
            {
                OrderID = orderDetailProxy.OrderID,
                ProductID = orderDetailProxy.ProductID,
                UnitPrice = orderDetailProxy.UnitPrice,
                Quantity = orderDetailProxy.Quantity,
                Discount = orderDetailProxy.Discount
            };
        }

        private static Shipper ToShipperPoco(this Shipper shipperProxy)
        {
            return new Shipper
            {
                ShipperID = shipperProxy.ShipperID,
                CompanyName = shipperProxy.CompanyName,
                Phone = shipperProxy.Phone
            };
        }
    }
}