using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp3.Models
{
    public class CalendarModel : BaseModel
    {
        private DateTime _date;
        private int _rand;

        public CalendarModel(DateTime dt)
        {
            Date = dt;
            Rand = new Random((int)DateTime.Now.Ticks).Next();
        }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                new List<string>() { "Date", "DayName", "TextColor", "TextBackground" }.ForEach(x => NotifyPropertyChanged(x));
            }
        }

        /// <summary>
        /// 日
        /// </summary>
        public int DayName
        {
            get { return Date.Day; }
        }
        /// <summary>
        /// 日付テキスト色
        /// </summary>
        public Brush TextColor
        {
            get
            {
                switch (Date.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        return new SolidColorBrush(Colors.LightBlue);
                    case DayOfWeek.Sunday:
                        return new SolidColorBrush(Colors.Red);
                    default:
                        return new SolidColorBrush(Colors.Black);
                }
            }
        }
        /// <summary>
        /// 日付背景色
        /// </summary>
        public Brush TextBackground
        {
            get
            {
                if (DateTime.Now > Date) return new SolidColorBrush(Colors.LightGray);
                else return new SolidColorBrush(Colors.White);
            }
        }

        public int Rand
        {
            get { return _rand; }
            set { _rand = value; NotifyPropertyChanged("Rand"); }
        }
    }
}
