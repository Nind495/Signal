namespace SignalLib
{
    using SignalLib.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Конвектор моделей.
    /// </summary>
    public class Convector : IConvector
    {
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
