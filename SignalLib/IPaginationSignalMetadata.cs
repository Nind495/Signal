namespace SignalLib
{
    using SignalLib.Models;

    /// <summary>
    /// Интерфейс постраничного вывода данных для MetaDataSignal.
    /// </summary>
    public interface IPaginationSignalMetadata
    {
        /// <summary>
        /// Колличество страниц на одном листе.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Асинхронная инициализация данных для постраничного вывода данных.
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync();

        /// <summary>
        /// Метод получения колличества страниц.
        /// </summary>
        /// <returns></returns>
        public Task<int> GetTotalCount();

        /// <summary>
        /// Загрузка данных текущей страницы.
        /// </summary>
        /// <returns></returns>
        public Task<List<MetaDataSignal>> LoadPageAsync();

        /// <summary>
        /// Перейти к следующей странице.
        /// </summary>
        public void Next();

        /// <summary>
        /// Вернуться на предыдушую страницу.
        /// </summary>
        public void Back();
    }
}
