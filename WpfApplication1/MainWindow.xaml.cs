using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
//using SecurityCustom;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;
        public delegate void NextPrimeDelegate();
        private long num = 3;
        private bool continueCalculating = false;
        private bool notAPrime = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GenericIdentity MyIdentity = new GenericIdentity("MyIdentity");
            // Create generic principal.
            String[] MyStringArray = { "Manager", "Teller" };
            GenericPrincipal MyPrincipal = new GenericPrincipal(MyIdentity, MyStringArray);

            // Attach the principal to the current thread.
            // This is not required unless repeated validation must occur,
            // other code in your application must validate, or the 
            // PrincipalPermisson object is used. 
            AppDomain.CurrentDomain.SetThreadPrincipal(MyPrincipal);

            var MyDataList = new HashSet<MyData>();
            MyDataList.Add(new MyData { ID = "1", Details = "Details for 1" });
            MyDataList.Add(new MyData { ID = "2", Details = "Details for 2" });
            MyDataList.Add(new MyData { ID = "3", Details = "Details for 3" });
            MyDataList.Add(new MyData { ID = "4", Details = "Prueba" });
            this.ComboBox1.DataContext = MyDataList;
            this.ComboBox2.DataContext = MyDataList;

            String original = "John Kennedy";
            //var result = Encryptor.Encode(original, Encryptor.Process("keypass"));
            //this.textBox1.Text = result;
            //this.textBox2.Text = Encryptor.Decode(result, Encryptor.Process("keypass"));
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (continueCalculating)
            {
                continueCalculating = false;
                this.StartStopButton.Content = "Resume";
            }
            else
            {
                continueCalculating = true;
                this.StartStopButton.Content = "Stop";
                this.StartStopButton.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal, new NextPrimeDelegate(this.CheckNextNumber));
            }
        }

        public void CheckNextNumber()
        {
            // Reset flag.
            notAPrime = false;
            for (long i = 3; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    // Set not a prime flag to true.
                    notAPrime = true;
                    break;
                }
            }
            // If a prime number. 
            if (!notAPrime)
            {
                bigPrimeTextBlock.Text = num.ToString();
            }
            num += 2;
            if (continueCalculating)
            {
                this.StartStopButton.Dispatcher.BeginInvoke(
                    DispatcherPriority.SystemIdle, new NextPrimeDelegate(this.CheckNextNumber));
            }
        }

        public event EventHandler Cancel = delegate { };
        public delegate void UpdateProgressDelegate(int percentage);

        void CancelProcess(object sender, EventArgs e)
        {
            //cancel the process
            if (worker != null)
                worker.CancelAsync();
        }

        //this is the method that the deleagte will execute
        public void UpdateProgressText(int percentage)
        {
            //set our progress dialog text and value
            this.SearchProgressBar.Value = percentage;
        }

        private void StartButtonA_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher pd = this.Dispatcher;
            this.Cancel += new EventHandler(CancelProcess);
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            //worker.DoWork += new DoWorkEventHandler(bw_DoWork);
            worker.DoWork += delegate(object s, DoWorkEventArgs args)
                {
                    string test = (string)args.Argument;
                    BackgroundWorker _worker = s as BackgroundWorker;

                    for (int i = 0; i < 200; i++)
                    {
                        if (_worker.CancellationPending)
                        {
                            args.Cancel = true;
                            return;
                        }

                        Thread.Sleep(50);
                        Int32 perc = Convert.ToInt32((((decimal)i / 200) * 100));
                        _worker.ReportProgress(perc);
                        //create a new delegate for updating our progress text
                        //UpdateProgressDelegate update = new UpdateProgressDelegate(UpdateProgressText);
                        //pd.BeginInvoke(update, perc);
                    }
                    //Retornar un valor desde el proceso "DoWork event handler"
                    args.Result = String.Format("Finalizo! {0}", test);
                };
            //worker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            worker.ProgressChanged += delegate(object s, ProgressChangedEventArgs args)
                {
                    this.SearchProgressBar.Value = args.ProgressPercentage;
                };
            worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
                {
                    if (!args.Cancelled)
                        MessageBox.Show((string)args.Result);

                    this.StartButtonA.IsEnabled = true;
                    this.StartButtonB.IsEnabled = false;
                    this.SearchProgressBar.Value = 0;
                };
            //Proporcionar un parámetro al "DoWork event handler"
            worker.RunWorkerAsync("Test param");
            this.StartButtonA.IsEnabled = false;
            this.StartButtonB.IsEnabled = true;
        }

        private void StartButtonB_Click(object sender, RoutedEventArgs e)
        {
            Cancel(sender, e);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            SelectDataTemplate(lbi.Content);
        }

        private void SelectDataTemplate(object value)
        {
            string numberStr = value as string;
            if (numberStr != null)
            {
                int num;
                try
                {
                    num = Convert.ToInt32(numberStr);
                }
                catch
                {
                    return;
                }
                DataTemplate template;
                // Select one of the DataTemplate objects, based on the 
                // value of the selected item in the ComboBox.
                if (num % 2 != 0)
                {
                    template = this.rootStackPanel.Resources["oddNumberTemplate"] as DataTemplate;
                }
                else
                {
                    template = this.rootStackPanel.Resources["evenNumberTemplate"] as DataTemplate;
                }
                selectedItemDisplay.Child = template.LoadContent() as UIElement;
                TextBlock tb = FindVisualChild<TextBlock>(selectedItemDisplay);
                tb.Text = numberStr;
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        void Go()
        {
            double val = 0;
            while (val < 100)
            {
                Thread.Sleep(50);
                Dispatcher.Invoke(new action(() =>
                {
                    this.SearchProgressBar.Value += 0.5;
                    val = this.SearchProgressBar.Value;
                }));
            }
        }

        delegate void action();
        Thread thread = null;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ThreadStart ts = new ThreadStart(Go);
            thread = new Thread(ts);
            thread.Start();

            //if (Application.Current.Dispatcher.CheckAccess())
            //{
            //    this.SearchProgressBar.Value = 50;
            //}
            //else
            //{
            //    Application.Current.Dispatcher.BeginInvoke(
            //      DispatcherPriority.Background,
            //      new Action(() =>
            //      {
            //          this.SearchProgressBar.Value = 50;
            //      }));
            //}
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
                this.SearchProgressBar.Value = 0;
            }
        }
    }
}