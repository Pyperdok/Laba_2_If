using System;

namespace Task_If
{
    public class Time
    {
       public int Hours { get; private set; }
       public int Minutes { get; private set; }

       public Time(int hours, int minutes)
       {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59) 
            { 
                throw new ArgumentException("Введены неверные данные");             
            }

            Hours = hours;
            Minutes = minutes;           
       }

       public static bool operator <(Time a, Time b)
       {
            bool Status = false;
            if (a.Hours < b.Hours)
            {
                Status = true;
            }
            else if (a.Hours == b.Hours && a.Minutes < b.Minutes)
            {
                Status = true;
            }
            return Status;
       }

       public static bool operator >(Time a, Time b)
       {
            bool Status = false;
            if (a.Hours > b.Hours)
            {
                Status = true;
            }
            else if (a.Hours == b.Hours && a.Minutes > b.Minutes)
            {
                Status = true;
            }
            return Status;
       }
    };

   public class Program
   {
        public static bool IsTrainStand(Time TrainArrive, Time TrainLeave, Time Passenger, bool IsLeaveDay)
        {
            bool CrossDay = TrainLeave < TrainArrive;
            bool Result = false;
            if (TrainArrive < Passenger && Passenger < TrainLeave) //Обычный целый день
            {
                Result = true;
            }
            else if(TrainArrive < Passenger && Passenger > TrainLeave && !IsLeaveDay && CrossDay) //Пассажир пришел в день прибытия
            {
                Result = true;
            }
            else if (TrainArrive > Passenger && Passenger < TrainLeave && IsLeaveDay && CrossDay) //Пассажир пришел в день отправления
            {
                Result = true;
            }
            return Result;
        }
   }
}
