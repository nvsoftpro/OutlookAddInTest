using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuredMail.Logger
{
    public enum CounterType
    {
        Read,
        Write,
        Sort,
        Search,
        Common
    }

    public enum CounterMeasureType
    {
        Sec,
        MilliSec,
        Ticks
    }

    public class Counter : IDisposable
    {
        private ILogger _logger;
        private Stopwatch _stopWatch;
        private CounterType _type;
        private CounterMeasureType _measure;
        private string _description;

        
        public Counter(CounterType counterType=CounterType.Common, string description = null, CounterMeasureType measureType = CounterMeasureType.MilliSec)
        {
            Init(counterType, description, measureType);
        }
        public void Dispose()
        {
            try
            {
                if (_stopWatch == null)
                {
                    return;
                }
                _stopWatch.Stop();
                var elapsedTime = String.Empty;
                switch (_measure)
                {
                    case CounterMeasureType.Sec:
                        elapsedTime = $"{_stopWatch.Elapsed.Seconds} sec.";
                        break;
                    case CounterMeasureType.MilliSec:
                        elapsedTime = $"{_stopWatch.ElapsedMilliseconds} millisec.";
                        break;
                    case CounterMeasureType.Ticks:
                        elapsedTime = $"{_stopWatch.ElapsedTicks} ticks.";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _logger.Message($"{_type}[{_description}]:{elapsedTime}");
            }
            catch (Exception)
            {

            }
        }

        [Conditional("COUNTER")]
        private void Init(CounterType counterType, string description, CounterMeasureType measureType)
        {
            _stopWatch = Stopwatch.StartNew();
            _type = counterType;
            _description = description;
            _measure = measureType;
            _stopWatch.Start();
        }
    }
}
