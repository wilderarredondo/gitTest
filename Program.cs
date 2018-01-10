using System;
using System.Linq;
using System.Collections.Generic;
using VisualCode.Entities;

namespace VisualCode
{
    public enum EnumMenuMain : int
    {
        Maintenance = 1,
        RegistrySales = 2,
        Reports = 3
    };

    public enum EnumMenuMaintenance : int
    {
        Customers = 1,
        Products = 2,
        Sellers = 3,
        MenuPrincipal = 4
    };

    public enum EnumMenuReport : int
    {
        Sales = 1,
        CustomersAge = 2,
        CustomersBirthdayMonth = 3,
        ProductsStock = 4
    };

    class Program
    {
        private static List<Customers> customersList = new List<Customers>();
        private static List<Products> productsList = new List<Products>();
        private static List<Sellers> sellersList = new List<Sellers>();
        private static List<Invoices> invoicesList = new List<Invoices>();
        private static List<InvoiceDetails> invoiceDetailsList = new List<InvoiceDetails>();

        static void Main(string[] args)
        {
            LoadDataDefault();

            MenuMain();

            ProcessMain();
        }

        private static void ProcessMain()
        {
            if (!Enum.TryParse(Console.ReadLine(), true, out EnumMenuMain optionMain))
            {
                return;
            }

            switch (optionMain)
            {
                case EnumMenuMain.Maintenance:
                    MenuMaintenance();

                    if (!Enum.TryParse(Console.ReadLine(), true, out EnumMenuMaintenance optionMaintenance))
                    {
                        return;
                    }

                    switch (optionMaintenance)
                    {
                        case EnumMenuMaintenance.Customers:
                            RegistryCustomer();
                            break;
                        case EnumMenuMaintenance.Products:
                            RegistryProduct();
                            break;
                        case EnumMenuMaintenance.Sellers:
                            RegistrySeller();
                            break;
                        case EnumMenuMaintenance.MenuPrincipal:
                            MenuMain();
                            ProcessMain();
                            break;
                        default:
                            Console.WriteLine("Enter a correct option");
                            MenuMain();
                            ProcessMain();
                            break;
                    }

                    break;
                case EnumMenuMain.RegistrySales:
                    RegistryInvoice();
                    break;
                case EnumMenuMain.Reports:
                    MenuReport();

                    if (!Enum.TryParse(Console.ReadLine(), true, out EnumMenuReport optionReport))
                    {
                        return;
                    }

                    switch (optionReport)
                    {
                        case EnumMenuReport.Sales:
                            ReportSales();
                            break;
                        case EnumMenuReport.CustomersAge:
                            ReportCustomersAge();
                            break;
                        case EnumMenuReport.CustomersBirthdayMonth:
                            ReportCustomersBirthay();
                            break;
                        case EnumMenuReport.ProductsStock:
                            ReportProductStock();
                            break;
                        default:
                            Console.WriteLine("Enter a correct option");
                            MenuMain();
                            ProcessMain();
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Enter a correct option");
                    MenuMain();
                    ProcessMain();
                    break;
            }
        }

        private static void MenuMain()
        {
            Console.WriteLine("1. Maintenance");
            Console.WriteLine("2. Registration of Sale");
            Console.WriteLine("3. Reports");
        }

        private static void MenuMaintenance()
        {
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Product");
            Console.WriteLine("3. Seller");
            Console.WriteLine("9. Main Menu");
        }

        private static void MenuReport()
        {
            Console.WriteLine("1. Sales");
            Console.WriteLine("2. Senior customers 55");
            Console.WriteLine("3. Customers meet this month");
            Console.WriteLine("4. Products below stock");
        }

        private static void RegistryCustomer()
        {
            Console.WriteLine("Id Customer: ");
            if (!int.TryParse(Console.ReadLine(), out int idCustomer))
            {
                return;
            }

            Console.WriteLine("Name: ");
            string nameCustomer = Console.ReadLine();

            Console.WriteLine("BirthDate (MM/dd/yyyy): ");
            string birthDateCustomer = Console.ReadLine();

            Customers customers = new Customers();
            customers.IdCustomer = idCustomer;
            customers.Name = nameCustomer;
            customers.BirthDate = birthDateCustomer;
            customersList.Add(customers);

            MenuMain();
            ProcessMain();
        }

        private static void RegistryProduct()
        {
            Console.WriteLine("Id Product: ");
            if (!int.TryParse(Console.ReadLine(), out int idProduct))
            {
                return;
            }

            Console.WriteLine("Description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                return;
            }

            Console.WriteLine("Stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                return;
            }

            Products products = new Products();
            products.IdProduct = idProduct;
            products.Description = description;
            products.Price = price;
            products.Stock = stock;
            productsList.Add(products);

            MenuMain();
            ProcessMain();
        }

        private static void RegistrySeller()
        {
            Console.WriteLine("Id Seller: ");
            if (!int.TryParse(Console.ReadLine(), out int idSeller))
            {
                return;
            }

            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            Sellers sellers = new Sellers();
            sellers.IdSeller = idSeller;
            sellers.Name = name;

            sellersList.Add(sellers);

            MenuMain();
            ProcessMain();
        }

        private static void RegistryInvoice()
        {
            Console.WriteLine("Id Customer: ");
            if (!int.TryParse(Console.ReadLine(), out int idCustomer))
            {
                return;
            }

            Customers objCustomers = customersList.SingleOrDefault(cliente => cliente.IdCustomer == idCustomer);
            PrintData(objCustomers.ToString());

            Console.WriteLine("Id Seller: ");
            if (!int.TryParse(Console.ReadLine(), out int idSeller))
            {
                return;
            }

            Sellers objSellers = sellersList.SingleOrDefault(vendedor => vendedor.IdSeller == idSeller);
            PrintData(objSellers.ToString());

            //get the last ID
            int maxIdInvoice = 1;
            if (invoicesList.Count > 0) //Another method
                maxIdInvoice = invoicesList.Max(c => c.IdInvoice) + 1;

            //The header is recorded
            Invoices invoices = new Invoices();
            invoices.IdInvoice = maxIdInvoice;
            invoices.Customers = objCustomers;
            invoices.Sellers = objSellers;

            string optionSelected = "y";
            int numberRegistry = 0;
            decimal total = 0;

            invoices.InvoiceDetails = new List<InvoiceDetails>();

            while (optionSelected == "y")
            {
                Console.WriteLine("Id Product: ");
                if (!int.TryParse(Console.ReadLine(), out int idProduct))
                {
                    return;
                }

                Products objProducts = productsList.SingleOrDefault(producto => producto.IdProduct == idProduct);
                PrintData(objProducts.ToString());

                Console.WriteLine("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    return;
                }

                InvoiceDetails invoiceDetails = new InvoiceDetails();
                invoiceDetails.IdInvoiceDetail = numberRegistry + 1;
                invoiceDetails.Products = objProducts;
                invoiceDetails.Price = objProducts.Price;
                invoiceDetails.Quantity = quantity;

                total = total + objProducts.Price*quantity;

                invoices.InvoiceDetails.Add(invoiceDetails);

                numberRegistry++;

                optionSelected = ConfirmationAddDetails();
            }

            invoices.Amount = total;
            invoicesList.Add(invoices);

            MenuMain();
            ProcessMain();
        }

        private static void ReportSales()
        {
            decimal TotalAmount = 0;

            Console.WriteLine("Sales Record");

            invoicesList.ForEach(itemInvoices => {
                TotalAmount = TotalAmount + itemInvoices.Amount;

                PrintData(itemInvoices.FullDetails());

                itemInvoices.InvoiceDetails.ForEach(itemInvoiceDetails =>
                {
                    PrintData(itemInvoiceDetails.FullDetails());
                });
            });

            Console.WriteLine($"Grand Total: {TotalAmount}");

            MenuMain();
            ProcessMain();
        }

        private static void ReportCustomersAge()
        {
            Console.WriteLine("Customers over 55");

            List<Customers> objCustomersList = new List<Customers>();
            objCustomersList = customersList.Where(c => DateTime.Today.Year - Convert.ToDateTime(c.BirthDate).Year > 55).ToList();

            foreach(Customers customers in objCustomersList)
            {
                PrintData(customers.FullDetails());
            }

            MenuMain();
            ProcessMain();
        }

        private static void ReportCustomersBirthay()
        {
            List<Customers> objCustomersList = null;
            objCustomersList = customersList.Where(c => DateTime.Today.Month == Convert.ToDateTime(c.BirthDate).Month).ToList();

            Console.WriteLine("Customers meet this month");

            foreach(Customers customers in objCustomersList)
            {
                PrintData(customers.FullDetails());
            }

            MenuMain();
            ProcessMain();
        }

        private static void ReportProductStock()
        {
            List<Products> objProductsList = null;
            objProductsList = productsList.Where(c => c.Stock <= 5).ToList();

            Console.WriteLine("Products with minimum stock");

            foreach(Products products in objProductsList)
            {
                PrintData(products.FullDetails());
            }

            MenuMain();
            ProcessMain();
        }

        private static string ConfirmationAddDetails()
        {
            Console.WriteLine("You want to add detail of the invoice?: (y/n)");
            return Console.ReadLine();
        }

        private static void PrintData(string item)
        {
            Console.WriteLine(item);
        }

        private static void LoadDataDefault()
        {
            Customers customers = new Customers();
            customers.IdCustomer = 1;
            customers.Name = "Victor Palomino";
            customers.BirthDate = "09/09/1982";
            customersList.Add(customers);

            customers = new Customers();
            customers.IdCustomer = 2;
            customers.Name = "Noemi Beltran";
            customers.BirthDate = "04/07/1902";
            customersList.Add(customers);

            customers = new Customers();
            customers.IdCustomer = 3;
            customers.Name = "Aldo Hiruma";
            customers.BirthDate = "01/04/1952";
            customersList.Add(customers);

            Products products = new Products();
            products.IdProduct = 1;
            products.Description = "Product A";
            products.Price = 10;
            products.Stock = 20;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 2;
            products.Description = "Product B";
            products.Price = 20;
            products.Stock = 40;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 3;
            products.Description = "Product C";
            products.Price = 30;
            products.Stock = 5;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 4;
            products.Description = "Product D";
            products.Price = 40;
            products.Stock = 3;
            productsList.Add(products);

            Sellers sellers = new Sellers();
            sellers.IdSeller = 1;
            sellers.Name = "Seller A";
            sellersList.Add(sellers);
        }
   }
}
