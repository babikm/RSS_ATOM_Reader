using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CrudService
{
    public partial class Service1: ServiceBase {  
        Timer timer = new Timer(); // name space(using System.Timers;)
        HttpClient client = new HttpClient();
        public Service1() {  
            InitializeComponent();  
        }  
        protected override void OnStart(string[] args) {  
            WriteToFile("Service is started at " + DateTime.Now);
            client.DeleteAsync("http://localhost:5000/api/newsfeed/");
            WriteToFile("deleting content in database " + DateTime.Now);
            client.PostAsync("http://localhost:5000/api/newsfeed/", null);
            WriteToFile("Making Post procedure " + DateTime.Now);
            client.GetStringAsync("http://localhost:5000/api/newsfeed/");
            WriteToFile("Making Get procedure " + DateTime.Now);

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);  
            timer.Interval = 310000; //number in milisecinds  
            timer.Enabled = true;  
        }  
        protected override void OnStop() {  
            WriteToFile("Service is stopped at " + DateTime.Now);  
        }  
        private async void OnElapsedTime(object source, ElapsedEventArgs e) {  
            WriteToFile("Service is recall at " + DateTime.Now);

            await client.DeleteAsync("http://localhost:5000/api/newsfeed/");
            await client.PostAsync("http://localhost:5000/api/newsfeed/", null);
            await client.GetStringAsync("http://localhost:5000/api/newsfeed/");


        }  
        public void WriteToFile(string Message) {  
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";  
            if (!Directory.Exists(path)) {  
                Directory.CreateDirectory(path);  
            }  
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";  
            if (!File.Exists(filepath)) {  
                // Create a file to write to.   
                using(StreamWriter sw = File.CreateText(filepath)) {  
                    sw.WriteLine(Message);  
                }  
            } else {  
                using(StreamWriter sw = File.AppendText(filepath)) {  
                    sw.WriteLine(Message);  
                }  
            }  
        }  
    }  
}