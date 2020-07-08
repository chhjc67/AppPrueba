using System;
using System.ServiceProcess;
using System.Threading;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Thread thread;
        bool flagStop = false;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Tarea.Escribir("Inicia");
            ThreadStart ts = new ThreadStart(ThreadRunProccess);
            thread = new Thread(ts);
            thread.Start();
        }

        protected override void OnStop()
        {
            flagStop = true;
            Tarea.Escribir("Finaliza");
        }

        // Se implemento para la depuración
        public void StartDebug()
        {
            OnStart(null);
            Thread.Sleep(TimeSpan.FromMinutes(5));
            flagStop = true;
            Tarea.Escribir("Finaliza");
        }

        protected void ThreadRunProccess()
        {
            //DateTime next = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //TimeSpan span = TimeSpan.FromDays(1);
            DateTime next = DateTime.Now;
            TimeSpan span = TimeSpan.FromMinutes(1);
            next += span;
            while (!flagStop)
            {
                if (DateTime.Now >= next)
                {
                    Tarea.Escribir("Ciclo");
                    next += span;
                }
            }
            flagStop = false;
        }
    }
}
