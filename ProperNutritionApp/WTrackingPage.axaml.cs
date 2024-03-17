﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MySql.Data.MySqlClient;

namespace ProperNutritionApp
{
    public partial class WTrackingPage : UserControl
    {
        private Database _db = new Database();
        private ObservableCollection<WeightTracking> _weightTrackings = new ObservableCollection<WeightTracking>();
        private string sql = "select * from weight_tracking";

        public WTrackingPage()
        {
            InitializeComponent();
            ShowTable(sql);
            PlotWeightTrackingData();
        }

        public void ShowTable(string sql)
        {
            _db.OpenConnection();
            MySqlCommand command = new MySqlCommand(sql, _db.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var currentWeight = new WeightTracking()
                {
                    WeightId = reader.GetInt32("weight_id"),
                    User = reader.GetInt32("user"),
                    Date = reader.GetDateTime("date"),
                    Weight = reader.GetDouble("weight")
                };
                _weightTrackings.Add(currentWeight);
            }
            _db.CloseConnection();
        }
        
        private void PlotWeightTrackingData()
        {
            var plotModel = new PlotModel { Title = "График изменения веса" };

            var lineSeries = new LineSeries { Title = "Изменение веса", MarkerType = MarkerType.Circle };

            // Добавляем точки данных о весе в серию данных
            foreach (var weightTracking in _weightTrackings)
            {
                // Создаем точку с координатами (X - дата, Y - вес)
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(weightTracking.Date.Ticks), weightTracking.Weight));
            }

            plotModel.Series.Add(lineSeries);

            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd/MM/yyyy",
                Title = "Дата"
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Вес"
            });

            PlotView.Model = plotModel;
        }
        
    }
}