using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp3.Utils;

namespace WpfApp3.Models
{
    /// <summary>
    /// カレンダー表示用モデル
    /// </summary>
    public class CalendarCollectionModel : BaseModel
    {
        private DateTime _yearMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private ObservableCollection<CalendarModel> _monthCalendar = new ObservableCollection<CalendarModel>();
        private Visibility _calendarVisible;
        /// <summary>
        /// 日付を変更するアクション
        /// </summary>
        private Action<DateTime> changeMethd;

        public CalendarCollectionModel(Action<DateTime> changeMethod)
        {
            CalendarVisible = Visibility.Collapsed;
            this.changeMethd = changeMethod;
        }

        /// <summary>
        /// カレンダーを表示する
        /// </summary>
        /// <param name="ymd"></param>
        public void show(DateTime? ymd = null)
        {
            YearMonth = (DateTime)(ymd ?? DateTime.Now);
            setCalendar();
            CalendarVisible = Visibility.Visible;
        }

        /// <summary>
        /// 隠す
        /// </summary>
        public void hide()
        {
            CalendarVisible = Visibility.Collapsed;
        }

        /// <summary>
        /// カレンダー可視不可視
        /// </summary>
        public Visibility CalendarVisible
        {
            get { return _calendarVisible; }
            private set { _calendarVisible = value; NotifyPropertyChanged("CalendarVisible"); }
        }
        /// <summary>
        /// 年月
        /// </summary>
        public DateTime YearMonth
        {
            get { return _yearMonth; }
            set
            {
                _yearMonth = value;
                new List<string>() { "YearMonth", "Year", "Month" }.ForEach(x => NotifyPropertyChanged(x));
            }
        }
        /// <summary>
        /// カレンダー年
        /// </summary>
        public int Year
        {
            get { return _yearMonth.Year; }
        }
        /// <summary>
        /// カレンダー月
        /// </summary>
        public int Month
        {
            get { return _yearMonth.Month; }
        }
        /// <summary>
        /// 月別カレンダー
        /// </summary>
        public ObservableCollection<CalendarModel> MonthCalendar
        {
            get { return _monthCalendar; }
            set { _monthCalendar = value; }
        }


        #region Commands
        public ICommand ToggleMonthCommand { get { return new RelayCommand<object>(OnToggleMonthCommand); } }
        /// <summary>
        /// 月切り替えコマンド
        /// </summary>
        private void OnToggleMonthCommand(object parameter)
        {
            int i;
            int.TryParse(parameter.ToString(), out i);
            YearMonth = YearMonth.AddMonths(i);
            setCalendar();
        }

        public ICommand SelectDayCmd { get { return new RelayCommand<object>(OnSelectDayCmd); } }
        /// <summary>
        /// 日付選択コマンド
        /// </summary>
        /// <param name="parameter"></param>
        private void OnSelectDayCmd(object parameter)
        {
            hide();
            this.changeMethd.Invoke(new DateTime(Year, Month, (int)parameter));
        }
        #endregion
        /// <summary>
        /// カレンダーの日付一覧をセットする
        /// </summary>
        private void setCalendar()
        {
            // 指定月のカレンダーの日付一覧を取得する
            MonthCalendar.Clear();
            var lastDay = new DateTime(YearMonth.AddMonths(1).Year, YearMonth.AddMonths(1).Month, 1).AddDays(-1).Day;
            Enumerable.Range(1, lastDay).ToList().ForEach(x => MonthCalendar.Add(new CalendarModel(new DateTime(Year, Month, x))));
        }
    }
}
