using System;
using VisualCode.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VisualCode
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customers> customersList = new List<Customers>();
            List<Product> productList = new List<Product>();
            List<Seller> sellerList = new List<Seller>();
            List<Invoice> invoiceList = new List<Invoice>();
            List<InvoiceDetail> invoiceDetailList = new List<InvoiceDetail>();

            LoadDataDefault();

            MenuPrincipal();
            ProcesoPrincipal();

            //Console.WriteLine($"escribiste {linea}");

            void ProcesoPrincipal()
            {
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuMaintenance();

                        opcion = Console.ReadLine();

                        switch (opcion)
                        {
                            case "1":
                                RegistryCustomer();
                                break;
                            case "2":
                                RegistryProduct();
                                break;
                            case "3":
                                RegistrySeller();
                                break;
                            case "9":
                                MenuPrincipal();
                                ProcesoPrincipal();
                                break;
                        }

                        break;
                    case "2": //Registry
                        RegistryInvoice();
                        break;
                    case "3": //Reportes
                        MenuReport();

                        opcion = Console.ReadLine();

                        switch (opcion)
                        {
                            case "1": //Ventas
                                ReportSales();
                                break;
                            case "2": //Clientes mayores a 55
                                ReportCustomersAge();
                                break;
                            case "3": //Clientes cumplen este mes
                                ReportCustomersBirthay();
                                break;
                            case "4": //Productos stock minimo
                                ReportProductStock();
                                break;
                        }
                    break;
                }
            }

            void MenuPrincipal()
            {
                Console.WriteLine("1. Mantenimiento");
                Console.WriteLine("2. Registro de Venta");
                Console.WriteLine("3. Reportes");
            }

            void MenuMaintenance()
            {
                Console.WriteLine("1. Cliente");
                Console.WriteLine("2. Producto");
                Console.WriteLine("3. Vendedor");
                Console.WriteLine("9. Menu Principal");
            }

            void MenuReport()
            {
                Console.WriteLine("1. Ventas");
                Console.WriteLine("2. Clientes mayores 55");
                Console.WriteLine("3. Clientes cumplan este mes");
                Console.WriteLine("4. Productos por debajo stock");
            }

            void RegistryCustomer()
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

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void RegistryProduct()
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

                Product product = new Product();
                product.IdProduct = idProduct;
                product.Description = description;
                product.Price = price;
                product.Stock = stock;

                productList.Add(product);

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void RegistrySeller()
            {
                Console.WriteLine("Codigo Vendedor: ");
                if (!int.TryParse(Console.ReadLine(), out int idSeller))
                {
                    return;
                }

                Console.WriteLine("Nombre: ");
                string name = Console.ReadLine();

                Seller seller = new Seller();
                seller.IdSeller = idSeller;
                seller.Name = name;

                sellerList.Add(seller);

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void RegistryInvoice()
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

                Seller objSeller = sellerList.SingleOrDefault(vendedor => vendedor.IdSeller == idSeller);

                Console.WriteLine($"Vendedor Seleccionado: {objSeller.Name}");

                //Obtenemos el ultimo ID
                int maxIdInvoice = 1;

                if (invoiceList.Count > 0) //Otro metodo
                {
                    maxIdInvoice = invoiceList.Max(c => c.IdInvoice) + 1;
                }

                //Se graba la cabecera
                Invoice invoice = new Invoice();
                invoice.IdInvoice = maxIdInvoice;
                invoice.Customer = objCustomers;
                invoice.Seller = objSeller;

                //invoiceList.Add(invoice);

                string optionSelected = "y";
                int numberRegistry = 0;
                decimal total = 0;

                invoice.InvoiceDetails = new List<InvoiceDetail>();

                while (optionSelected == "y")
                {
                    Console.WriteLine("Codigo Producto: ");
                    if (!int.TryParse(Console.ReadLine(), out int idProduct))
                    {
                        return;
                    }

                    Product objProduct = productList.SingleOrDefault(producto => producto.IdProduct == idProduct);

                    PrintData(objProduct);

                    Console.WriteLine("Cantidad: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        return;
                    }

                    InvoiceDetail invoiceDetail = new InvoiceDetail();
                    invoiceDetail.IdInvoiceDetail = numberRegistry + 1;
                    invoiceDetail.Product = objProduct;
                    invoiceDetail.Price = objProduct.Price;
                    invoiceDetail.Quantity = quantity;

                    total = total + objProduct.Price*quantity;
                    
                    invoice.InvoiceDetails.Add(invoiceDetail);

                    numberRegistry++;

                    Console.WriteLine("Desea agregar detalle de la factura?: (y/n)");
                    optionSelected = Console.ReadLine();
                }

                invoice.Amount = total;
                invoiceList.Add(invoice);

                MenuPrincipal();
                ProcesoPrincipal();
                //int a=3;
                //int b =4;
                //int c  = a.sumar(b);
            }

            void ReportSales()
            {
                decimal TotalAmount = 0;

                Console.WriteLine("Registro de Ventas");

                invoiceList.ForEach(itemInvoice => {
                    //Console.Write( $"{item.IdInvoice} {item.Customer.Name} {item.Seller.Name}");
                    TotalAmount = TotalAmount + itemInvoice.Amount;
                    itemInvoice.InvoiceDetails.ForEach(itemInvoiceDetails => 
                    {
                            Console.WriteLine($"Id Factura: {itemInvoice.IdInvoice}  Id Factura Detalle: {itemInvoiceDetails.IdInvoiceDetail} Producto: {itemInvoiceDetails.Product.Description}   Cliente: {itemInvoice.Customer.Name} Precio: {itemInvoiceDetails.Price}    Cantidad: {itemInvoiceDetails.Quantity} Total: {itemInvoiceDetails.Price * itemInvoiceDetails.Quantity}");
                    });
                });

                Console.WriteLine($"Total General: {TotalAmount}");

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void ReportCustomersAge()
            {
                List<Customers> objCustomersList = new List<Customers>();
                objCustomersList = customersList.Where(c => DateTime.Today.Year - Convert.ToDateTime(c.BirthDate).Year > 55).ToList();

                Console.WriteLine("Clientes mayores a 55");

                foreach(Customers customers in objCustomersList)
                {
                    Console.WriteLine($"Codigo Cliente: {customers.IdCustomer}  Nombre: {customers.Name}  Fecha Nacimiento: {customers.BirthDate}");
                }

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void ReportCustomersBirthay()
            {
                List<Customers> objCustomersList = new List<Customers>();
                objCustomersList = customersList.Where(c => DateTime.Today.Month == Convert.ToDateTime(c.BirthDate).Month).ToList();

                Console.WriteLine("Clientes cumplen este mes");

                foreach(Customers customers in objCustomersList)
                {
                    Console.WriteLine($"Codigo Cliente: {customers.IdCustomer}  Nombre: {customers.Name}  Fecha Nacimiento: {customers.BirthDate}");
                }

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void ReportProductStock()
            {
                List<Product> objProductList = new List<Product>();
                objProductList = productList.Where(c => c.Stock <= 5).ToList();

                Console.WriteLine("Productos con stock minimo");

                foreach(Product product in objProductList)
                {
                    Console.WriteLine($"Codigo Producto: {product.IdProduct}  Descripcion: {product.Description}  Precio: {product.Price} Stock: {product.Stock}");
                }

                MenuPrincipal();
                ProcesoPrincipal();
            }

            void LoadDataDefault()
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

                Product product = new Product();
                product.IdProduct = 1;
                product.Description = "Producto A";
                product.Price = 10;
                product.Stock = 20;
                productList.Add(product);

                product = new Product();
                product.IdProduct = 2;
                product.Description = "Producto B";
                product.Price = 20;
                product.Stock = 40;
                productList.Add(product);

                product = new Product();
                product.IdProduct = 3;
                product.Description = "Producto C";
                product.Price = 30;
                product.Stock = 5;
                productList.Add(product);

                product = new Product();
                product.IdProduct = 4;
                product.Description = "Producto D";
                product.Price = 40;
                product.Stock = 3;
                productList.Add(product);

                Seller seller = new Seller();
                seller.IdSeller = 1;
                seller.Name = "Vendedor A";
                sellerList.Add(seller);
            }
        }

        private static void PrintData(object item) 
        {
            Console.WriteLine($"{item.ToString()}");
        }
   }

   //public static class extension 
   //{
   //    public static int sumar(this int n1, int prm1)
   //    {
   //        return n1 + prm1;
   //    }
   //}
}
