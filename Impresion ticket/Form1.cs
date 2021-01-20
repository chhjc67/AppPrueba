using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using TicketPrinting;

namespace TicketPrinting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string descripcion = "Aspirina tabletas";
            int cantidad = 2;
            double precio = 45.25;
            double total = 90.5;
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();

            if (DialogResult.OK == pd.ShowDialog(this))
            {
                pd.PrinterSettings.PrintToFile = true;
                
                CreaTicket Ticket1 = new CreaTicket(pd.PrinterSettings.PrinterName);
                Ticket1.AbreCajon();  //abre el cajon
                Ticket1.TextoCentro("Venta mostrador"); // imprime en el centro "Venta mostrador"
                Ticket1.LineasGuion(); // imprime una linea de guiones
                Ticket1.EncabezadoVenta(); // imprime encabezados
                Ticket1.AgregaArticulo(descripcion, cantidad, precio, total); //imprime una linea de descripcion
                Ticket1.LineasTotales(); // imprime linea 
                Ticket1.AgregaTotales("Total", total); // imprime linea con total
                Ticket1.CortaTicket(); // corta el ticket
            }

            pd.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();

                if (DialogResult.OK == pd.ShowDialog(this))
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);

                pd.Dispose();
            }

            ofd.Dispose();
        }
    }
}

    #region Clase para generar ticket
    // La clase "CreaTicket" tiene varios metodos para imprimir con diferentes formatos (izquierda, derecha, centrado, desripcion precio,etc), a
    // continuacion se muestra el metodo con ejemplo de parametro que acepta, longitud maxima y un ejemplo de como imprimira, esta clase esta
    // basada en una impresora Epson de matriz de puntos con impresion maxima de 40 caracteres por renglon
    // METODO                                      MAX_LONG                        EJEMPLOS
    //--------------------------------------------------------------------------------------------------------------------------
    // TextoIzquierda("Empleado 1")                    40                      Empleado 1      
    // TextoDerecha("Caja 1")                          40                                                        Caja 1
    // TextoCentro("Ticket")                           40                                         Ticket   
    // TextoExtremos("Fecha 6/1/2011","Hora:13:25")     18 y 18                 Fecha 6/1/2011                Hora:13:25
    // EncabezadoVenta()                                n/a                     Articulo        Can    P.Unit    Importe
    // LineasGuion()                                    n/a                     ----------------------------------------
    // AgregaArticulo("Aspirina","2",45.25,90.5)        16,3,10,11              Aspirina          2    $45.25     $90.50
    // LineasTotales()                                  n/a                                                ----------
    // AgregaTotales("Subtotal",235.25)                 25 y 15                Subtotal                         $235.25
    // LineasAsterisco()                                n/a                     ****************************************
    // LineasIgual()                                    n/a                     ========================================
    // CortaTicket()
    // AbreCajon()
    public class CreaTicket
    {
        string ticket = "";
        string parte1, parte2;
        string _impresora = "\\\\PC-JOSE\\EPSON LX-300+ /II"; // nombre exacto de la impresora como esta en el panel de control
            
        int max, cort;

        public CreaTicket(string impresora)
        {
            _impresora = impresora;
        }

        public void LineasGuion()
        {
            ticket = "----------------------------------------\n";   // agrega lineas separadoras -
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime linea
        }

        public void LineasAsterisco()
        {
            ticket = "****************************************\n";   // agrega lineas separadoras *
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime linea
        }

        public void LineasIgual()
        {
            ticket = "========================================\n";   // agrega lineas separadoras =
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime linea
        }

        public void LineasTotales()
        {
            ticket = "                             -----------\n"; ;   // agrega lineas de total
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime linea
        }

        public void EncabezadoVenta()
        {
            ticket = "Articulo        Can    P.Unit    Importe\n";   // agrega lineas de  encabezados
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void TextoIzquierda(string par1)                          // agrega texto a la izquierda
        {
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);        // si es mayor que 40 caracteres, lo corta
            }
            else
                parte1 = par1;

            ticket = parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void TextoDerecha(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
            }
            else 
                parte1 = par1;

            max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
            for (int i = 0; i < max; i++)
                ticket += " ";                          // agrega espacios para alinear a la derecha

            ticket += parte1 + "\n";                    //Agrega el texto
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void TextoCentro(string par1)
        {
            ticket = "";
            max = par1.Length;

            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);          // si es mayor que 40 caracteres, lo corta
            }
            else
                parte1 = par1;

            max = (int)(40 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos

            for (int i = 0; i < max; i++)                // **********
                ticket += " ";                           // Agrega espacios antes del texto a centrar

            ticket += parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void TextoExtremos(string par1, string par2)
        {
            max = par1.Length;

            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte1 = par1.Remove(18, cort);          // si par1 es mayor que 18 lo corta
            }
            else
                parte1 = par1;

            ticket = parte1;                             // agrega el primer parametro
            max = par2.Length;

            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte2 = par2.Remove(18, cort);          // si par2 es mayor que 18 lo corta
            }
            else 
                parte2 = par2;

            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)  
                ticket += " ";

            ticket += parte2 + "\n";                     // agrega el segundo parametro al final
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void AgregaTotales(string par1, double total)
        {
            max = par1.Length;

            if (max > 25)                                 // **********
            {
                cort = max - 25;
                parte1 = par1.Remove(25, cort);          // si es mayor que 25 lo corta
            }
            else
                parte1 = par1;

            ticket = parte1;
            parte2 = total.ToString("c");
            max = 40 - (parte1.Length + parte2.Length);

            for (int i = 0; i < max; i++) 
                ticket += " ";            

            ticket += parte2 + "\n";
            RawPrinterHelper.SendStringToPrinter(_impresora, ticket); // imprime texto
        }

        public void AgregaArticulo(string par1, int cant, double precio, double total)
        {
            if (cant.ToString().Length <= 3 && precio.ToString("c").Length <= 10 &&
                total.ToString("c").Length <= 11) // valida que cant precio y total esten dentro de rango
            {
                max = par1.Length;

                if (max > 16)
                {
                    cort = max - 16;
                    parte1 = par1.Remove(16, cort);          // corta a 16 la descripcion del articulo
                }
                else
                    parte1 = par1;

                ticket = parte1;                             // agrega articulo
                max = (3 - cant.ToString().Length) + (16 - parte1.Length);

                for (int i = 0; i < max; i++)                // **********
                    ticket += " ";

                ticket += cant.ToString();                   // Agrega cantidad
                max = 10 - (precio.ToString("c").Length);

                for (int i = 0; i < max; i++)
                    ticket += " ";

                ticket += precio.ToString("c");             // Agrega precio
                max = 11 - (total.ToString().Length);

                for (int i = 0; i < max; i++)
                    ticket += " ";

                ticket += total.ToString("c") + "\n";       // Agrega precio
                RawPrinterHelper.SendStringToPrinter(_impresora, ticket);
            }
            else
            {
                MessageBox.Show("Valores fuera de rango");
                RawPrinterHelper.SendStringToPrinter(_impresora, "Error, valor fuera de rango\n");
            }
        }

        public void CortaTicket()
        {
            string corte = "\x1B" + "m";           // caracteres de corte
            string avance = "\x1B" + "d" + "\x09"; // avanza 9 renglones
            RawPrinterHelper.SendStringToPrinter(_impresora, avance);
            RawPrinterHelper.SendStringToPrinter(_impresora, corte);
        }

        public void AbreCajon()
        {
            string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96"; // caracteres de apertura cajon 0
            string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96"; // caracteres de apertura cajon 1
            RawPrinterHelper.SendStringToPrinter(_impresora, cajon0);
            RawPrinterHelper.SendStringToPrinter(_impresora, cajon1);
        }
    }

    #endregion
