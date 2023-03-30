using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleAppPrueba
{
    class Program
    {
        public static readonly string DEST1 = "D:/Download/Results/Documento1.pdf";
        public static readonly string DEST2 = "D:/Download/Results/Documento2.pdf";

        static void Main(string[] args)
        {
            //Prueba01();
            Prueba02();
            //Prueba03();
            //Prueba04();
            //Prueba05();
            //Prueba06();
            //Prueba07();
            //Prueba08();
        }

        #region Prueba01() // Definición de variable
        static void Prueba01() 
        {
            var pathBase = AppDomain.CurrentDomain.BaseDirectory;
            var result = Regex.IsMatch("a_A1-", "[^0-9a-zA-Z_]+");

            HashSet<Person> employee = new HashSet<Person>()
            {
                new Person("John", "123-45-678", 100),
                new Person("Nassy", "524-35-754", 200),
                new Person("John", "123-45-678", 150),
                new Person("Nassy", "254250352", 150)
            };

            foreach (var item in employee)
                foreach(var prop in item.GetType().GetProperties())
                    Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(item, null));

            Type t = typeof(Person);
            Console.WriteLine("The {0} type has the following properties: ", t.Name);

            foreach (var prop in t.GetProperties())
                Console.WriteLine(" {0} ({1})", prop.Name, prop.PropertyType.Name);

            employee.First(u => u.LastName.Equals("Nassy") && u.SSN.Equals("254-25-0352")).Quantity = 205;
            employee.RemoveWhere(u => u.LastName.Equals("Maria"));
            try
            {
                Person applicant = new Person("Juan", "301-4-045", 0);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Excepción: {0}", e.Message);
            }

            //En el bloque de iteradores, yield se usa junto con return para proporcionar un valor al objeto enumerador
            foreach (int ii in Power(2, 8))
                Console.WriteLine("{0} Power: {1} \n", CultureResources.StringResource.StringTest, ii);
            Console.WriteLine("---------");

            // FileStream
            string path = @"C:\Users\chhjc\source\repos\chhjc67\AppPrueba\ConsoleAppPrueba\Archivo.txt";
            FileInfo fileInfo = new FileInfo(path);
            FileStream myFileStream = new FileStream(path, FileMode.Open);
            byte[] ByteArray = new byte[Convert.ToInt32(fileInfo.Length)];
            int nBytesRead = myFileStream.Read(ByteArray, 0, Convert.ToInt32(fileInfo.Length));
            Console.WriteLine("{0} bytes have been read from the specified file.", nBytesRead.ToString());
            myFileStream.Position = 0;
            StreamReader myStreamReader = new StreamReader(myFileStream);
            string ss;
            while ((ss = myStreamReader.ReadLine()) != null)
                Console.WriteLine(ss);

            path = @"C:\Users\chhjc\source\repos\chhjc67\AppPrueba\ConsoleAppPrueba\Descarga.jpg";
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            fileInfo = new FileInfo(path);
            byte[] tempByte = binaryReader.ReadBytes(Convert.ToInt32(fileInfo.Length));
            Console.ReadLine();
            Console.Clear();
#if DEBUG
            string applicationName = Environment.GetCommandLineArgs()[0];
#else
            string applicationName = Environment.GetCommandLineArgs()[0]+ ".exe";
#endif
            string exePath = System.IO.Path.Combine(Environment.CurrentDirectory, applicationName);
            Configuration config1 = ConfigurationManager.OpenExeConfiguration(exePath);
            Console.WriteLine("\nArchivo Configuracion: {0}", config1.FilePath);
            Console.ReadLine();
            Console.Clear();
        }

        static IEnumerable Power(int number, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Console.WriteLine(CultureResources.StringResource.StringTest);
        }

        #endregion

        #region Prueba02() // Lambda expression // Delegados // Linq // Expression<Func<>> // Xml)
        static void Prueba02()
        {
            List<Student> students = new List<Student>()
            {
                new Student {First="Juliana",
                    Last="Perez", 
                    Street="Av America",
                    City="Cali",
                    Scores= new List<int> {97, 92, 81, 60}},
                new Student {First="Clara",
                    Last="Lopez", 
                    Street="Av Sol",
                    City="Medellin",
                    Scores= new List<int> {75, 84, 91, 39}},
                new Student {First="Alejandra",
                    Last="Mendez",
                    Street="Av Caraca",
                    City="Bogota",
                    Scores= new List<int> {88, 94, 65, 91}},
                new Student {First="John",
                    Last="Kennedy",
                    Street="Av Paris",
                    City="Ibaque",
                    Scores= new List<int> {88, 94, 65, 91}},
                new Student {First="Fabiola",
                    Last="Botelo",
                    Street="Av San Antonio",
                    City="Cali",
                    Scores= new List<int> {88, 94, 65, 91}},
                new Student {First = "Diana",
                    Last = "Caballero",
                    Street = "Av Euro",
                    City = "Bogota",
                    Scores = new List<int> {97, 92, 81, 60}},
                new Student {First = "Andrea",
                    Last = "Fuentez",
                    Street = "Av Euro",
                    City = "Bogota",
                    Scores = new List<int> {97, 92, 81, 60}}
            };
            List<Teacher> teachers = new List<Teacher>()
            {                
                new Teacher {First="Ana", Last="Bello", City = "Bogota"},
                new Teacher {First="Alex", Last="Robinson", City = "Cali"},
                new Teacher {First="Mario", Last="Santos", City = "Cali"},
                new Teacher {First="Mauricio", Last="Gomez", City = "Medellin"}
            };

            int length = students.Max(u => u.Street.Length);
            Console.WriteLine($"Colección 'students' la maxima longitud de la propiedad street es de {length}");
            Console.WriteLine("-----------");

            var peopleInCali = (from student in students
                where student.City == "Cali"
                select new { student.First, student.Last })
                .Concat(from teacher in teachers
                        where teacher.City == "Cali"
                        select new { teacher.First, teacher.Last });
            XElement peopleToXML = new XElement("root", from people in peopleInCali
                select new XElement("people",
                            new XAttribute("First", people.First),
                            new XAttribute("Last", people.Last)));
            Console.WriteLine("The following students and teachers:");
            Console.WriteLine(peopleToXML);
            Console.WriteLine("-----------");
            XElement studentsToXML = new XElement("root", from student in students
                let x = $"{student.Scores[0]}, {student.Scores[1]}, {student.Scores[2]}, {student.Scores[3]}"
                select new XElement("student",
                            new XElement("First", student.First),
                            new XElement("Last", student.Last),
                            new XElement("Score", from score in student.Scores
                                select new XElement("value", score)),
                            new XElement("Scores", x)));
            studentsToXML.Add(peopleToXML.Elements("people"));
            Console.WriteLine(studentsToXML);
            Console.WriteLine("-----------");
            IEnumerable<XElement> studentXML = from nn in studentsToXML.Elements("student")
                where ((string)nn.Element("First")).Contains("An")
                select nn;
            foreach (XElement nn in studentXML)
                Console.WriteLine(nn);
            Console.WriteLine("-----------");
            try
            {
                var path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
                var textReader = new XmlTextReader($"{path}\\Test.xml");
                XDocument docXml1 = XDocument.Load(textReader);
                Console.WriteLine(docXml1);
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("-----------");
            IEnumerable<Student> collect = 
                from e in studentsToXML.Elements("student")
                select new Student
                {
                    First = (string)e.Attribute("First"),
                    Last = (string)e.Attribute("Last"),
                };
            Console.ReadLine();
            Console.Clear();

            IEnumerable<int> num = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            decimal promedio = num.Aggregate(0, (rr, ii) => rr + ii, rr => (decimal)rr / num.Count());
            //Delegados
            int[] result = Array.FindAll<int>(num.ToArray(), delegate(int x) { return x % 2 != 0; });
            Console.WriteLine("Resultado: " + String.Join(", ", result));
            Console.WriteLine("--------");
            //Expression Lambda
            result = Array.FindAll(num.ToArray(), x => x % 2 != 0);
            Console.WriteLine("Resultado: " + String.Join(", ", result));
            Console.WriteLine("--------");
            //Expression Linq / Lambda
            foreach (int i in num.Where(x => x % 2 != 0))
                Console.WriteLine("Resultado: {0}:", i);
            Console.WriteLine("--------");
            var pares = from n in num
                        where n % 2 != 0
                        select n;
            foreach (int i in pares)
                Console.WriteLine("Resultado: {0}:", i);
            Console.WriteLine("--------");
            foreach (var item in num.TakeWhile(x => x < 5))
                Console.WriteLine("Resultado: {0}", item);
            Console.WriteLine("--------");
            var peoples = from s in students
                          group s by s.City into g
                          join t in teachers on
                          g.Key equals t.City
                          orderby t.City, t.First
                          select new XElement("Persona",
                            new XElement("First", t.First),
                            new XElement("Last", t.Last));
            XDocument doc = new XDocument(
                new XComment("Summarized Incoming Call Stats"),
                new XElement("contacts", peoples));
            Console.ReadLine();
            Console.Clear();

            double r = 3.0, p = 5.0;
            var cuadrado = new Dimensions(r, p);
            var circle = new Circle(r);
            var sphere = new Sphere(r);
            var cylinder = new Cylinder(r, p);
            Console.WriteLine("Area of Cuadrado = {0:F2}", cuadrado.Area());
            Console.WriteLine("Area of Circle   = {0:F2}", circle.Area());
            Console.WriteLine("Area of Sphere   = {0:F2}", sphere.Area());
            Console.WriteLine("Area of Cylinder = {0:F2}", cylinder.Area());
            Console.ReadLine();

            var MyBrewMaster = new BrewMaster();
            MyBrewMaster.SetBeerBuilder(new AmberBuilder());
            MyBrewMaster.BrewBeer();
            Beer MyAmberBuilder = MyBrewMaster.GetBeer();
            MyBrewMaster.SetBeerBuilder(new StoutBuilder());
            MyBrewMaster.BrewBeer();
            Beer MyStoutBuilder = MyBrewMaster.GetBeer();
            Console.WriteLine(MyAmberBuilder);
            Console.WriteLine(MyStoutBuilder);
            Console.ReadLine();

            var teacherAct = new Teacher() { First = "John", Last = "Kennedy", City = "Barcelona" };
            //se instancias mediante un delegado Action
            Action method = teacherAct.ToConsole;
            method();
            //métodos anónimos
            method = delegate() { teacherAct.ToConsole(); };
            method();
            //expresión lambda para instanciar el delegado Action
            method = () => teacherAct.ToConsole();
            method();

            Action<string> messageTarget;
            messageTarget = ShowWindowsMessage;
            messageTarget("Action => messageTarget");
            messageTarget = Console.WriteLine;
            messageTarget("Action => messageTarget " + Environment.GetCommandLineArgs().Length.ToString());
            Console.WriteLine("--------");

            //delegado Func<TResult>, no es necesario definir explícitamente un delegado que
            //encapsule un método sin parámetros
            OutputTarget output = new OutputTarget();
            Func<bool> methodCall = output.SendToFile;
            if (methodCall())
                Console.WriteLine("Success!");
            Console.WriteLine("--------");
            // Lambda expression as executable code.
            Func<int, bool> deleg = i => i < 5;
            Console.WriteLine("Expresión evaluada 4 < 5 Devuelve {0}", deleg(4));
            deleg = i => i > 2;
            Console.WriteLine("Expresión evaluada 3 > 2 Devuelve {0}", deleg(3));
            Func<double, double, double> TriangleArea = (b, h) => b * h / 2;
            Console.WriteLine("Expresión envia 7 y 12 Devuelve {0}", TriangleArea(7, 12));
            Console.WriteLine("--------");
            // Lambda expression as data in the form of an expression tree.
            Expression<Func<int, bool>> expr = i => i < 5;
            deleg = expr.Compile();
            Console.WriteLine("Expresión evaluada 4 < 5 Devuelev {0}", deleg(4));
            expr = i => i > 2;
            deleg = expr.Compile();
            Console.WriteLine("Expresión evaluada 3 > 2 Devueleve {0}", deleg(3));
            Expression<Func<double, double, double>> TriangleAreaExp = (b, h) => b * h / 2;
            Console.WriteLine("Expresión envia 7 y 12 Devuelve {0}", TriangleAreaExp.Compile()(7, 12));
            Console.WriteLine("--------");
            Expression<Func<int, int>> Double = (n) => n * 2;
            Expression<Func<int, int>> CallExp = (x) => Double.Compile()(x) + 1;
            Console.WriteLine("Expresion (n * 2) + 1 = {0}", CallExp.Compile()(5));
            Expression top = Double.Body;
            Expression constant1 = top.Right() as Expression;
            Expression constant2 = top.Left() as Expression;
            Console.WriteLine("Right: {0} Left: {1}", constant1, constant2);
            Console.WriteLine("--------");
            // Representa un objeto cuyos miembros se pueden agregar y quitar de forma dinámica en tiempo de ejecución
            dynamic obj = new ExpandoObject();
            obj.Mensaje = @"La secuencia de escape \n no se procesan => ";
            obj.number = 10;
            obj.Evento = null;
            obj.Evento += (Action<object, EventArgs>)((o, e) => Console.WriteLine("\nEvento: " + o.GetType() + " e: " + e.ToString()));
            obj.Metodo = (Action<string>)((x) =>
                    {
                        obj.Evento(obj, EventArgs.Empty);
                        Console.WriteLine(x + " number: " + obj.number);
                    });
            obj.Increment = (Action)(() => { obj.number++; });
            obj.Metodo(obj.Mensaje);
            obj.Increment();
            obj.Metodo("Nuevo valor:");
            Console.ReadLine();
            Console.Clear();

            List<Stock> listStock = new List<Stock>();
            Stock stock = new Stock { Name = "Stock Demo 1", Ticker = "AGHQ", Shares = 100000M };
            stock.Quotes = new List<Quote>
            {
                new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 45 ), Price = 57.63M },
                new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 56 ), Price = 56.92M },
                new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 08 ), Price = 57.05M },
                new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 56.87M },
                new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 59.00M }
            };
            listStock.Add(stock);
            stock = new Stock { Name = "Stock Demo 2", Ticker = "JTYU", Shares = 200000M };
            stock.Quotes = new List<Quote>
                {
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 45 ), Price = 17.63M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 56 ), Price = 56.92M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 08 ), Price = 97.05M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 46.87M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 69.00M }
                };
            listStock.Add(stock);
            stock = new Stock { Name = "Stock Demo 3", Ticker = "KLWS", Shares = 300000M };
            stock.Quotes = new List<Quote>
                {
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 45 ), Price = 87.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 56 ), Price = 56.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 08 ), Price = 37.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 16.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 59.00M }
                };
            listStock.Add(stock);
            stock = new Stock { Name = "Stock Demo 4", Ticker = "JTYU", Shares = 356820M };
            stock.Quotes = new List<Quote>
                {
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 45 ), Price = 87.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 8, 56 ), Price = 56.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 08 ), Price = 37.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 16.00M },
                    new Quote {Time = new DateTime(2007, 12, 5, 11, 9, 23 ), Price = 59.00M }
                };
            listStock.Add(stock);

            var queryGroup = from item in listStock
                             group item by item.Ticker into ticker
                             select new
                             {
                                 ticker = ticker.Key,
                                 shares = ticker.Sum(u => u.Shares)
                             };
            // Aggregate
            Quote aggQuote = stock.Quotes.MinPrice();
            var average = stock.Quotes.Average(x => x.Price);
            aggQuote.MargenAlcanzado += MargenAlcanzadoExecute;
            aggQuote.VerificarMargen(new Decimal(10), new Decimal(17.8));
            var paralle = from item in listStock.AsParallel()
                          .AsOrdered()
                          .WithDegreeOfParallelism(Environment.ProcessorCount / 2)
                          select item;
            foreach (var ii in paralle)
                Console.WriteLine("Name: {0}  Ticket: {1}", ii.Name, ii.Ticker);
            Console.ReadLine();
            Console.Clear();

            string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
            // Determine whether any string in the array is longer than "banana".
            string longestName = fruits.Aggregate("banana",
                (longest, next) => next.Length > longest.Length ? next : longest, fruit => fruit.ToUpper());
            Console.WriteLine("The fruit with the longest name is {0}.", longestName);
            ///LINQ to Entities: La combinación de predicados (Ver class ParameterRebinder y class Extensions)
            Expression<Func<Stock, bool>> IsName1 = c1 => c1.Name == "Stock Demo 3";
            Expression<Func<Stock, bool>> IsTicker1 = c1 => c1.Ticker == "JTYU";
            Console.WriteLine("{0}  Result: {1}", IsName1.ToString(), IsName1.Compile()(stock));
            Console.WriteLine("{0}  Result: {1}", IsTicker1.ToString(), IsTicker1.Compile()(stock));
            Expression<Func<Stock, bool>> IsNameOrIsTicker = IsName1.Or(IsTicker1);
            Expression<Func<Stock, bool>> IsNameAndIsTicker = IsName1.And(IsTicker1);
            Console.WriteLine("{0}  Result: {1}", IsNameOrIsTicker.ToString(), IsNameOrIsTicker.Compile()(stock));
            Console.WriteLine("{0}  Result: {1}", IsNameAndIsTicker.ToString(), IsNameAndIsTicker.Compile()(stock));

            ParameterExpression c2 = Expression.Parameter(typeof(Stock), "c2");
            Expression IsName2 = Expression.Equal(Expression.Property(c2, "Name"), Expression.Constant("Stock Demo 3"));
            Expression IsTicker2 = Expression.Equal(Expression.Property(c2, "Ticker"), Expression.Constant("JTYU"));
            IsNameOrIsTicker = Expression.Lambda<Func<Stock, bool>>(Expression.Or(IsName2, IsTicker2), c2);
            IsNameAndIsTicker = Expression.Lambda<Func<Stock, bool>>(Expression.And(IsName2, IsTicker2), c2);
            Console.WriteLine("{0}  Result: {1}", IsNameOrIsTicker.ToString(), IsNameOrIsTicker.Compile()(stock));
            Console.WriteLine("{0}  Result: {1}", IsNameAndIsTicker.ToString(), IsNameAndIsTicker.Compile()(stock));
            Console.ReadLine();
            Console.Clear();

            DataSet _studentDS = new DataSet();
            _studentDS.Tables.Add("Student");
            DataColumn _idColumna = new DataColumn();
            _idColumna.ColumnName = "ID";
            _idColumna.DataType = Type.GetType("System.Int32");
            _studentDS.Tables["Student"].Columns.Add(_idColumna);
            _studentDS.Tables["Student"].Columns.Add("First");
            _studentDS.Tables["Student"].Columns.Add("Last");
            _studentDS.Tables["Student"].Columns.Add("City");
            foreach (var _item in students)
            {
                DataRow _fila = _studentDS.Tables["Student"].NewRow();
                _fila["First"] = _item.First;
                _fila["Last"] = _item.Last;
                _fila["City"] = _item.Street;
                _studentDS.Tables["Student"].Rows.Add(_fila);
            }
            IEnumerable<DataRow> query = from datos in _studentDS.Tables["Student"].AsEnumerable()
                                         where datos.Field<string>("Last").Contains("a")
                                         select datos;
            foreach (DataRow _fila in query)
                Console.WriteLine("First: {0}  Last: {1}", _fila.Field<string>("First"), _fila.Field<string>("Last"));
            Console.ReadLine();
            Console.Clear();

            IEnumerable<DataRow> query1 = from datos in _studentDS.Tables["Student"].AsEnumerable()
                                          select datos;
            IEnumerable<DataRow> query2 = query1.Where(u => u.Field<string>("First").StartsWith("J"));
            IEnumerable<DataRow> query2a = query2.ToArray();
            foreach (DataRow _fila in query2a)
                Console.WriteLine("First: {0}  Last: {1}", _fila.Field<string>("First"), _fila.Field<string>("Last"));
            Console.ReadLine();
            Console.Clear();

            List<Expression<Func<Student, object>>> expressions = new List<Expression<Func<Student, object>>>();
            var properties = typeof(Student).GetProperties();
            foreach (var property in properties)
            {
                Expression<Func<Student, object>> exp = u => (u.GetType().InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null));
                Console.WriteLine("Ordenar por:" + property.Name);
                IQueryable<Student> studentsLinq = students.AsQueryable().OrderBy<Student, object>(exp);
                foreach (Student item in studentsLinq)
                    Console.WriteLine("First: {0}   Last: {1}   Street: {2}   City: {3}",
                        item.First, item.Last, item.Street, item.City);
                Console.WriteLine();
                //ConstantExpression instance = Expression.Constant(new Student());
                //MemberExpression propertyExpression = Expression.Property(instance, property);
                //exp = u => propertyExpression;
                expressions.Add(exp);
            }

            Console.ReadLine();
            Console.Clear();

            foreach (var exp in expressions)
            {
                Console.WriteLine("Ordenar por:");
                IQueryable<Student> studentsLinq1 = students.AsQueryable().OrderBy<Student, object>(exp);
                foreach (Student item in studentsLinq1)
                    Console.WriteLine("First: {0}   Last: {1}   Street: {2}   City: {3}",
                        item.First, item.Last, item.Street, item.City);
            }

            Console.ReadLine();
            Console.Clear();

            var parameter = Expression.Parameter(typeof(Student), "u");
            // u.First == "Andrea"
            var equalExpression = Expression.Equal(Expression.PropertyOrField(parameter, "First"), Expression.Constant("Andrea", typeof(string)));
            // u => u.First == "Andrea"
            Expression<Func<Student, bool>> lambda1 = (Expression<Func<Student, bool>>)Expression.Lambda(equalExpression, parameter);
            Expression<Func<Student, object>> lambda2 = u => (u.GetType().InvokeMember("Last", BindingFlags.GetProperty, null, u, null));
            IQueryable<Student> studentsLinq2 = students.AsQueryable().Where(lambda1).OrderBy(lambda2);
            foreach (Student item in studentsLinq2)
                Console.WriteLine("First: {0}   Last: {1}   Street: {2}   City: {3}",
                    item.First, item.Last, item.Street, item.City);

            Console.ReadLine();
        }

        static void ShowWindowsMessage(string message)
        {
            MessageBox.Show(message);
        }

        static void MargenAlcanzadoExecute(object sender, EventArgs e)
        {
            Console.WriteLine("El precio esta por debajo del margen");
        }
        #endregion

        #region Prueba03() // Operaciones asincrónicas
        // The delegate must have the same signature as the method
        // it will call asynchronously.
        delegate string AsyncMethodCaller(TimeSpan callDuration, out int threadId);
        static void Prueba03()
        {
            // El método asíncrono pone el identificador de subproceso aquí
            int threadId;
            AsyncDemo asyncDemo = new AsyncDemo();
            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(asyncDemo.TestMethod);
            IAsyncResult asyncResult = caller.BeginInvoke(TimeSpan.FromSeconds(15), out threadId, null, null);
            Console.WriteLine("Tarea en ejecución: {0}:{1}  Main thread {2} does some work Id {3}",
                DateTime.Now.Minute, DateTime.Now.Second, Thread.CurrentThread.ManagedThreadId, threadId );
            // Espere a que el WaitHandle se señalice
            for (int ii = 1; ii <= 6; ii++)
            {
                Console.WriteLine("Procesa =>For: {0}", ii);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            //Console.WriteLine("=> Block");
            //asyncResult.AsyncWaitHandle.WaitOne();
            //Console.WriteLine("=> UnBlock");

            // Llama EndInvoke para recuperar los resultados.
            string result = caller.EndInvoke(out threadId, asyncResult);
            // Close the wait handle.
            asyncResult.AsyncWaitHandle.Close();

            Console.WriteLine("{0}  Call executed on thread {1}", result, threadId);
            Console.WriteLine("===========================");
            Console.ReadLine();
        }
        #endregion

        #region Prueba04() //Procesos en paralelo
        static void Prueba04() 
        {
            for (int ii = 0; ii < 3; ii++)
            {
                Parallel.Invoke(() => Prueba04C(), () => Prueba04B(), () => Prueba04A());
                Console.WriteLine();
            }
            Console.WriteLine("completed");
            Console.ReadLine();
            Console.Clear();

            Parallel.For(0, 40, i =>
            {
                Console.WriteLine("i(1)= {0}", i);
            });
            var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            Parallel.For(0, 40, options, i =>
            {
                Console.WriteLine("i(2)= {0}", i);
            });
            Parallel.For(0, 100, options, (i, loop) =>
            {
                Console.WriteLine("i(3)= {0}", i);
                if (i == 20)
                    loop.Break();
            });
            Console.WriteLine("completed");
            Console.ReadLine();
            Console.Clear();

            Action<object> action = (object obj) =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(6));
                Console.WriteLine("Obj={0}, Task={1}, Thread={2}",
                    obj.ToString(), Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            };
            Console.WriteLine("Main Thread={0}", Thread.CurrentThread.ManagedThreadId);
            Task task1 = new Task(action, "task1");
            Task task2 = Task.Factory.StartNew(action, "task2");
            task1.Start();
            Task task3 = new Task(action, "task3");
            task3.RunSynchronously();
            task1.Wait();
            task2.Wait();
            Console.WriteLine("completed");
            Console.ReadLine();
            Console.Clear();

            Task task5 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("task5 beginning.");
                Task child1 = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(4));
                    Console.WriteLine("task5 child completed.");
                });
                Task<string[]> child2 = Task<string[]>.Factory.StartNew(() =>
                {
                    string path = @"C:\Users\chhjc\source\repos\chhjc67";
                    string[] files = System.IO.Directory.GetFiles(path);
                    var result = (from file in files.AsParallel()
                                    let info = new System.IO.FileInfo(file)
                                    where info.Extension == ".pdf"
                                    select file).ToArray();
                    return result;
                });
                foreach (var name in child2.Result)
                    Console.WriteLine(name);
            });
            task5.Wait();
            Console.WriteLine("task5... ");
            Task task6 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("task6 beginning.");
                Task child = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    Console.WriteLine("task6 child completed.");
                }, TaskCreationOptions.AttachedToParent);
            });
            task6.Wait();
            Console.WriteLine("task6 completed.");
            Console.ReadLine();
            Console.Clear();

            // Temporizadores de subprocesos
            StateObject stateObject = new StateObject();
            stateObject.TimerCanceled = false;
            stateObject.SomeValue = 1;
            TimerCallback timerDelegate = new TimerCallback(Prueba04D);
            Console.WriteLine("beginning {0}:{1} ", DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            // Create a timer that calls a procedure every 2 seconds.
            // Note: There is no Start method; the timer starts running as soon as 
            // Save a reference for Dispose.
            stateObject.TimerReference = new System.Threading.Timer(timerDelegate, stateObject, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            // Run for ten loops.
            while (stateObject.SomeValue < 10) 
            {
                Console.WriteLine(stateObject.SomeValue);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (stateObject.SomeValue == 6)
                    stateObject.TimerReference.Change(TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds(4));
            }
            // Request Dispose of the timer object.
            Thread.Sleep(TimeSpan.FromSeconds(8));
            stateObject.TimerCanceled = true;
            Console.WriteLine("Completed {0}:{1}", DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            Console.ReadLine();
        }

        static void Prueba04A()
        {
            int[] nums = Enumerable.Range(0, 100000).ToArray();
            //int[] nums = { 100, 1000, 10000, 10, 1 };
            long total = 0;
            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            }, (finalResult) => Interlocked.Add(ref total, finalResult));
            Console.WriteLine("For-The total is {0:N0}", total);
        }

        static void Prueba04B()
        {
            int[] nums = Enumerable.Range(0, 100000).ToArray();
            //int[] nums = { 100, 1000, 10000, 10, 1 };
            long total = 0;
            Parallel.ForEach<int, long>(nums, () => 0, (j, loop, subtotal) =>
            {
                subtotal += j;
                return subtotal;
            }, (finalResult) => Interlocked.Add(ref total, finalResult));
            Console.WriteLine("ForEach-The total is {0:N0}", total);
        }

        static void Prueba04C()
        {
            int[] nums = Enumerable.Range(0, 100000).ToArray();
            long total = 0;
            for (int ii = 0; ii < nums.Count(); ii++)
            {
                total += nums[ii];
            }
            Console.WriteLine("The total is {0:N0}", total);
        }

        static void Prueba04D(object stateObject)
        {
            StateObject state = (StateObject)stateObject;
            // Use the interlocked class to increment the counter variable.
            Interlocked.Increment(ref state.SomeValue);
            Console.WriteLine("Launched new thread: {0}:{1} ", DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            if (state.TimerCanceled)
            // Dispose Requested.
            {
                state.TimerReference.Dispose();
                Console.WriteLine("Canceled: {0}:{1} ", DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString());
            }
        }
        #endregion

        static void Prueba05() // Cálculo del dígito de control EAN (Código de barra)
        {
            int iSum = 0;
            int iSumInpar = 0;
            int iDigit = 0;
            string EAN = "123456789041";
            EAN = EAN.PadLeft(17, '0');
            for (int i = EAN.Length; i >= 1; i--)
            {
                iDigit = Convert.ToInt32(EAN.Substring(i - 1, 1));
                if (i % 2 != 0)
                {
                    iSumInpar += iDigit;
                }
                else
                {
                    iSum += iDigit;
                }
            }
            iDigit = (iSumInpar * 3) + iSum;
            int iCheckSum = (10 - (iDigit % 10)) % 10;
            Console.Write("Digito de control: " + iCheckSum.ToString());
            Console.ReadLine();
        }

        static void Prueba06()  //Crear e inicializar agentes de escucha de seguimiento
        {
            string dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FileStream myFileStream = new FileStream(dir + @"\TraceListener.log", FileMode.OpenOrCreate);
            TextWriterTraceListener myListener1 = new TextWriterTraceListener(myFileStream);
            TextWriterTraceListener myListener2 = new TextWriterTraceListener(Console.Out);
            Debug.Listeners.Add(myListener1);
            Debug.Listeners.Add(myListener2);
            Debug.AutoFlush = true;
            Debug.WriteLine("----------------");
            Debug.Indent();
            Debug.WriteLine("Escucha Inicio");
            Debug.Indent();
            Debug.WriteLine("Error: File not found");
            Debug.WriteLine("Error: Directory not found");
            Debug.Unindent();
            Debug.WriteLine("Escucha Final");
            Debug.Unindent();
            Debug.WriteLine("----------------");

            Debug.Assert(false);
            TraceSource mySource = new TraceSource("ConsoleAppPrueba");
            //Actividad 1
            mySource.TraceEvent(TraceEventType.Warning, 11, "Actividad 1");
            mySource.TraceEvent(TraceEventType.Error, 12, "Actividad 1");
            mySource.TraceEvent(TraceEventType.Critical, 13, "Actividad 1");
            mySource.TraceEvent(TraceEventType.Information, 14, "Actividad 1");
            mySource.TraceInformation("TraceInformation Actividad 1");
            Debug.WriteLine("----------------");
            // Guarda la configuración originar del archivo de configuración
            EventTypeFilter configFilter = (EventTypeFilter)mySource.Listeners["console"].Filter;
            // Asigna un nuevo filtro para los mensajes de seguimiento 
            mySource.Listeners["console"].Filter = new EventTypeFilter(SourceLevels.Critical);
            //Actividad 2
            mySource.TraceEvent(TraceEventType.Warning, 21, "Actividad 2");
            mySource.TraceEvent(TraceEventType.Error, 22, "Actividad 2");
            mySource.TraceEvent(TraceEventType.Critical, 23, "Actividad 2");
            mySource.TraceEvent(TraceEventType.Information, 24, "Actividad 2");
            mySource.TraceInformation("TraceInformation Actividad 2");
            Debug.WriteLine("----------------");
            // Restaura configuración originar del archivo de configuración
            mySource.Listeners["console"].Filter = configFilter;
            //Actividad 3
            mySource.TraceEvent(TraceEventType.Warning, 31, "Actividad 3");
            mySource.TraceEvent(TraceEventType.Error, 32, "Actividad 3");
            mySource.TraceEvent(TraceEventType.Critical, 33, "Actividad 3");
            mySource.TraceEvent(TraceEventType.Information, 34, "Actividad 3");
            mySource.TraceInformation("TraceInformation Actividad 3");
            Debug.WriteLine("----------------");
            // Esta sentencia sobreescribe cualquier configuración y permite eschucar cualquier tipo de evento
            mySource.Switch.Level = SourceLevels.All;
            //Actividad 4
            mySource.TraceEvent(TraceEventType.Warning, 41, "Actividad 4");
            mySource.TraceEvent(TraceEventType.Error, 42, "Actividad 4");
            mySource.TraceEvent(TraceEventType.Critical, 43, "Actividad 4");
            mySource.TraceEvent(TraceEventType.Information, 44, "Actividad 4");
            mySource.TraceInformation("TraceInformation Actividad 4");
            mySource.Close();
            Console.ReadLine();
        }

        static void Prueba07()
        {
            FileInfo file = new FileInfo(DEST1);
            file.Directory.Create();
            new Program().ManipulatePdf(DEST1);
            new Program().CreatePdf(DEST2);
        }

        static void Prueba08() //Se utiliza la libreria "DocumentFormat.OpenXml" para general un archivo Excel 
        {
            GeneratedClass x = new GeneratedClass();
            x.CreateDocument("D:\\Test1.xlsx");
            ReportWithChart y = new ReportWithChart();
            y.CreateDocument("D:\\Test2.xlsx");
        }

        private void ManipulatePdf(String dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();

            for (int i = 0; i < 17; i++)
            {
                table.AddCell("hi");
            }

            doc.Add(table);
            doc.Close();
        }

        private void CreatePdf(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            //Add new page
            PageSize ps = PageSize.A4;
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);
            IList<String> text = new List<String>();
            text.Add("         Episode V         ");
            text.Add("  THE EMPIRE STRIKES BACK  ");
            text.Add("It is a dark time for the");
            text.Add("Rebellion. Although the Death");
            text.Add("Star has been destroyed,");
            text.Add("Imperial troops have driven the");
            text.Add("Rebel forces from their hidden");
            text.Add("base and pursued them across");
            text.Add("the galaxy.");
            text.Add("Evading the dreaded Imperial");
            text.Add("Starfleet, a group of freedom");
            text.Add("fighters led by Luke Skywalker");
            text.Add("has established a new secret");
            text.Add("base on the remote ice world");
            text.Add("of Hoth...");
            //Replace the origin of the coordinate system to the top left corner
            canvas.ConcatMatrix(1, 0, 0, 1, 0, ps.GetHeight());
            canvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD), 14)
                .SetLeading(14 * 1.2f).MoveText(70, -40);
            foreach (String s in text)
            {
                //Add text and move to the next line
                canvas.NewlineShowText(s);
            }
            canvas.EndText();
            //Close document
            pdf.Close();
        }
    }

    #region Class Dimensions
    class Dimensions
    {
        public const double PI = Math.PI;
        protected double _x, _y;
        public Dimensions(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public virtual double Area()
        {
            return _x * _y;
        }
    }

    class Circle : Dimensions
    {
        public Circle(double r)
            : base(r, 0)
        {
        }

        public override double Area()
        {
            return PI * _x * _x;
        }
    }

    class Sphere : Dimensions
    {
        public Sphere(double r)
            : base(r, 0)
        {
        }

        public override double Area()
        {
            return 4 * PI * _x * _x;
        }
    }

    class Cylinder : Dimensions
    {
        public Cylinder(double r, double h)
            : base(r, h)
        {
        }

        public override double Area()
        {
            return 2 * PI * _x * _x + 2 * PI * _x * _y;
        }
    }
    #endregion

    #region Class Beer
    class Beer
    {
        public string Label { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public double Potency { get; set; }

        public override string ToString()
        {
            return $"Label:{Label}  Volumen:{Volume} oz. Potencia:{Potency}% ABV Precio:{Price:C}";
        }
    }

    interface IBeerBuilder
    {
        void Brew();
        void Ferment();
        void Bottle();
        void Age();
        Beer GetBeer();
    }

    class BrewMaster
    {
        IBeerBuilder _beerBuilder;

        public void SetBeerBuilder(IBeerBuilder beerBuilder)
        {
            _beerBuilder = beerBuilder;
        }

        public Beer GetBeer()
        {
            return _beerBuilder.GetBeer();
        }

        public void BrewBeer()
        {
            _beerBuilder.Brew();
            _beerBuilder.Ferment();
            _beerBuilder.Bottle();
            _beerBuilder.Age();
        }
    }

    class AmberBuilder : IBeerBuilder
    {
        private Beer _beer = new Beer();

        public void Brew()
        {
            _beer.Volume = 64;
        }

        public void Ferment()
        {
            _beer.Potency = 5.5;
        }

        public void Bottle()
        {
            _beer.Label = "Amber";
        }

        public void Age()
        {
            _beer.Price = 16;
        }

        public Beer GetBeer()
        {
            return _beer;
        }
    }

    class StoutBuilder : IBeerBuilder
    {
        private Beer _beer = new Beer();

        public void Brew()
        {
            _beer.Volume = 72;
        }

        public void Ferment()
        {
            _beer.Potency = 6.5;
        }

        public void Bottle()
        {
            _beer.Label = "Stout";
        }

        public void Age()
        {
            _beer.Price = 22;
        }

        public Beer GetBeer()
        {
            return _beer;
        }
    }
    #endregion

    class OutputTarget
    {
        public bool SendToFile()
        {
            try
            {
                string fn = System.IO.Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(fn);
                sw.WriteLine("Hello, World!");
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    class Stock
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public decimal Shares { get; set; }
        public List<Quote> Quotes { get; set; }
        public override string ToString()
        {
            return $"Ticker:{Ticker} Shares:{Shares} Price (Min:{Quotes.Min(q => q.Price)} Max:{Quotes.Max(q => q.Price)})";
        }
    }

    class Quote
    {
        public decimal Price { get; set; }

        public DateTime Time { get; set; }

        public event EventHandler MargenAlcanzado;

        public void VerificarMargen(decimal porc, decimal price)
        {
            price = price * (1 - porc / 100);
            if (Price < price)
                OnMargenAlcanzado(EventArgs.Empty);
        }

        protected virtual void OnMargenAlcanzado(EventArgs e)
        {
            EventHandler handler = MargenAlcanzado;
            if (handler != null)
                handler(this, e);
        }
    }

    static class Extensions
    {
        public static Quote MinPrice(this IEnumerable<Quote> source)
        {
            return source.Aggregate((t, s) => t.Price < s.Price ? t : s);
        }

        public static Expression Left(this Expression exp)
        {
            return (exp as BinaryExpression).Left;
        }

        public static Expression Right(this Expression exp)
        {
            return (exp as BinaryExpression).Right;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    class ParameterRebinder : ExpressionVisitor //componer expresiones lambda sin usar invoke
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (_map.TryGetValue(p, out replacement))
                p = replacement;
            return base.VisitParameter(p);
        }
    }

    class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public List<int> Scores;
    }

    class Teacher
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string City { get; set; }

        public void ToConsole()
        {
            Console.WriteLine("First / Last: {0}, {1}", First, Last);
        }
    }

    class Person : IEquatable<Person>
    {
        private string uniqueSsn;
        private string lName;

        public Person(string lastName, string ssn, decimal quantity)
        {
            this.SSN = ssn;
            this.LastName = lastName;
            this.Quantity = quantity;
        }

        public string SSN
        {
            get { return this.uniqueSsn; }
            set
            {
                if (Regex.IsMatch(value, @"\d{9}"))
                    this.uniqueSsn = $"{value.Substring(0, 3)}-{value.Substring(3, 2)}-{value.Substring(5, 4)}";
                else if (Regex.IsMatch(value, @"\d{3}-\d{2}-\d{3}"))
                    this.uniqueSsn = value;
                else
                    throw new FormatException("The social security number has an invalid format.");
            }
        }

        public string LastName
        {
            get { return this.lName; }
            set
            {
                //if (String.IsNullOrEmpty(value))
                //    throw new NullReferenceException("The last name cannot be null or empty.");
                //else
                    this.lName = value;
            }
        }

        public decimal Quantity { get; set; }

        public bool Equals(Person other)
        {
            if (this.uniqueSsn == other.SSN)
            {
                this.Quantity += other.Quantity;
                return true;
            }
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return base.Equals(obj);

            if (!(obj is Person))
                throw new InvalidCastException("The 'obj' argument is not a Person object.");
            else
                return Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            return this.SSN.GetHashCode();
        }

        public Person Clone()
        {
            return (Person)this.MemberwiseClone();
        }
    }

    class AsyncDemo
    {
        public string TestMethod(TimeSpan callDuration, out int threadId)
        {
            Console.WriteLine("TestMethod begins.");
            for (int ii = 10; ii <= 16; ii++)
            {
                Console.WriteLine("TestMethod =>: {0}", ii);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            threadId = Thread.CurrentThread.ManagedThreadId;
            return $"Resultado: {DateTime.Now.Minute}:{DateTime.Now.Second}";
        }
    }

    class StateObject
    {
        // Used to hold parameters for calls to TimerTask.
        public int SomeValue;
        public System.Threading.Timer TimerReference;
        public bool TimerCanceled;
    }

    #region Clase de la Prueba08
    class GeneratedClass
    {
        public void CreateDocument(string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>();
                WorksheetPart worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart1.Worksheet = new Worksheet();
                worksheetPart2.Worksheet = new Worksheet();
                WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                workbookStylesPart.Stylesheet = GenerateStylesheet();
                workbookStylesPart.Stylesheet.Save();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet1 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart1), SheetId = 1, Name = "Sheet1" };
                Sheet sheet2 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart2), SheetId = 2, Name = "Sheet2" };
                sheets.Append(sheet1);
                sheets.Append(sheet2);
                workbookPart.Workbook.Save();
                TestContent1(worksheetPart1);
                worksheetPart1.Worksheet.Save();
                TestContent2(worksheetPart2);
                worksheetPart2.Worksheet.Save();
            }
        }

        private DocumentFormat.OpenXml.Spreadsheet.Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex,
            };
        }

        private Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;
            Fonts fonts = new Fonts(
                new Font(new FontSize() { Val = 10 }), // Index 0 - default
                new Font(new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "000000" }) // Index 1 - header
                );
            Fills fills = new Fills(
                new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                new Fill(new PatternFill(TranslateForeground(System.Drawing.Color.DodgerBlue))
                { PatternType = PatternValues.Solid }), // Index 2 - header
                new Fill(new PatternFill(TranslateForeground(System.Drawing.Color.WhiteSmoke))
                { PatternType = PatternValues.Solid }) // Index 3 - body
                );
            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );
            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true }, // header
                    new CellFormat { FontId = 0, FillId = 3, BorderId = 1, ApplyBorder = true } // body
                );
            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }

        private static ForegroundColor TranslateForeground(System.Drawing.Color fillColor)
        {
            return new ForegroundColor()
            {
                Rgb = new HexBinaryValue()
                {
                    Value =
                        System.Drawing.ColorTranslator.ToHtml(
                        System.Drawing.Color.FromArgb(
                            fillColor.A,
                            fillColor.R,
                            fillColor.G,
                            fillColor.B)).Replace("#", "")
                }
            };
        }

        private void TestContent1(WorksheetPart worksheetPart)
        {
            // Setting up columns
            Columns columns = new Columns(
                    new Column // Id column
                    {
                        Min = 1,
                        Max = 1,
                        Width = 4,
                        CustomWidth = true
                    },
                    new Column // Name and Birthday columns
                    {
                        Min = 2,
                        Max = 3,
                        Width = 15,
                        CustomWidth = true
                    },
                    new Column // Salary column
                    {
                        Min = 4,
                        Max = 4,
                        Width = 8,
                        CustomWidth = true
                    });
            worksheetPart.Worksheet.AppendChild(columns);
            List<Employee> employees = Employees.EmployeesList;
            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
            // Constructing header
            Row row = new Row();
            row.Append(
                ConstructCell("Id", CellValues.String, 2),
                ConstructCell("Name", CellValues.String, 2),
                ConstructCell("Birth Date", CellValues.String, 2),
                ConstructCell("Salary", CellValues.String, 2));
            // Insert the header row to the Sheet Data
            sheetData.AppendChild(row);
            // Inserting each employee
            uint styleIndex = 1;
            Int32 count = 0;

            foreach (var employee in employees)
            {
                if (count % 2 == 0)
                    styleIndex = 1;
                else
                    styleIndex = 3;

                row = new Row();
                row.Append(
                    ConstructCell(employee.Id.ToString(), CellValues.Number, styleIndex),
                    ConstructCell(employee.Name, CellValues.String, styleIndex),
                    ConstructCell(employee.DOB.ToString("yyyy/MM/dd"), CellValues.String, styleIndex),
                    ConstructCell(employee.Salary.ToString(), CellValues.Number, styleIndex));
                sheetData.AppendChild(row);
                count++;
            }
        }

        private void TestContent2(WorksheetPart worksheetPart)
        {
            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
            Row row1 = new Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell1 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "A1",
                DataType = CellValues.String,
                CellValue = new CellValue("Valor 1")
            };
            DocumentFormat.OpenXml.Spreadsheet.Cell cell2 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "B1",
                DataType = CellValues.String,
                CellValue = new CellValue("Valor 2"),
            };
            DocumentFormat.OpenXml.Spreadsheet.Cell cell3 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "C1",
                DataType = CellValues.String,
                CellValue = new CellValue("Total"),
            };
            row1.Append(cell1, cell2, cell3);
            sheetData.Append(row1);
            row1 = new Row() { RowIndex = 2 };
            DocumentFormat.OpenXml.Spreadsheet.Cell cell4 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "A2",
                DataType = CellValues.Number,
                CellValue = new CellValue("2500.5")
            };
            DocumentFormat.OpenXml.Spreadsheet.Cell cell5 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "B2",
                DataType = CellValues.Number,
                CellValue = new CellValue("528")
            };
            DocumentFormat.OpenXml.Spreadsheet.Cell cell6 = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellReference = "C2",
                DataType = CellValues.Number,
                CellFormula = new CellFormula("SUM(A2:B2)")
            };
            row1.Append(cell4, cell5, cell6);
            sheetData.Append(row1);
        }
    }

    class ReportWithChart
    {
        public void CreateDocument(string fileName)
        {
            List<Employee> employees = Employees.EmployeesList;

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Students" };
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                // Add drawing part to WorksheetPart
                DrawingsPart drawingsPart = worksheetPart.AddNewPart<DrawingsPart>();
                worksheetPart.Worksheet.Append(new Drawing() { Id = worksheetPart.GetIdOfPart(drawingsPart) });
                worksheetPart.Worksheet.Save();
                drawingsPart.WorksheetDrawing = new WorksheetDrawing();
                sheets.Append(sheet);
                workbookPart.Workbook.Save();
                // Add a new chart and set the chart language
                ChartPart chartPart = drawingsPart.AddNewPart<ChartPart>();
                chartPart.ChartSpace = new ChartSpace();
                chartPart.ChartSpace.AppendChild(new EditingLanguage() { Val = "en-US" });
                Chart chart = chartPart.ChartSpace.AppendChild(new Chart());
                chart.AppendChild(new AutoTitleDeleted() { Val = true }); // We don't want to show the chart title
                // Create a new Clustered Column Chart
                PlotArea plotArea = chart.AppendChild(new PlotArea());
                Layout layout = plotArea.AppendChild(new Layout());
                BarChart barChart = plotArea.AppendChild(new BarChart(
                        new BarDirection() { Val = new EnumValue<BarDirectionValues>(BarDirectionValues.Column) },
                        new BarGrouping() { Val = new EnumValue<BarGroupingValues>(BarGroupingValues.Clustered) },
                        new VaryColors() { Val = false }
                    ));
                // Constructing header
                Row row = new Row();
                int rowIndex = 1;
                row.AppendChild(ConstructCell(string.Empty, CellValues.String));

                foreach (var month in Months.Short)
                    row.AppendChild(ConstructCell(month, CellValues.String));

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);
                rowIndex++;

                // Create chart series
                for (int i = 0; i < employees.Count; i++)
                {
                    BarChartSeries barChartSeries = barChart.AppendChild(new BarChartSeries(
                        new Index() { Val = (uint)i },
                        new Order() { Val = (uint)i },
                        new SeriesText(new NumericValue() { Text = employees[i].Name })
                    ));

                    // Adding category axis to the chart
                    CategoryAxisData categoryAxisData = barChartSeries.AppendChild(new CategoryAxisData());
                    // Category
                    // Constructing the chart category
                    string formulaCat = "Students!$B$1:$G$1";
                    StringReference stringReference = categoryAxisData.AppendChild(new StringReference()
                    {
                        Formula = new DocumentFormat.OpenXml.Drawing.Charts.Formula() { Text = formulaCat }
                    });
                    StringCache stringCache = stringReference.AppendChild(new StringCache());
                    stringCache.Append(new PointCount() { Val = (uint)Months.Short.Length });

                    for (int j = 0; j < Months.Short.Length; j++)
                        stringCache.AppendChild(new NumericPoint() { Index = (uint)j }).Append(new NumericValue(Months.Short[j]));
                }

                var chartSeries = barChart.Elements<BarChartSeries>().GetEnumerator();

                for (int i = 0; i < employees.Count; i++)
                {
                    row = new Row();
                    row.AppendChild(ConstructCell(employees[i].Name, CellValues.String));
                    chartSeries.MoveNext();
                    string formulaVal = string.Format("Students!$B${0}:$G${0}", rowIndex);
                    DocumentFormat.OpenXml.Drawing.Charts.Values values = chartSeries.Current.AppendChild(new DocumentFormat.OpenXml.Drawing.Charts.Values());
                    NumberReference numberReference = values.AppendChild(new NumberReference()
                    {
                        Formula = new DocumentFormat.OpenXml.Drawing.Charts.Formula() { Text = formulaVal }
                    });
                    NumberingCache numberingCache = numberReference.AppendChild(new NumberingCache());
                    numberingCache.Append(new PointCount() { Val = (uint)Months.Short.Length });

                    for (uint j = 0; j < employees[i].Values.Length; j++)
                    {
                        var value = employees[i].Values[j];
                        row.AppendChild(ConstructCell(value.ToString(), CellValues.Number));
                        numberingCache.AppendChild(new NumericPoint() { Index = j }).Append(new NumericValue(value.ToString()));
                    }

                    sheetData.AppendChild(row);
                    rowIndex++;
                }

                barChart.AppendChild(new DataLabels(
                                    new ShowLegendKey() { Val = false },
                                    new ShowValue() { Val = false },
                                    new ShowCategoryName() { Val = false },
                                    new ShowSeriesName() { Val = false },
                                    new ShowPercent() { Val = false },
                                    new ShowBubbleSize() { Val = false }
                                ));
                barChart.Append(new AxisId() { Val = 48650112u });
                barChart.Append(new AxisId() { Val = 48672768u });

                // Adding Category Axis
                plotArea.AppendChild(
                    new CategoryAxis(
                        new AxisId() { Val = 48650112u },
                        new Scaling(new DocumentFormat.OpenXml.Drawing.Charts.Orientation() { Val = new EnumValue<DocumentFormat.OpenXml.Drawing.Charts.OrientationValues>(DocumentFormat.OpenXml.Drawing.Charts.OrientationValues.MinMax) }),
                        new Delete() { Val = false },
                        new AxisPosition() { Val = new EnumValue<AxisPositionValues>(AxisPositionValues.Bottom) },
                        new TickLabelPosition() { Val = new EnumValue<TickLabelPositionValues>(TickLabelPositionValues.NextTo) },
                        new CrossingAxis() { Val = 48672768u },
                        new Crosses() { Val = new EnumValue<CrossesValues>(CrossesValues.AutoZero) },
                        new AutoLabeled() { Val = true },
                        new LabelAlignment() { Val = new EnumValue<LabelAlignmentValues>(LabelAlignmentValues.Center) }
                    ));

                // Adding Value Axis
                plotArea.AppendChild(
                    new ValueAxis(
                        new AxisId() { Val = 48672768u },
                        new Scaling(new DocumentFormat.OpenXml.Drawing.Charts.Orientation() { Val = new EnumValue<DocumentFormat.OpenXml.Drawing.Charts.OrientationValues>(DocumentFormat.OpenXml.Drawing.Charts.OrientationValues.MinMax) }),
                        new Delete() { Val = false },
                        new AxisPosition() { Val = new EnumValue<AxisPositionValues>(AxisPositionValues.Left) },
                        new MajorGridlines(),
                        new DocumentFormat.OpenXml.Drawing.Charts.NumberingFormat()
                        {
                            FormatCode = "General",
                            SourceLinked = true
                        },
                        new TickLabelPosition() { Val = new EnumValue<TickLabelPositionValues>(TickLabelPositionValues.NextTo) },
                        new CrossingAxis() { Val = 48650112u },
                        new Crosses() { Val = new EnumValue<CrossesValues>(CrossesValues.AutoZero) },
                        new CrossBetween() { Val = new EnumValue<CrossBetweenValues>(CrossBetweenValues.Between) }
                    ));

                chart.Append(
                        new PlotVisibleOnly() { Val = true },
                        new DisplayBlanksAs() { Val = new EnumValue<DisplayBlanksAsValues>(DisplayBlanksAsValues.Gap) },
                        new ShowDataLabelsOverMaximum() { Val = false }
                    );
                chartPart.ChartSpace.Save();

                // Positioning the chart on the spreadsheet
                TwoCellAnchor twoCellAnchor = drawingsPart.WorksheetDrawing.AppendChild(new TwoCellAnchor());

                twoCellAnchor.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker(
                        new ColumnId("0"),
                        new ColumnOffset("0"),
                        new RowId((rowIndex + 2).ToString()),
                        new RowOffset("0")
                    ));

                twoCellAnchor.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.ToMarker(
                        new ColumnId("8"),
                        new ColumnOffset("0"),
                        new RowId((rowIndex + 12).ToString()),
                        new RowOffset("0")
                    ));

                // Append GraphicFrame to TwoCellAnchor
                GraphicFrame graphicFrame = twoCellAnchor.AppendChild(new GraphicFrame());
                graphicFrame.Macro = string.Empty;

                graphicFrame.Append(new NonVisualGraphicFrameProperties(
                        new NonVisualDrawingProperties()
                        {
                            Id = 2u,
                            Name = "Sample Chart"
                        },
                        new NonVisualGraphicFrameDrawingProperties()
                    ));

                graphicFrame.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.Transform(
                        new DocumentFormat.OpenXml.Drawing.Offset() { X = 0L, Y = 0L },
                        new DocumentFormat.OpenXml.Drawing.Extents() { Cx = 0L, Cy = 0L }
                    ));

                graphicFrame.Append(new DocumentFormat.OpenXml.Drawing.Graphic(
                        new DocumentFormat.OpenXml.Drawing.GraphicData(
                                new ChartReference() { Id = drawingsPart.GetIdOfPart(chartPart) }
                            )
                        { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" }
                    ));
                twoCellAnchor.Append(new ClientData());
                drawingsPart.WorksheetDrawing.Save();
                worksheetPart.Worksheet.Save();
            }
        }

        private DocumentFormat.OpenXml.Spreadsheet.Cell ConstructCell(string value, CellValues dataType)
        {
            return new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
            };
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public decimal Salary { get; set; }
        public byte[] Values { get; set; }
    }

    public struct Months
    {
        public static string[] Short = {
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun"
            };
    }

    public sealed class Employees
    {
        static List<Employee> _employees;
        const int COUNT = 15;

        public static List<Employee> EmployeesList
        {
            private set { }
            get
            {
                return _employees;
            }
        }

        static Employees()
        {
            _employees = new List<Employee>();
            Random random = new Random();

            for (int i = 0; i < COUNT; i++)
            {
                _employees.Add(new Employee()
                {
                    Id = i,
                    Name = "Employee " + i,
                    DOB = new DateTime(1999, 1, 1).AddMonths(i),
                    Salary = random.Next(100, 10000) + 0.5m,
                    Values = (i % 2 == 0 ? new byte[] { 10, 25, 30, 15, 20, 19 } : new byte[] { 20, 15, 26, 30, 10, (byte)random.Next(1, 20) })
                });
            }
        }
    }

    //public class CustomStylesheet : Stylesheet
    //{
    //    public CustomStylesheet()
    //    {
    //        var cellStyleFormats = new CellStyleFormats();
    //        var cellFormat = new CellFormat
    //        {
    //            NumberFormatId = 0,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0
    //        };
    //        cellStyleFormats.Append(cellFormat);
    //        cellStyleFormats.Count =
    //           UInt32Value.FromUInt32((uint)cellStyleFormats.ChildElements.Count);

    //        uint iExcelIndex = 164;
    //        var numberingFormats = new NumberingFormats();
    //        var cellFormats = new CellFormats();
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = 0,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0
    //        };
    //        cellFormats.Append(cellFormat);
    //        var nformatDateTime = new NumberingFormat
    //        {
    //            NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
    //            FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm:ss")
    //        };
    //        numberingFormats.Append(nformatDateTime);
    //        var nformat4Decimal = new NumberingFormat
    //        {
    //            NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
    //            FormatCode = StringValue.FromString("#,##0.0000")
    //        };
    //        numberingFormats.Append(nformat4Decimal);
    //        var nformat2Decimal = new NumberingFormat
    //        {
    //            NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++),
    //            FormatCode = StringValue.FromString("#,##0.00")
    //        };
    //        numberingFormats.Append(nformat2Decimal);
    //        var nformatForcedText = new NumberingFormat
    //        {
    //            NumberFormatId = UInt32Value.FromUInt32(iExcelIndex),
    //            FormatCode = StringValue.FromString("@")
    //        };
    //        numberingFormats.Append(nformatForcedText);
    //        // index 1
    //        // Cell Standard Date format 
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = 14,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 2
    //        // Cell Standard Number format with 2 decimal placing
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = 4,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };

    //        cellFormats.Append(cellFormat);
    //        // Index 3
    //        // Cell Date time custom format
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatDateTime.NumberFormatId,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 4
    //        // Cell 4 decimal custom format
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformat4Decimal.NumberFormatId,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 5
    //        // Cell 2 decimal custom format
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformat2Decimal.NumberFormatId,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 6
    //        // Cell forced number text custom format
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatForcedText.NumberFormatId,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 7
    //        // Cell text with font 12 
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatForcedText.NumberFormatId,
    //            FontId = 1,
    //            FillId = 0,
    //            BorderId = 0,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 8
    //        // Cell text
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatForcedText.NumberFormatId,
    //            FontId = 0,
    //            FillId = 0,
    //            BorderId = 1,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 9
    //        // Coloured 2 decimal cell text
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformat2Decimal.NumberFormatId,
    //            FontId = 1,
    //            FillId = 3,
    //            BorderId = 2,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 10
    //        // Coloured cell text
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatForcedText.NumberFormatId,
    //            FontId = 0,
    //            FillId = 2,
    //            BorderId = 2,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        // Index 11
    //        // Coloured cell text
    //        cellFormat = new CellFormat
    //        {
    //            NumberFormatId = nformatForcedText.NumberFormatId,
    //            FontId = 1,
    //            FillId = 3,
    //            BorderId = 2,
    //            FormatId = 0,
    //            ApplyNumberFormat = BooleanValue.FromBoolean(true)
    //        };
    //        cellFormats.Append(cellFormat);
    //        numberingFormats.Count =
    //          UInt32Value.FromUInt32((uint)numberingFormats.ChildElements.Count);
    //        cellFormats.Count = UInt32Value.FromUInt32((uint)cellFormats.ChildElements.Count);
    //        this.Append(numberingFormats);
    //        this.Append(cellStyleFormats);
    //        this.Append(cellFormats);
    //        var css = new CellStyles();
    //        var cs = new CellStyle
    //        {
    //            Name = StringValue.FromString("Normal"),
    //            FormatId = 0,
    //            BuiltinId = 0
    //        };
    //        css.Append(cs);
    //        css.Count = UInt32Value.FromUInt32((uint)css.ChildElements.Count);
    //        this.Append(css);
    //        var dfs = new DifferentialFormats { Count = 0 };
    //        this.Append(dfs);
    //        var tss = new TableStyles
    //        {
    //            Count = 0,
    //            DefaultTableStyle = StringValue.FromString("TableStyleMedium9"),
    //            DefaultPivotStyle = StringValue.FromString("PivotStyleLight16")
    //        };
    //        this.Append(tss);
    //    }
    //}
    #endregion
}
