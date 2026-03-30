using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalGeneratorWinForms
{
    public partial class MainForm : Form
    {
        CancellationTokenSource cts;

        public MainForm()
        {
            InitializeComponent();

            // Настраиваем ComboBox
            comboBoxSignalType.Items.AddRange(Enum.GetNames(typeof(SignalType)));
            comboBoxSignalType.SelectedIndex = 0;

            // Настраиваем ProgressBar
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // Ввод данных
            double amplitude, frequency, phase;
            int numPoints;
            double startTime, endTime;

            if (!double.TryParse(txtAmplitude.Text, out amplitude) ||
                !double.TryParse(txtFrequency.Text, out frequency) ||
                !double.TryParse(txtPhase.Text, out phase) ||
                !int.TryParse(txtPoints.Text, out numPoints) ||
                !double.TryParse(txtStartTime.Text, out startTime) ||
                !double.TryParse(txtEndTime.Text, out endTime))
            {
                MessageBox.Show("Проверьте правильность ввода");
                return;
            }

            // Валидация
            var errors = ValidateParams(amplitude, frequency, phase, numPoints, endTime - startTime);
            if (errors.Length > 0)
            {
                MessageBox.Show(string.Join("\n", errors));
                return;
            }

            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            progressBar.Value = 0;

            cts = new CancellationTokenSource();

            try
            {
                var signal = await GenerateSignalAsync(
                    (SignalType)Enum.Parse(typeof(SignalType), comboBoxSignalType.SelectedItem.ToString()),
                    amplitude, frequency, phase, numPoints, startTime, endTime,
                    new Progress<double>(p => progressBar.Value = (int)(p * 100)),
                    cts.Token);

                MessageBox.Show("Генерация завершена!");
                // Тут можно сохранить или обработать сигнал
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Генерация отменена");
            }
            finally
            {
                btnStart.Enabled = true;
                btnCancel.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private string[] ValidateParams(double amplitude, double frequency, double phase, int numPoints, double duration)
        {
            var errors = new System.Collections.Generic.List<string>();
            if (amplitude <= 0)
                errors.Add("Амплитуда должна быть положительной");
            if (frequency <= 0)
                errors.Add("Частота должна быть положительной");
            if (phase < 0 || phase > 2 * Math.PI)
                errors.Add("Фаза должна быть в диапазоне [0, 2π]");
            if (numPoints <= 0)
                errors.Add("Количество точек должно быть положительным");
            if (duration <= 0)
                errors.Add("Длина интервала должна быть положительной");
            return errors.ToArray();
        }

        private async Task<double[]> GenerateSignalAsync(
            SignalType type,
            double amplitude,
            double frequency,
            double phase,
            int numPoints,
            double startTime,
            double endTime,
            IProgress<double> progress,
            CancellationToken token)
        {
            int totalSteps = 10;
            for (int step = 1; step <= totalSteps; step++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(200);
                progress.Report((double)step / totalSteps);
            }

            var result = new double[numPoints];
            double duration = endTime - startTime;

            for (int i = 0; i < numPoints; i++)
            {
                token.ThrowIfCancellationRequested();
                double t = startTime + i * duration / (numPoints - 1);
                result[i] = GenerateSample(type, amplitude, frequency, phase, t);
            }

            return result;
        }

        private double GenerateSample(SignalType type, double amplitude, double frequency, double phase, double t)
        {
            switch (type)
            {
                case SignalType.Sine:
                    return amplitude * Math.Sin(2 * Math.PI * frequency * t + phase);
                case SignalType.Square:
                    return amplitude * Math.Sign(Math.Sin(2 * Math.PI * frequency * t + phase));
                case SignalType.Triangle:
                    return amplitude * (2 * Math.Abs(2 * (frequency * t - Math.Floor(0.5 + frequency * t))) - 1);
                case SignalType.Sawtooth:
                    return amplitude * (2 * (frequency * t - Math.Floor(frequency * t + 0.5)));
                default:
                    throw new ArgumentException("Некорректный тип сигнала");
            }
        }
    }

    public enum SignalType
    {
        Sine,
        Square,
        Triangle,
        Sawtooth
    }
}