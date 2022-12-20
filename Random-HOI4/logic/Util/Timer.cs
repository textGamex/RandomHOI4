using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Random_HOI4.Logic.Util
{
    /// <summary>
    /// 计时器
    /// </summary>
    internal class Timer
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop() 
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        /// <summary>
        /// 已用毫秒
        /// </summary>
        public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;
        /// <summary>
        /// 已用秒
        /// </summary>
        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds;
        /// <summary>
        /// 已用分钟
        /// </summary>
        public double ElapsedMinutes => _stopwatch.Elapsed.TotalMinutes;
    }
}
