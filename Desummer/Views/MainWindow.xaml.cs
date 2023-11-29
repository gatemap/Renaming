﻿using Desummer.Scripts;
using System.Windows;
using System.Windows.Controls;

namespace Desummer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProcessData temperatrueProcessData;

        PlotControl plotControl;
        PlotControl donutControl;

        DataGridControl showOnDataGrid;

        public MainWindow()
        {
            InitializeComponent();

            temperatrueProcessData = new ProcessData();
            plotControl = new PlotControl(temperaturePlot, temperatrueProcessData.TemperatureTotalData());
            donutControl = new PlotControl(temperatureDonut1, temperatureDonut2, temperatureDonut3, temperatrueProcessData.TemperatureTotalData());

            //실행했을때 데이터그리드가 비어있지 않게 전체 데이터에 대한 요약 출력
            showOnDataGrid = new DataGridControl();
            showOnDataGrid.MinMaxAvg(DataGrid_TempData, temperatrueProcessData.TemperatureTotalData());
        }

        private void ShowAthermalFurnace(object sender, RoutedEventArgs e)
        {
            if (plotControl is null) return;

            plotControl.ThermalFurnaceVisible(1, true);
        }

        private void HideAthermalFurnace(object sender, RoutedEventArgs e)
        {
            plotControl.ThermalFurnaceVisible(1, false);
        }

        private void ShowBthermalFurnace(object sender, RoutedEventArgs e)
        {
            if (plotControl is null) return;

            plotControl.ThermalFurnaceVisible(2, true);
        }

        private void HideBthermalFurnace(object sender, RoutedEventArgs e)
        {
            plotControl.ThermalFurnaceVisible(2, false);
        }

        private void ShowCthermalFurnace(object sender, RoutedEventArgs e)
        {
            if (plotControl is null) return;

            plotControl.ThermalFurnaceVisible(3, true);
        }

        private void HideCthermalFurnace(object sender, RoutedEventArgs e)
        {
            plotControl.ThermalFurnaceVisible(3, false);
        }

        private void Button_ShowData_Click(object sender, RoutedEventArgs e)
        {
            //선택된 month 체크
            ComboBoxItem SelectMonth = (ComboBoxItem)ComboBox_SelectMonth.SelectedValue;
            string SelectedMonth = (string) SelectMonth.Content;

            //선택된 week 체크
            ComboBoxItem SelectWeek = (ComboBoxItem)ComboBox_SelectWeek.SelectedValue;
            string SelectedWeek = (string) SelectWeek.Content;

            //ProcessData 클래스의 split 메서드 호출해 해당 week 데이터 가져오기
            ProcessData Month = new ProcessData();
            List<TemperatureData> Select_Month_Week = Month.SplitDataMonthly(SelectedMonth, SelectedWeek);

            //최대, 최소, 평균 출력하기
            showOnDataGrid = new DataGridControl();
            showOnDataGrid.MinMaxAvg(DataGrid_TempData, Select_Month_Week);
        }

        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (plotControl is null || !plotControl.pauseGraph) return;

            plotControl.CrosshairVisible(true);
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (plotControl is null) return;

            plotControl.CrosshairVisible(false);
        }

        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (plotControl is null || !plotControl.pauseGraph) return;

            plotControl.ShowCrosshairData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (plotControl.pauseGraph)
                pauseButton.Content = "재생";
            else
                pauseButton.Content = "일시정지";

            plotControl.PauseGraph();
        }
    }
}