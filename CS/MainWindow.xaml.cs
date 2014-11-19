#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Threading;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using RealTimeUpdateDemo;
using System.Collections;
using System.Windows.Controls.Primitives;
using Syncfusion.Windows.SampleLayout;
using Syncfusion.UI.Xaml.Charts;

namespace RealTimeUpdateDemo
{    
    public partial class MainWindow : SampleLayoutWindow
    {
        DataGenerator view = new DataGenerator();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Start();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 152);
   //         view.LoadData();
            this.Chart.Series[0].ItemsSource = view.DynamicData;
            // this.Chart.Series[1].ItemsSource = view.DynamicData;
            //this.Chart.Series[2].ItemsSource = view.DynamicData;           
        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            view.AddData();                  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            else
                timer.Start();
        }              

    }

    public class DataGenerator
    {
        public int DataCount = 60000;
        private int RateOfData = 84;
        private int NumOfDataSamplesShown = 30000;
        private ObservableCollection<Data> Data;
        private Random randomNumber;
        int myindex = 0;
        public ObservableCollection<Data> DynamicData { get; set; } 

        public DataGenerator()
        {
            randomNumber = new Random();
            DynamicData = new ObservableCollection<Data>();
            Data = new ObservableCollection<Data>();
            Data = GenerateData();
        }
        public void AddData()
        {
            for (int i = 0; i < RateOfData; i++)
            {
                myindex++;
                if (myindex < NumOfDataSamplesShown)
                {
                    this.Data[myindex].Date = DateTime.Now.ToString("ss.fff"); ;
                    DynamicData.Add(this.Data[myindex]);
                }
           //     else if (myindex % 1000 == 0)
           //     {
           //         DynamicData.RemoveAt(0);
           //         DynamicData.Add(new Data(new DateTime(2009, 1, 1), 1006, 1006, 1006));
           //     }
                else if (myindex > NumOfDataSamplesShown)
                {
                    DynamicData.RemoveAt(0);
                    this.Data[(myindex % (this.Data.Count - 1))].Date = DateTime.Now.ToString("ss.fff");
                    DynamicData.Add(this.Data[(myindex % (this.Data.Count - 1))]);
                }
                
            }

        }

        public void LoadData()
        {
            for (int i = 0; i < 10; i++)
            {
                myindex++;
                if (myindex < Data.Count)
                {
                    DynamicData.Add(this.Data[myindex]);
                }
            }

        }      
        public ObservableCollection<Data> GenerateData()
        {
            ObservableCollection<Data> datas = new ObservableCollection<Data>();

            DateTime date = new DateTime(2009, 1, 1);
            double value = 1000;
            double value1 = 1001;
            double value2 = 1002;
            for (int i = 0; i < this.DataCount; i++)
            {
                datas.Add(new Data(date, value, value1, value2));
                //date = date.Add(TimeSpan.FromSeconds(5));

             //  value2  = value1 = value = i%1000;
               
               if ((randomNumber.NextDouble() + value2) < 1004.85)
               {
                   double random = randomNumber.NextDouble();
                   value += random;
                  value1 += random;
                   value2 += random;
               }
               else
               {
                   double random = randomNumber.NextDouble();
                   value -= random;
                   value1 -= random;
                   value2 -= random;
               }
            }

            return datas;
        }
    }

    public class Data
    {
        public Data(DateTime date, double value, double value1, double value2)
        {
            Date = date.ToString("ss.fff");
            Value = value;
            Value1 = value1;
            Value2 = value2;
        }

        public String Date
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }
        public double Value1
        {
            get;
            set;
        }
        public double Value2
        {
            get;
            set;
        }
    }   
}