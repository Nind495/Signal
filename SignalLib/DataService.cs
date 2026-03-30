namespace SignalLib
{
    using SignalLib.Models;
    using SQLite;
    using System.Collections.ObjectModel;

    public class DataService : IDataService
    {
        private SQLiteAsyncConnection _connection;

        public DataService (string connectionString)
        {
            _connection = new SQLiteAsyncConnection(connectionString);
        }

        public async Task<MetaDataSignal> GetSignalAsync(string primaryKey)
        {
            return await _connection.GetAsync<MetaDataSignal>(primaryKey);
        }
        
        public async Task SavePoints(MetaDataSignal signal, double[] time, double[] value)
        {
            await _connection.RunInTransactionAsync(async (transaction) =>
            {
                for (int i = 0; i < time.Length; i++)
                {
                    PointSignal pointSignal = new PointSignal();
                    pointSignal.PrimaryKey = Guid.NewGuid();
                    pointSignal.Signal = signal.PrimaryKey;
                    pointSignal.TimeValue = time[i];
                    pointSignal.Value = value[i];
                    await _connection.InsertAsync(pointSignal);
                }
            });
        }


        public async Task SaveSignal(MetaDataSignal signal)
        {
            if (signal.PrimaryKey == Guid.Empty)
            {
               await _connection.InsertAsync(signal);
            }
            else
            {
                await _connection.InsertAsync(signal);
                
            }
        }

        public async Task SaveHistory(History history)
        {
            await _connection.InsertAsync(history);
        }


        public async Task SaveHistory(MetaDataSignal signal, DateTime generateTime, string Description)
        {
            var history = new History();
            history.Signal = signal.PrimaryKey;
            history.Generate_time = DateTime.Now;
            history.Description = Description;
            await  _connection.InsertAsync(history);
        }

        public async Task<List<MetaDataSignal>> GetSignals(int pageNumber, int pageSize)
        {
            var metadataSignal  = await _connection.Table<MetaDataSignal>()
                .Skip(pageNumber)
                .Take(pageSize)
                .ToListAsync();
            return metadataSignal;
        }

        /// <summary>
        /// Вернуть колличество записей в таблице MetadataSignal.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMetadataSignalCount()
        {
            return await _connection.Table<MetaDataSignal>().CountAsync();
        }
    }
}
