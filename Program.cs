using System;
using System.Linq;
using System.Collections.Generic;
using VisualCode.Entities;

namespace VisualCode
{
    class Program
    {
        enum enumMenuMain : int {Maintenance = 1, RegistrySales = 2, Reports = 3};
        enum enumMenuMaintenance : int {Customers = 1, Products = 2, Sellers = 3, MenuPrincipal = 4};
        enum enumMenuReport : int {Sales = 1, CustomersAge = 2, CustomersBirthdayMonth = 3, ProductsStock = 4};

        static List<Customers> customersList = new List<Customers>();
        static List<Products> productsList = new List<Products>();
        static List<Sellers> sellersList = new List<Sellers>();
        static List<Invoices> invoicesList = new List<Invoices>();
        static List<InvoiceDetails> invoiceDetailsList = new List<InvoiceDetails>();

        static void Main(string[] args)
        {
            LoadDataDefault();

            MenuMain();
            ProcessMain();
        }

        private static void ProcessMain()
        {
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                return;
            }

            switch (option)
            {
                case (int)enumMenuMain.Maintenance:
                    MenuMaintenance();

                    if (!int.TryParse(Console.ReadLine(), out option))
                    {
                        return;
                    }

                    switch (option)
                    {
                        case (int)enumMenuMaintenance.Customers:
                            RegistryCustomer();
                            break;
                        case (int)enumMenuMaintenance.Products:
                            RegistryProduct();
                            break;
                        case (int)enumMenuMaintenance.Sellers:
                            RegistrySeller();
                            break;
                        case (int)enumMenuMaintenance.MenuPrincipal:
                            MenuMain();
                            ProcessMain();
                            break;
                        default:
                            Console.WriteLine("Ingrese una opcion correcta");
                            MenuMain();
                            ProcessMain();
                            break;
                    }

                    break;
                case (int)enumMenuMain.RegistrySales:
                    RegistryInvoice();
                    break;
                case (int)enumMenuMain.Reports:
                    MenuReport();

                    if (!int.TryParse(Console.ReadLine(), out option))
                    {
                        return;
                    }

                    switch (option)
                    {
                        case (int)enumMenuReport.Sales:
                            ReportSales();
                            break;
                        case (int)enumMenuReport.CustomersAge:
                            ReportCustomersAge();
                            break;
                        case (int)enumMenuReport.CustomersBirthdayMonth:
                            ReportCustomersBirthay();
                            break;
                        case (int)enumMenuReport.ProductsStock:
                            ReportProductStock();
                            break;
                        default:
                            Console.WriteLine("Ingrese una opcion correcta");
                            MenuMain();
                            ProcessMain();
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Ingrese una opcion correcta");
                    MenuMain();
                    ProcessMain();
                    break;
            }
        }

        private static void MenuMain()
        {
            Console.WriteLine("1. Mantenimiento");
            Console.WriteLine("2. Registro de Venta");
            Console.WriteLine("3. Reportes");
        }

        private static void MenuMaintenance()
        {
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Producto");
            Console.WriteLine("3. Vendedor");
            Console.WriteLine("9. Menu Principal");
        }

        private static void MenuReport()
        {
            Console.WriteLine("1. Ventas");
            Console.WriteLine("2. Clientes mayores 55");
            Console.WriteLine("3. Clientes cumplan este mes");
            Console.WriteLine("4. Productos por debajo stock");
        }

        private static void RegistryCustomer()
        {
            Console.WriteLine("Codigo Cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int idCustomer))
            {
                return;
            }

            Console.WriteLine("Nombre: ");
            string nameCustomer = Console.ReadLine();

            Console.WriteLine("Fecha de Nacimiento (MM/dd/yyyy): ");
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
            Console.WriteLine("Codigo Producto: ");
            if (!int.TryParse(Console.ReadLine(), out int idProduct))
            {
                return;
            }

            Console.WriteLine("Descripcion: ");
            string description = Console.ReadLine();

            Console.WriteLine("Precio: ");
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
            Console.WriteLine("Codigo Vendedor: ");
            if (!int.TryParse(Console.ReadLine(), out int idSeller))
            {
                return;
            }

            Console.WriteLine("Nombre: ");
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
            Console.WriteLine("Codigo Cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int idCustomer))
            {
                return;
            }

            Customers objCustomers = customersList.SingleOrDefault(cliente => cliente.IdCustomer == idCustomer);

            Console.WriteLine($"Cliente Seleccionado: {objCustomers.Name} , Fecha de Nacimiento: {objCustomers.BirthDate}");

            Console.WriteLine("Codigo Vendedor: ");
            if (!int.TryParse(Console.ReadLine(), out int idSeller))
            {
                return;
            }

            Sellers objSellers = sellersList.SingleOrDefault(vendedor => vendedor.IdSeller == idSeller);

            Console.WriteLine($"Vendedor Seleccionado: {objSellers.Name}");

            //get the last ID
            int maxIdInvoice = 1;

            if (invoicesList.Count > 0) //Another method
            {
                maxIdInvoice = invoicesList.Max(c => c.IdInvoice) + 1;
            }

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
                Console.WriteLine("Codigo Producto: ");
                if (!int.TryParse(Console.ReadLine(), out int idProduct))
                {
                    return;
                }

                Products objProducts = productsList.SingleOrDefault(producto => producto.IdProduct == idProduct);

                PrintData(objProducts);

                Console.WriteLine("Cantidad: ");
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

            Console.WriteLine("Registro de Ventas");

            invoicesList.ForEach(itemInvoices => {
                TotalAmount = TotalAmount + itemInvoices.Amount;
                itemInvoices.InvoiceDetails.ForEach(itemInvoiceDetails =>
                {
                        Console.WriteLine($"Id Factura: {itemInvoices.IdInvoice}  Id Factura Detalle: {itemInvoiceDetails.IdInvoiceDetail} Producto: {itemInvoiceDetails.Products.Description}   Cliente: {itemInvoices.Customers.Name} Precio: {itemInvoiceDetails.Price}    Cantidad: {itemInvoiceDetails.Quantity} Total: {itemInvoiceDetails.Price * itemInvoiceDetails.Quantity}");
                });
            });

            Console.WriteLine($"Total General: {TotalAmount}");

            MenuMain();
            ProcessMain();
        }

        private static void ReportCustomersAge()
        {
            List<Customers> objCustomersList = new List<Customers>();
            objCustomersList = customersList.Where(c => DateTime.Today.Year - Convert.ToDateTime(c.BirthDate).Year > 55).ToList();

            Console.WriteLine("Clientes mayores a 55");

            foreach(Customers customers in objCustomersList)
            {
                Console.WriteLine($"Codigo Cliente: {customers.IdCustomer}  Nombre: {customers.Name}  Fecha Nacimiento: {customers.BirthDate}");
            }

            MenuMain();
            ProcessMain();
        }

        private static void ReportCustomersBirthay()
        {
            List<Customers> objCustomersList = new List<Customers>();
            objCustomersList = customersList.Where(c => DateTime.Today.Month == Convert.ToDateTime(c.BirthDate).Month).ToList();

            Console.WriteLine("Clientes cumplen este mes");

            foreach(Customers customers in objCustomersList)
            {
                Console.WriteLine($"Codigo Cliente: {customers.IdCustomer}  Nombre: {customers.Name}  Fecha Nacimiento: {customers.BirthDate}");
            }

            MenuMain();
            ProcessMain();
        }

        private static void ReportProductStock()
        {
            List<Products> objProductsList = new List<Products>();
            objProductsList = productsList.Where(c => c.Stock <= 5).ToList();

            Console.WriteLine("Productos con stock minimo");

            foreach(Products products in objProductsList)
            {
                Console.WriteLine($"Codigo Producto: {products.IdProduct}  Descripcion: {products.Description}  Precio: {products.Price} Stock: {products.Stock}");
            }

            MenuMain();
            ProcessMain();
        }

        private static string ConfirmationAddDetails()
        {
            Console.WriteLine("Desea agregar detalle de la factura?: (y/n)");
            return Console.ReadLine();
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
            products.Description = "Producto A";
            products.Price = 10;
            products.Stock = 20;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 2;
            products.Description = "Producto B";
            products.Price = 20;
            products.Stock = 40;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 3;
            products.Description = "Producto C";
            products.Price = 30;
            products.Stock = 5;
            productsList.Add(products);

            products = new Products();
            products.IdProduct = 4;
            products.Description = "Producto D";
            products.Price = 40;
            products.Stock = 3;
            productsList.Add(products);

            Sellers sellers = new Sellers();
            sellers.IdSeller = 1;
            sellers.Name = "Vendedor A";
            sellersList.Add(sellers);
        }

        private static void PrintData(object item)
        {
            Console.WriteLine($"{item.ToString()}");
        }
   }
}
