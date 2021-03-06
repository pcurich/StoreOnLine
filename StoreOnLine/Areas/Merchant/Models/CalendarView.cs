﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Models
{
    public class CalendarView
    {
        public string WeekName { get; set; }
        public string DayNumber { get; set; }
        public int MonthNumber { get; set; }
        public string TypeOfTask { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public string PersonId { get; set; }

        public static List<CalendarView> GetWeek(int baseDays,int from, string dayOfWeek, int to, int month, int year, IEnumerable<Person> people )
        {
            var start = from;
            var days = 0;
            var list = new List<CalendarView>();
            while (start <= to && days < baseDays)
            {
                var result = GetDateName(dayOfWeek).Split('|');
                var dayName = result[0];
                dayOfWeek = result[1];
                list.Add(new CalendarView {TypeOfTask="0",PersonId = "0",WeekName = dayName, DayNumber = (start).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), MonthName = GetNameMonth(month), MonthNumber=month,Year = year});
                start++;
                days++;
            }
            return list;
        }

        public static string GetDateName(string dayOfWeek)
        {
            if (String.Compare(DayOfWeek.Monday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Lu" + "|" + DayOfWeek.Tuesday;
            }
            if (String.Compare(DayOfWeek.Tuesday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Ma" + "|" + DayOfWeek.Wednesday;
            }
            if (String.Compare(DayOfWeek.Wednesday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Mi" + "|" + DayOfWeek.Thursday;
            }
            if (String.Compare(DayOfWeek.Thursday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Ju" + "|" + DayOfWeek.Friday;
            }
            if (String.Compare(DayOfWeek.Friday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Vi" + "|" + DayOfWeek.Saturday;
            }
            if (String.Compare(DayOfWeek.Saturday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Sa" + "|" + DayOfWeek.Sunday;
            }
            if (String.Compare(DayOfWeek.Sunday.ToString(), dayOfWeek, StringComparison.Ordinal) == 0)
            {
                return "Do" + "|" + DayOfWeek.Monday;
            }

            return "";
        }
        public static string GetNameMonth(int month)
        {
            var monthName = "";
            switch (month)
            {
                case 1:
                    monthName = "Ene"; break;
                case 2:
                    monthName = "Feb"; break;
                case 3:
                    monthName = "Mar"; break;
                case 4:
                    monthName = "Abr"; break;
                case 5:
                    monthName = "May"; break;
                case 6:
                    monthName = "Jun"; break;
                case 7:
                    monthName = "Jul"; break;
                case 8:
                    monthName = "Ago"; break;
                case 9:
                    monthName = "Sep"; break;
                case 10:
                    monthName = "Oct"; break;
                case 11:
                    monthName = "Nov"; break;
                case 12:
                    monthName = "Dic"; break;
            }
            return monthName;
        }
    }


}