namespace SignalLib
{
    using SignalLib.Models;
    /// <summary>
    /// Класс валидации.
    /// </summary>
    public class Validator : IValidator
    {
        /// <inheritdoc/>
        public void Validate(Parameters parameters)
        {
            // Валидация входных данных
            if (parameters.Amplitude <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters.Amplitude), "Амплитуда должна быть положительной.");
            if (parameters.Frequency <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters.Frequency), "Частота должна быть положительной.");
            if (parameters.NumPoints <= 1)
                throw new ArgumentOutOfRangeException(nameof(parameters.NumPoints), "Количество точек должно быть больше 1.");
            if (parameters.EndTime <= parameters.StartTime )
                throw new ArgumentOutOfRangeException("Время окончания должно быть больше времени начала.");
        }
    }
}
