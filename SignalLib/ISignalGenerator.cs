namespace SignalLib
{
    using SignalLib.Models;
    using System;
    using System.Threading.Tasks;
    /// <summary>
    /// Интерфейс генератора сигнала.
    /// </summary>
    public interface ISignalGenerator
    {
        /// <summary>
        /// Асинхронно генерирует массив значений сигнала в заданном диапазоне времени, основываясь на указанных параметрах сигнала.
        /// </summary>
        /// <param name="type">Тип сигнала (синусоидальный, квадратный, треугольный или пилообразный).</param>
        /// <param name="parameters">Параметры сигнала.</param>
        /// <param name="progress">Объект для отчёта о прогрессе выполнения задачи.</param>
        /// <param name="token">Токен отмены, позволяющий прервать выполнение метода.</param>
        /// <returns>Массив значений сигнала в заданном диапазоне времени.</returns>
        /// <exception cref="OperationCanceledException">Выбрана отмена выполнения через токен.</exception>
        public Task<(double[] values, double[] times)> GenerateSignalAsync( tSignalType type, Parameters parameters, IProgress<double> progress, CancellationToken token);
    }
}
