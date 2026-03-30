namespace SignalLib
{
    using SignalLib.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Класс постраничного вывода SignalMetadata.
    /// </summary>
    public class PaginationSignalMetadata : IPaginationSignalMetadata
    {
        /// <inheritdoc/>
        private IDataService _dataService;

        /// <inheritdoc/>
        public int PageSize { get; } = 10;

        /// <inheritdoc/>
        public int CurrentPage { get; set; }

        /// <inheritdoc/>
        public int TotalPages { get; set; }
        public PaginationSignalMetadata (IDataService dataService, int totalPages)
        {
            _dataService = dataService;
            TotalPages = totalPages;
            CurrentPage = 1;
        }

        /// <inheritdoc/>
        public async Task InitializeAsync()
        {
            TotalPages = await GetTotalCount();
        }

        /// <inheritdoc/>
        public Task<List<MetaDataSignal>> GetPageMetadataSignal(int pageNumber)
        {
            return _dataService.GetSignals(pageNumber, PageSize);
        }

        /// <inheritdoc/>
        public Task<int> GetTotalCount()
        {
            return _dataService.GetMetadataSignalCount();
        }

        /// <inheritdoc/>
        public async Task<List<MetaDataSignal>> LoadPageAsync()
        {
            int skip = (CurrentPage - 1) * PageSize;
            return await _dataService.GetSignals(skip, PageSize);
        }

        /// <inheritdoc/>
        public void Next()
        {
            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }
           else
            {
                CurrentPage++;
            }
        }

        /// <inheritdoc/>
        public void Back()
        {
            if (CurrentPage <= 1)
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage--;
            }
        }
    }
}
