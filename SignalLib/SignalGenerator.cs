namespace SignalLib
{
    using SignalLib.Models;

    /// <summary>
    ///  Генератора сигнала.
    /// </summary>
    public class SignalGenerator : ISignalGenerator
    {
        private readonly IValidator _validator;

        public SignalGenerator(IValidator validator)
        {
            _validator = validator;
        }
        /// <summary>
        /// Генерация сигнала ассинхронно.
        /// </summary>
        /// <param name="type">Тип сигнала.</param>
        /// <param name="parameters">Параметры сигнала.</param>
        /// <param name="progress">Тулбар процесса генерации.</param>
        /// <param name="token">Токен отмены генерации.</param>
        /// <returns></returns>
        public async Task<(double[] values, double[] times)> GenerateSignalAsync(
            tSignalType type,
            Parameters parameters,
            IProgress<double> progress,
            CancellationToken token)
        {
            //progress.Report(parameters.NumPoints);


            _validator.Validate(parameters);
            double duration = parameters.EndTime - parameters.StartTime;
            double[] times = new double[parameters.NumPoints];
            double[] values = new double[parameters.NumPoints];

            int reportInterval = Math.Max(1, parameters.NumPoints / 1000);
            double dt = (parameters.EndTime - parameters.StartTime) / (parameters.NumPoints - 1);

            for (int i = 0; i < parameters.NumPoints; i++)
            {
                token.ThrowIfCancellationRequested();
                times[i] = parameters.StartTime + dt * i;
                values[i] = GenerateSample(type, parameters.Amplitude, parameters.Frequency, parameters.Phase, times[i]);

                if (i % reportInterval == 0)
                {
                    progress.Report(i);
                }
            }

            return (values, times);
        }

        /// <summary>
        /// Генерирует значение сигнала в заданный момент времени <paramref name="t"/>, основываясь на выбранном типе сигнала и его параметрах.
        /// </summary>
        /// <param name="type">Тип сигнала.</param>
        /// <param name="amplitude">Амплитуда сигнала.</param>
        /// <param name="frequency">Частота сигнала.</param>
        /// <param name="phase">Фазовый сдвиг сигнала.</param>
        /// <param name="t">Момент времени, в который необходимо рассчитать сигнал.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private double GenerateSample(tSignalType type, double amplitude, double frequency, double phase, double t)
        {
            switch (type)
            {
                case tSignalType.Sine:
                    return amplitude * Math.Sin(2 * Math.PI * frequency * t + phase);
                case tSignalType.Square:
                    return amplitude * Math.Sign(Math.Sin(2 * Math.PI * frequency * t + phase));
                case tSignalType.Triangle:
                    return amplitude * (2 * Math.Abs(2 * (frequency * t - Math.Floor(0.5 + frequency * t))) - 1);
                case tSignalType.Sawtooth:
                    return amplitude * (2 * (frequency * t - Math.Floor(frequency * t + 0.5)));
                default: 
                    throw new ArgumentException("Некорректный тип сигнала");
            }
        }
    }
}
