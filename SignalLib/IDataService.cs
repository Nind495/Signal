namespace SignalLib
{
    using SignalLib.Models;

    /// <summary>
    /// Интерфейс операций с бд
    /// </summary>
    public interface IDataService
    {

        /// <summary>
        /// Вернуть сигнал по primarykey.
        /// </summary>
        /// <param name="primaryKey">primarykey сигнала.</param>
        /// <returns></returns>
        public Task<MetaDataSignal> GetSignalAsync(string primaryKey);
        /// <summary>
        /// Сохранение сигнала.
        /// </summary>
        /// <param name="signal">Сигнал.</param>
        /// <returns></returns>
        public Task SaveSignal(MetaDataSignal signal);

        /// <summary>
        /// Сохранение точек сигнала.
        /// </summary>
        /// <param name="signal">Сигнал</param>
        /// <param name="time">Время сигнала.</param>
        /// <param name="value">Значение сигнала.</param>
        /// <returns></returns>
        public Task SavePoints(MetaDataSignal signal, double[] time, double[] value);

        /// <summary>
        /// Сохранение истории сигнала.
        /// </summary>
        /// <param name="history">История сигнала.</param>
        /// <returns></returns>
        public Task SaveHistory(History history);

        /// <summary>
        /// Сохранение истории сигнала.
        /// </summary>
        /// <param name="signal">сигнал</param>
        /// <param name="generateTime">Время генерации.</param>
        /// <param name="Description">Описание.</param>
        /// <returns></returns>
        public Task SaveHistory(MetaDataSignal signal, DateTime generateTime, string Description);

        /// <summary>
        /// Вернуть колличество записей в таблице MetadataSignal.
        /// </summary>
        /// <returns></returns>
        public Task<int> GetMetadataSignalCount();

        /// <summary>
        /// Вернуть список сигналов с пагинацией.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<MetaDataSignal>> GetSignals(int pageNumber, int pageSize);
    }
}
