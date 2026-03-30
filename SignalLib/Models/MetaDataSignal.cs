namespace SignalLib.Models
{
    using SQLite;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Класс для таблицы MetaDataSignal.
    /// </summary>

    [Table("metadata_signal") ]
    public class MetaDataSignal
    {
        /// <summary>
        /// Первичный ключ MetaDataSignal.
        /// </summary>
        [PrimaryKey, Column("primary_key")]
        public Guid PrimaryKey { get; set; }

        /// <summary>
        /// Параметры сигнала.
        /// </summary>
        [Column("parameters")]
        public string? Parametes { get; set; }

        /// <summary>
        /// Тип сигнала.
        /// </summary>
        [Column("signal_type")]
        public tSignalType Type { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        [Column("create_time")]
        public DateTime createTime { get; set; }
        
        /// <summary>
        /// Связанные с сигналом точки сигнала. 
        /// </summary>
        [Ignore]
        public List<PointSignal> Points { get; set; } = new List<PointSignal>();

        public ObservableCollection<SignalRecord> ConvertSignalToSignalRecord(List<MetaDataSignal> signalMetadata)
        {
            var signalsRecord = new ObservableCollection<SignalRecord>();

            foreach (var signal in signalMetadata)
            {
                var signalRecord = new SignalRecord()
                {
                    Id = signal.PrimaryKey,
                    GenerationDate = signal.createTime,
                    Parameters = signal.Parametes,
                    SignalType = Enum.GetName(signal.Type),
                };

                signalsRecord.Add(signalRecord);
            }

            return signalsRecord;
        }
    }
}
