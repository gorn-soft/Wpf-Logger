using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Command;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Application = System.Windows.Forms.Application;
using WpfApp15.ViewModel;
using MessageBox = System.Windows.MessageBox;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Cursor = System.Windows.Input.Cursor;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Diagnostics;
using System.Windows.Input;
using LiveCharts;
using System.Windows.Threading;
using System.Threading;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Management;
using CpuGpuGraph;

namespace TaskManager
{
    public class ViewModel : MainViewModel
    {
        public CpuModel CpuModel { get; } = new CpuModel();
        //private static PerformanceCounter cpuCounter;
        //private static PerformanceCounter ramCounter;

        #region Proc

        //public static string getCurrentCpuUsage()
        //{
        //    int countData = Convert.ToInt32(cpuCounter.NextValue());
        //    return countData.ToString() + "%";
        //}

        //public static string getCurrentProcessQty()
        //{
        //    Process[] processList = Process.GetProcesses();
        //    return processList.Length.ToString();

        //}

        //public static string getAvailableRAM()
        //{
        //    var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
        //    int percent = 0;
        //    var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
        //        FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
        //        TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
        //    }).FirstOrDefault();

        //    if (memoryValues != null)
        //    {
        //        percent = Convert.ToInt32(((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100);
        //    }

        //    return percent.ToString() + "%";

        //}
        void LoadPriorites()
        {
            var el = Enum.GetValues(typeof(ProcessPriorityClass)).Cast<ProcessPriorityClass>();
            foreach (var el2 in el)
            {
                Priorites.Add(el2);
            }
            SelectedPriorites = Priorites[0];
        }
        private ProcessListItem _selectedProcess;

        internal void ChangePriority()
        {
            SelectedProcess.ChangePriority(SelectedPriorites);
        }

        public ProcessListItem SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged("SelectedProcess");
            }
        }
        private ProcessPriorityClass _selectedPriorites;
        public ProcessPriorityClass SelectedPriorites
        {
            get => _selectedPriorites;
            set
            {
                _selectedPriorites = value;
                OnPropertyChanged(nameof(SelectedPriorites));
            }
        }

        public ObservableCollection<ProcessListItem> Processes { get; } = new ObservableCollection<ProcessListItem>();
        public ObservableCollection<ProcessPriorityClass> Priorites { get; set; } = new ObservableCollection<ProcessPriorityClass>();



        //public void ChangePriority(ProcessPriorityClass priority)
        //{
        //    SelectedProcess.PriorityClass = priority;
        //}

        public void KillSelectedProcess()
        {
            try
            {
                SelectedProcess.Kill();
            }
            catch (Exception er)
            {
                System.Windows.MessageBox.Show(er.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private RelayCommand killCommand;
        public RelayCommand KillCommand
        {
            get
            {
                return killCommand ??
                    (killCommand = new RelayCommand(obj =>
                    {

                        KillSelectedProcess();
                    }));
            }
        }
        private RelayCommand changeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                return changeCommand ??
                    (changeCommand = new RelayCommand(obj =>
                    {
                        ChangePriority();
                    }));
            }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ??
                    (refreshCommand = new RelayCommand(obj =>
                    {
                        UpdateProcessesFilter();
                    }));
            }
        }

        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                    (startCommand = new RelayCommand(obj =>
                    {
                        StartProcess();
                    }));
            }
        }
        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                    (filterCommand = new RelayCommand(obj =>
                    {
                        UpdateProcessesFilter();
                    }));
            }
        }
        private RelayCommand checkedCommand;
        public RelayCommand CheckedCommand
        {
            get
            {
                return checkedCommand ??
                    (checkedCommand = new RelayCommand(obj =>
                    {
                        if (SelectedProcess.KeepAlive == false)
                        {
                            SelectedProcess.KeepAlive = true;
                        }
                        else
                        {
                            SelectedProcess.KeepAlive = false;
                        }
                    }));
            }
        }
        private string searchString;
        public string SearchString
        {
            get { return searchString; }
            set
            {
                if (searchString != value)
                {
                    searchString = value;
                    OnPropertyChanged(nameof(SearchString));
                }
            }
        }
        private void UpdateProcessesFilter(object sender = null, EventArgs e = null)
        {
            var currentIds = Processes.Select(p => p.Id).ToList();

            foreach (var p in Process.GetProcesses())
            {
                if (searchString.Replace(" ", "") == "" || searchString.ToLower() == "all" )
                {
                    if (!currentIds.Remove(p.Id))
                    {
                        Processes.Add(new ProcessListItem(p));
                    }
                }
                else if (p.ProcessName.Contains(searchString))
                {
                    if (!currentIds.Remove(p.Id))
                    {
                        Processes.Add(new ProcessListItem(p));
                    }
                }
            }

            foreach (var id in currentIds)
            {
                var process = Processes.First(p => p.Id == id);
                if (process.KeepAlive)
                {
                    Process.Start(process.ProcessName, process.Arguments);
                }
                Processes.Remove(process);
            }

        }

        async void ShowStaticsAsync(object sender = null, EventArgs e = null)
        {
            //UIHelper.FindChild<TextBlock>(System.Windows.Application.Current.MainWindow, "proctxt").Text = await Task.Run(() =>
            //{
            //    return getCurrentProcessQty();
            //});
            //UIHelper.FindChild<TextBlock>(System.Windows.Application.Current.MainWindow, "cptxt").Text = await Task.Run(() =>
            //{
            //    return getCurrentCpuUsage();
            //});
            //UIHelper.FindChild<TextBlock>(System.Windows.Application.Current.MainWindow, "memorytxt").Text = await Task.Run(() =>
            //{
            //    return getAvailableRAM();
            //});
        }

        public static string getAvailableRAM()
        {

            var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            int percent = 0;
            var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault();

            if (memoryValues != null)
            {
                percent = Convert.ToInt32(((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100);
            }

            return percent.ToString() + "%";

        }
        private void StartProcess()
        {
            string _file;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _file = openFileDialog1.FileName;
                try
                {
                    Process.Start(_file);
                }
                catch (IOException)
                {
                }
            }
        }
        #endregion

        public SeriesCollection LastHourSeries { get; set; }
        private double _lastLecture;
        public static double _trend;
        public double LastLecture
        {
            get { return _lastLecture; }
            set
            {
                _lastLecture = value;
                OnPropertyChanged("LastLecture");
            }
        }

        public static string pingValue { get; set; } = "n/a";
        public string PingValue
        {
            get { return pingValue; }
            set
            {
                pingValue = value;
                OnPropertyChanged(nameof(PingValue));
            }
        }


        private void SetLecture()
        {
            var target = ((ChartValues<ObservableValue>)LastHourSeries[0].Values).Last().Value;
            var step = (target - _lastLecture) / 4;
            Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 4; i++)
                {
                    Thread.Sleep(100);
                    LastLecture += step;
                }
                LastLecture = target;
            });
        }
        private void StartCountEnterKey()
        {
            LastHourSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(3),
                        new ObservableValue(5),
                        new ObservableValue(6),
                        new ObservableValue(7),
                        new ObservableValue(3),
                        new ObservableValue(4),
                        new ObservableValue(2),
                        new ObservableValue(5),
                        new ObservableValue(8),
                        new ObservableValue(3),
                        new ObservableValue(5),
                        new ObservableValue(6),
                        new ObservableValue(7),
                        new ObservableValue(3),
                        new ObservableValue(4),
                        new ObservableValue(2),
                        new ObservableValue(5),
                        new ObservableValue(8)
                    }
                }
            };
            _trend = 0;
            Task.Factory.StartNew(() =>
            {
                Action action = delegate
                {
                    LastHourSeries[0].Values.Add(new ObservableValue(_trend));
                    LastHourSeries[0].Values.RemoveAt(0);
                    SetLecture();
                };
                while (true)
                {
                    _trend = 0;
                    Thread.Sleep(1000);
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, action);
                }
            });
        }
        public ViewModel(Window window):base(window)
        {
            searchString = "";
            StartCountEnterKey();
            Cursor cursor =  new Cursor($"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}\\ViewModel\\Images\\Cursors\\Cursor.ani"); 
            window.Cursor = cursor;

            //cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            //timer.Tick += ShowStaticsAsync;
            Task.Factory.StartNew( () =>
            {
                Action action = delegate
                {
                    UpdateProcessesFilter();
                };
                while (true)
                {
                     Task.Delay(10);
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, action);
                }
            });
            LoadPriorites();
        }


        private RelayCommand changeTabControl;
        public RelayCommand ChangeTabControlStat
        {
            get
            {
                return changeTabControl ??
                    (changeTabControl = new RelayCommand(obj =>
                    {
                        try
                        {
                            UIHelper.FindChild<System.Windows.Controls.TabControl>(System.Windows.Application.Current.MainWindow, "tabControl1").SelectedIndex = 1;



                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("Eror!", er.Message, MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }

                    }));
            }
        }
        private RelayCommand changeTabControlSettings;
        public RelayCommand ChangeTabControlSettings
        {
            get
            {
                return changeTabControlSettings ??
                    (changeTabControlSettings = new RelayCommand(obj =>
                    {
                        try
                        {
                            UIHelper.FindChild<System.Windows.Controls.TabControl>(System.Windows.Application.Current.MainWindow, "tabControl1").SelectedIndex = 3;



                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("Eror!", er.Message, MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }

                    }));
            }
        }

        private RelayCommand changeTabControlOtchet;
        public RelayCommand ChangeTabControlOtchet
        {
            get
            {
                return changeTabControlOtchet ??
                    (changeTabControlOtchet = new RelayCommand(obj =>
                    {
                        try
                        {
                            UIHelper.FindChild<System.Windows.Controls.TabControl>(System.Windows.Application.Current.MainWindow, "tabControl1").SelectedIndex = 2;



                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("Eror!", er.Message, MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }

                    }));
            }
        }

        private RelayCommand openInst;
        public RelayCommand OpenInst
        {
            get
            {
                return openInst ??
                    (openInst = new RelayCommand(obj =>
                    {
                        StartProcess("https://www.instagram.com/pr_m.a.x/");
                    }));
            }
        }
        private RelayCommand openGit;
        public RelayCommand OpenGit
        {
            get
            {
                return openGit ??
                    (openGit = new RelayCommand(obj =>
                    {
                        StartProcess("https://github.com/MaxymGorn");
                    }));
            }
        }
        private async void StartProcess(string text)
        {
            await Task.Run(() => Process.Start(text));
        }
        private RelayCommand openGmail;
        public RelayCommand OpenGmail
        {
            get
            {
                return openGmail ??
                    (openGmail = new RelayCommand(obj =>
                    {
                        StartProcess("https://mail.google.com/mail/?view=cm&fs=1&tf=1&to=maximus56132@gmail.com");
                    }));
            }
        }

        public static bool writefile;
        public bool Writefile
        {
            get
            {
                return writefile;
            }
            set
            {
                writefile = value;
                OnPropertyChanged(nameof(Writefile));

            }
        }

        public static bool writedirectoryscreendekstop;
        public bool Writedirectoryscreendekstop
        {
            get
            {
                return writedirectoryscreendekstop;
            }
            set
            {
                writedirectoryscreendekstop = value;
                OnPropertyChanged(nameof(Writedirectoryscreendekstop));

            }
        }

        private RelayCommand startlogginng;
        public RelayCommand Startlogginng
        {
            get
            {
                return startlogginng ??
                    (startlogginng = new RelayCommand(obj =>
                    {
                        if (Writefile == false)
                        {
                            Writefile = true;
                            UpdateEventLog("Start KeyLogger: ", $"Time: {DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
                            Task.Run(() => ScreenShot.sendNotificationKeyLoggerDekstop("Start key log to file!"));
                        }
                        else
                        {
                            Writefile = false;
                            UpdateEventLog("Stop KeyLogger: ", $"Time: {DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
                        }
                        try
                        {
                            object text=UIHelper.FindChild<System.Windows.Controls.TextBox>(System.Windows.Application.Current.MainWindow, "outputfile").Text;
                            if((text as string).Length == 0)
                            {
                                throw new IOException("Please choose a file correct!");
                            }
                            
                            Task.Run(()=>LoggerKeys.StartLogging(text as string, this));
                            Task.Delay(1);

                        }
                        catch (IOException er)
                        {
                            MessageBox.Show("Eror!", er.Message, MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                        catch(Exception er)
                        {
                            MessageBox.Show("Eror!", er.Message, MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                       
                    }));
            }
        }

        private RelayCommand changeDictionary;
        public RelayCommand ChangeDictionaries
        {
            get
            {
                return changeDictionary ??
                    (changeDictionary = new RelayCommand(obj =>
                    {
                        ChangeDictionary();
                    }));
            }
        }

        void ChangeDictionary()
        {
            if (_blue)
                System.Windows.Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("Blue.xaml", UriKind.Relative) };
            else
                System.Windows.Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("Black.xaml", UriKind.Relative) };
            _blue = !_blue;
        }

        private RelayCommand startlogginngsCREEN;
        public RelayCommand StartlogginngsCREEN
        {
            get
            {
                return startlogginngsCREEN ??
                    (startlogginngsCREEN = new RelayCommand(obj =>
                    {
                        if (Writedirectoryscreendekstop == false)
                        {
                            Writedirectoryscreendekstop = true;
                            Task t= Task.Run(() => ScreenShot.sendNotificationKeyLoggerDekstop("Start Logger Dekstop Screen to directory...!"));
                            Task.WaitAll(t);
                            UpdateEventLog("Start Logger Dekstop Screen: ", $"Time: {DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
                            captureScreen(false, UIHelper.FindChild<System.Windows.Controls.TextBox>(System.Windows.Application.Current.MainWindow, "outputfilescreen").Text, "files", "1", ImageFormat.Png);
                        }
                        else
                        {
                            Writedirectoryscreendekstop = false;
                            UpdateEventLog("Stop Logger Dekstop Screen: ", $"Time: {DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
                        }
                        
                    }));
            }
        }
        private void captureScreen(bool cursor, string folder, string pattern, string number, ImageFormat format)
        {
            var screenshot = new ScreenShot(cursor, folder, pattern, number, format, true, this);
             screenshot.CaptureAndSave();
        }

        private RelayCommand changeDirectory;
        public RelayCommand ChangeDirectory
        {
            get
            {
                return changeDirectory ??
                    (changeDirectory = new RelayCommand(obj =>
                    {
                        string _file;
                        OpenFileDialog openFileDialog1 = new OpenFileDialog() { InitialDirectory= $" ../../{Application.StartupPath }"};
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            _file = openFileDialog1.FileName;
                            try
                            {
                                UIHelper.FindChild<System.Windows.Controls.TextBox>(System.Windows.Application.Current.MainWindow, "outputfile").Text = openFileDialog1.FileName;
                            }
                            catch (IOException)
                            {
                            }
                        }
                    }));
            }
        }

        private RelayCommand changeDirectoryDedstopScreen;
        public RelayCommand ChangeDirectoryDekstopScreen
        {
            get
            {
                return changeDirectoryDedstopScreen ??
                    (changeDirectoryDedstopScreen = new RelayCommand(obj =>
                    {
                        using (var fldrDlg = new FolderBrowserDialog() { SelectedPath = Application.StartupPath + @"\Autosave" })
                        {
                            string _file;
                            if (fldrDlg.ShowDialog() == DialogResult.OK)
                            {
                                _file = fldrDlg.SelectedPath;
                                try
                                {
 
                                    UIHelper.FindChild<System.Windows.Controls.TextBox>(System.Windows.Application.Current.MainWindow, "outputfilescreen").Text = _file;
                                    //using (System.Drawing.Image img = ScreenShot.CaptureFullscreen()) {
                                    //    img.Save(_file+ count_screens_img);
                                    //}
                                }
                                catch (IOException)
                                {
                                }
                            }
                        }
                    }));
            }
            
        }
        
        private RelayCommand changeDirectoryWebScreen;
        public RelayCommand ChangeDirectoryWebScreen
        {
            get
            {
                return changeDirectoryWebScreen ??
                    (changeDirectoryWebScreen = new RelayCommand(obj =>
                    {
                        using (var fldrDlg = new FolderBrowserDialog() { SelectedPath = Application.StartupPath })
                        {
                            string _file;
                            if (fldrDlg.ShowDialog() == DialogResult.OK)
                            {
                                _file = fldrDlg.SelectedPath;
                                try
                                {
                                    UIHelper.FindChild<System.Windows.Controls.TextBox>(System.Windows.Application.Current.MainWindow, "outputfilewebcam").Text = _file;
                                }
                                catch (IOException)
                                {
                                }
                            }
                        }

                    }));
            }
        } 


    }
  
}
