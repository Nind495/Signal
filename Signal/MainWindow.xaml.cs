
namespace WpfApp1
{
    using Microsoft.Extensions.Logging;
    using SignalLib;
    using SignalLib.Models;
    using System.Collections.ObjectModel;
    using System.Text.Json;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using MetaDataSignal = SignalLib.Models.MetaDataSignal;

    public partial class MainWindow : Window
    {
       
        private IDataService _dataService;
        private IConvector _convector;
        private ISignalGenerator _signalGenerator;
        private CancellationTokenSource cts;
        private List<MetaDataSignal> signals;
        private History history;
        private readonly ILogger<MainWindow> _logger;
        private IPaginationSignalMetadata _paginationSignalMetadata;
        private List<MetaDataSignal> saveSignals;

        public ObservableCollection<SignalRecord> SignalHistory { get; set; } = new ObservableCollection<SignalRecord>();

        public MainWindow()
        {
            InitializeComponent();
            
            //Инициализация бд.
            _dataService = new DataService("BD.db");

            _convector = new Convector();
            //Инициализация сервисов.
            _signalGenerator = new SignalGenerator(new SignalLib.Validator());
            _paginationSignalMetadata = new PaginationSignalMetadata(_dataService, 10);

            signals = new List<MetaDataSignal>();
            saveSignals = new List<MetaDataSignal>();
            history = new History();
            SignalTypeComboBox.ItemsSource = Enum.GetValues(typeof(tSignalType));
            //Асинхронная загрузка бд.
            this.Loaded += loadDataBD;
        }

        public async void loadDataBD(object sender, RoutedEventArgs e)
        {
            try
            {
                _paginationSignalMetadata.CurrentPage = 1;
                await _paginationSignalMetadata.InitializeAsync();
                signals = await _paginationSignalMetadata.LoadPageAsync();
                
                SignalHistory = _convector.ConvertSignalToSignalRecord(signals);
                dgHistory.ItemsSource = SignalHistory;
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            double frequency = 0;
            int numPoints = 0;
            double amplitude = 0;
            double phase = 0;
            int startDate = 0;
            int endDate = 0;

            if (!System.Double.TryParse(FrequencyTextBox.Text, out frequency))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (!System.Double.TryParse(AmplitudeTextBox.Text, out amplitude))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (!System.Int32.TryParse(PointsTextBox.Text, out numPoints))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (!System.Double.TryParse(PhaseTextBox.Text, out phase))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (!Int32.TryParse(DataStartTextBox.Text, out startDate))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (!Int32.TryParse(TimeToTextBox.Text, out endDate))
            {
                throw new Exception("Не удалось привести к числу.");
            }

            if (endDate < startDate)
            {
                throw new Exception("Дата начала должна быть больше даты окончания");
            }

            CancelButton.IsEnabled = true;

            cts = new CancellationTokenSource();
            GenerateButton.IsEnabled = false;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = numPoints;
            ProgressBar.Visibility = Visibility.Visible;

            try
            {
                var progress = new Progress<double>(value =>
                {
                    ProgressBar.Value = value;
                });
                
                tSignalType signalType = (tSignalType)SignalTypeComboBox.SelectedItem;

                Parameters parameters = new Parameters 
                { 
                    Amplitude = amplitude,
                    Frequency =  frequency,
                    EndTime = endDate,
                    NumPoints = numPoints,
                    Phase = phase,
                    StartTime = startDate
                };

                (double[] dataX, double[] times) = await Task.Run(() =>                
                     _signalGenerator.GenerateSignalAsync(signalType, parameters, progress, cts.Token)
                );

                await Task.Run(() => WpfPlot1.Plot.Add.Scatter(times, dataX));
                WpfPlot1.Refresh();

            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show("Генерация отменена");
                _logger.LogWarning(ex.Message);
            }
            finally
            {
                ProgressBar.Visibility = Visibility.Collapsed;
                GenerateButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveButton.IsEnabled = false;
                var saveTasks = saveSignals.Select(async signal =>
                {
                    await _dataService.SaveSignal(signal);
                    double[] times = signal.Points.Select(point => point.TimeValue).ToArray();
                    double[] values = signal.Points.Select(point => point.Value).ToArray();
                    await _dataService.SavePoints(signal, times, values);
                });

                await Task.WhenAll(saveTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally 
            {
                SaveButton.IsEnabled = true;
            }
            
        }

        private async void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            WpfPlot1.Plot.Clear();
            WpfPlot1.Refresh();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void PrevPageBtn_Click(object sender, RoutedEventArgs e)
        {
            _paginationSignalMetadata.Back();
            _paginationSignalMetadata.LoadPageAsync();
            SignalHistory = _convector.ConvertSignalToSignalRecord(signals);
            dgHistory.ItemsSource = SignalHistory;
        }

        private async void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            _paginationSignalMetadata.Next();

            var signals = await _paginationSignalMetadata.LoadPageAsync();

            SignalHistory = _convector.ConvertSignalToSignalRecord(signals);
            dgHistory.ItemsSource = SignalHistory;
        }
    }
}