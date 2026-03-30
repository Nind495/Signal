namespace SignalLib.Models
{
    using SQLite;

    /// <summary>
    /// Таблица с точками сигнала.
    /// </summary>
    [Table("point_signal")]
    public class PointSignal
    {
        [PrimaryKey, Column("primary_key")]
        public Guid PrimaryKey { get; set; }

        /// <summary>
        /// id сигнала
        /// </summary>
        [Column("signal_id"), ]
        public Guid Signal { get; set; }

        /// <summary>
        /// Промежуток времени(значение).
        /// </summary>
        [Column("time")]
        public double TimeValue { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        [Column("value")]
        public double Value { get; set; }
    }
}
