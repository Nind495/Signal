

namespace SignalLib
{
    using SignalLib.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Конвектор моделей.
    /// </summary>
    public interface IConvector
    {
        /// <summary>
        /// Конвертировать metadataSignal в SignalReport.
        /// </summary>
        /// <param name="signalMetadata"></param>
        /// <returns></returns>
        public ObservableCollection<SignalRecord> ConvertSignalToSignalRecord(List<MetaDataSignal> signalMetadata);
    }
}
