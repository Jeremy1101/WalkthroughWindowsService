using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

// EventLog 所在 Namespace
using System.Diagnostics;
// Timer 所在 Namespace
using System.Timers;

namespace WalkthroughWindowsService
{
    // 繼承 System.ServiceProcess.ServiceBase
    public partial class MyNewService : ServiceBase
    {
        private EventLog eventLog1;
        private Timer timer;
        // 紀錄 Timer 觸發幾次
        private int eventId = 1;

        public MyNewService()
        {
            InitializeComponent();

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("MySource"))
                EventLog.CreateEventSource("MySource", "MyNewLog");

            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Windows Service 啟動");

            timer = new Timer();
            // 1 分鐘 (60 秒) 觸發一次
            timer.Interval = 60000;
            timer.Elapsed += (sender, e) =>
            {
                // 利用事件識別碼來記錄 Timer 觸發幾次
                eventLog1.WriteEntry("Timer 觸發", EventLogEntryType.Information, eventId++);
            };
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Windows Service 停止");
            timer.Stop();
            timer = null;
        }
    }
}
