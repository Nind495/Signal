

namespace SignalLib.Models
{
    using SQLite;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Класс для таблицы с историей сигнала.
    /// </summary>
    public class History
    {
        /// <summary>
        /// Первичный ключ истории.
        /// </summary>
        [PrimaryKey, Column("primary_key")]
        public Guid PrimaryKey {  get; set; }

        /// <summary>
        /// Id сигнала.
        /// </summary>
        [Column("signal_id")]
        public Guid Signal { get; set; }

        /// <summary>
        /// Время генерации.
        /// </summary>
        [Column("generate_time")]
        public DateTime Generate_time { get; set; }

        /// <summary>
        /// Заметки по сигналу.
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        [Ignore]
        /// <summary>
        /// Список связанных сигналов с историей.
        /// </summary>
        public List<MetaDataSignal> metaDataSignal { get; set; } = new List<MetaDataSignal>();

        public virtual ObservableCollection<History> Convert()
        {
            return new ObservableCollection<History>();
        }
    }
}
