﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Enigma.D3.MapHack.Markers;

namespace Enigma.D3.MapHack
{
    public partial class Shell : Window, INotifyPropertyChanged
    {
        private static readonly Lazy<Shell> _lazyInstance = new Lazy<Shell>(() => new Shell());

        public static Shell Instance { get { return _lazyInstance.Value; } }

        private bool _isAttached;
        private ShellTraceListener _tracer;

        public MapMarkerOptions Options { get; private set; }
        public bool IsAttached { get { return _isAttached; } set { if (_isAttached != value) { _isAttached = value; Refresh("IsAttached"); } } }

        public Shell()
        {
            Options = MapMarkerOptions.Instance;
            DataContext = this;
            InitializeComponent();
            Trace.Listeners.Add(_tracer = new ShellTraceListener(Dispatcher, Log));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void Refresh(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private class ShellTraceListener : TraceListener
        {
            private readonly Dispatcher _dispatcher;
            private readonly TextBox _log;

            public ShellTraceListener(Dispatcher dispatcher, TextBox log)
            {
                _dispatcher = dispatcher;
                _log = log;
            }

            public override void Write(string message) => WriteLine(message);

            public override void WriteLine(string message) => _dispatcher.BeginInvoke((Action)(() =>
            {
                WriteLineSync(message);
            }));

            internal void WriteLineSync(string message)
            {
                if (_log.Tag == null)
                    return;

                var scroll = _log.GetLastVisibleLineIndex() <= _log.LineCount - 1;
                _log.AppendText(DateTime.Now.ToString("HH:mm:ss.ffffff") + ": " + message + Environment.NewLine);
                if (scroll)
                    _log.ScrollToEnd();
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Content.Equals("Start"))
            {
                Log.Tag = new object();
                Log.Foreground = Brushes.Black;
                button.Content = "Stop";
                Trace.WriteLine("Logging started.");
            }
            else
            {
                _tracer.WriteLineSync("Logging stopped.");
                Log.Tag = null;
                Log.Foreground = Brushes.Gray;
                button.Content = "Start";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Log.Clear();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            Problem.Visibility = Visibility.Collapsed;
            ControlHelper.DisableTrollMode = true;
            Minimap.Instance.ForceReset = true;
            AppTab.IsSelected = true;
        }
    }
}
