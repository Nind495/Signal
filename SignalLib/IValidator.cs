namespace SignalLib
{
    using SignalLib.Models;

    /// <summary>
    /// Интерфейс валидации.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Проверяет на корректность данные параметров.
        /// </summary>
        /// <param name="parameters">Параметры сигнала.</param>
       public void Validate(Parameters parameters);
    }
}
