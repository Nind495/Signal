namespace SignalLib
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Тип сигнала.
    /// </summary>
    public enum tSignalType
    {
        [Display(Name = "Синус")]
        [Description("Синус")]
        /// <summary>
        /// синусоидный сигнал.
        /// </summary>
        Sine,
        [Display(Name = "Меандр")]
        /// <summary>
        /// Меандр (прямоугольный) сигнал
        /// </summary>
        Square,

        [Display(Name = "Треугольный")]
        /// <summary>
        /// Треугольный сигнал.
        /// </summary>
        Triangle,

        [Display(Name = "Пилообразный")]
        /// <summary>
        /// Пилообразный сигнал
        /// </summary>
        Sawtooth
    }
}
