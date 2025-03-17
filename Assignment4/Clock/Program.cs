using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Clock
{
    internal class Program
    {
        public class TickArgs
        {
            public int count;
        }
        public delegate void TickHandler(object sender,TickArgs args);
        public delegate void AlarmHandler(object sender);


        public class Clock
        {
            public int count;
            public bool alarmSwitch;
            public int alarmTime;
            public event TickHandler OnTick;
            public event AlarmHandler OnAlarm;
            public Clock()
            {
                count = 0;
                alarmSwitch = false;
                alarmTime = 0;
            }
            public void Reset()
            {
                count = 0;
                alarmSwitch = false;
                alarmTime = 0;
            }
            public void SetAlarm(int t)
            {
                alarmSwitch = true;
                alarmTime = t;
            }
            public void Tick()
            {
                count++;
                TickArgs args = new TickArgs()
                {
                    count = this.count
                };
                OnTick(this, args);
                if(alarmSwitch && count == alarmTime)
                {
                    OnAlarm(this);
                }
            }
        }
        static void Clock_OnTick(object sender,TickArgs args)
        {
            Console.WriteLine("Tick: " + args.count);
        }
        static void Clock_OnAlarm(object sender)
        {
            Console.WriteLine("Alarm!!!");
        }

        private static Clock clock;
        private static void SetClock()
        {
            clock = new Clock();
            clock.SetAlarm(3);
            clock.OnTick += new TickHandler(Clock_OnTick);
            clock.OnAlarm += new AlarmHandler(Clock_OnAlarm);
        }
        static void Main(string[] args)
        {
            SetClock();
            for(int i=0;i<5;i++) clock.Tick();
        }
    }
}
