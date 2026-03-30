using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalLib.Models
{
    public class Parameters
    {
        /// <summary>
        /// Амплитуда.
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// Частота.
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// Фаза.
        /// </summary>
        public double Phase {  get; set; }

        /// <summary>
        /// Количество точек сигнала, которые следует сгенерировать
        /// </summary>
        public int NumPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double StartTime { get; set; }

        public double EndTime { get; set; }
    }
}
 