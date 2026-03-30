using System.Collections.ObjectModel;

namespace SignalLib.Models
{
    /// <summary>
    /// Класс отображения значений сигнала на форме.
    /// </summary>
    public class SignalRecord
    {
        /// <summary>
        /// Id сигнала.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Время генериации.
        /// </summary>
        public DateTime GenerationDate { get; set; }

        /// <summary>
        /// Тип сигнала.
        /// </summary>
        public required string SignalType { get; set; }

        /// <summary>
        /// Параметры сигнала.
        /// </summary>
        public required string Parameters { get; set; }
    }
}
