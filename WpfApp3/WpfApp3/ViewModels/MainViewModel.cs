using MvvmDialogs;
using log4net;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Linq;
using WpfApp3.Views;
using WpfApp3.Utils;
using WpfApp3.Models;
using System.Windows;

namespace WpfApp3.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region Parameters
        private readonly IDialogService DialogService;

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title
        {
            get { return "発表会テーブル"; }
        }
        /// <summary>
        /// ラベルテキスト
        /// </summary>
        private string _lblTxt;
        public string LblTxt
        {
            get { return _lblTxt; }
            set
            {
                _lblTxt = value;
                NotifyPropertyChanged("LblTxt");
            }
        }
        /// <summary>
        /// リスト
        /// </summary>
        private ObservableCollection<SampleModel> _listData;
        public ObservableCollection<SampleModel> ListData
        {
            get { return _listData; }
            set { _listData = value; }
        }

        /// <summary>
        /// 実際に表示させるリスト
        /// </summary>
        public List<SampleModel> ShowListData
        {
            get { return ListData.ToList().Union(new List<SampleModel>() { new SampleModel() { IsEnabled = false, } }).ToList(); }
        }
        /// <summary>
        /// 選択したリスト
        /// </summary>
        private SampleModel _selectionListData;
        public SampleModel SelectionListData
        {
            get { return _selectionListData; }
            set { _selectionListData = value; NotifyPropertyChanged("SelectionListData"); }
        }
        /// <summary>
        /// カレンダー
        /// </summary>
        private CalendarCollectionModel _calendarCollection;
        public CalendarCollectionModel CalendarCollection
        {
            get { return _calendarCollection; }
            set { _calendarCollection = value; NotifyPropertyChanged("CalendarCollection"); }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            // DialogService is used to handle dialogs
            this.DialogService = new MvvmDialogs.DialogService();
            _listData = new ObservableCollection<SampleModel>()
            {
                new SampleModel(){Name = "姫路しかま", Address="兵庫県姫路市", Age=25, Birthday=new DateTime(1974,1,12), Job="プログラマー"},
                new SampleModel(){Name = "元町もとこ", Address="兵庫県神戸市中央区", Age=45, Birthday=new DateTime(1988,1,12), Job="SE"},
            };

            SelectionListData = ListData.First();
            CalendarCollection = new CalendarCollectionModel(InputDatetime);

            #region non meaning
            // クラスの宣言
            var classlist = new List<SampleModel>()
            {
                new SampleModel(){Name = "名前１", Age = 10, Address="姫路",},
                new SampleModel(){Name = "名前２", Age = 20, Address="姫路",},
                new SampleModel(){Name = "名前３", Age = 30, Address="姫路",},
                new SampleModel(){Name = "なまえ４", Age = 40, Address="神戸",},
            };
            // 「名前」を含む人を取り出す
            var linq1 = classlist.Where(x => x.Name.Contains("名前"));
            // 年齢の合計
            var linq2 = classlist.Sum(x => x.Age);
            // 「名前：年齢：住所」で転写
            Func<SampleModel, string> formatMember = x => x.Name + "：" + x.Age.ToString() + "：" + x.Address;
            var linq3 = classlist.Select(formatMember);
            ListData.Aggregate(new ObservableCollection<SampleModel>(), (x, y) => x);
            #endregion

        }

        #endregion

        #region Methods

        #endregion

        #region Commands
        public ICommand LabelCmd { get { return new RelayCommand(OnLabelCmd, AlwaysTrue); } }

        public ICommand ShowCalendarCmd { get { return new RelayCommand<object>(OnCalendarCmd); } }

        public ICommand ListDataSelectionChangedCmd { get { return new RelayCommand<object>(OnListDataSelectionChangedCmd); } }
        /// <summary>
        /// 
        /// </summary>
        private void OnLabelCmd()
        {
            LblTxt += "bbb";
            ListData.Add(new SampleModel() { Name = "no name", Address = "六甲山のどっか", Age = 35, Birthday = new DateTime(2001, 1, 12), Job = "プログラマー" });
            NotifyPropertyChanged("ShowListData");
        }

        private void OnCalendarCmd(object parameter)
        {
            CalendarCollection.show();

            //InputDatetime.Invoke(DateTime.Now);
        }

        /// <summary>
        /// 日付の入力を行います。
        /// </summary>
        public Action<DateTime> InputDatetime
        {
            get { return (dt) => SelectionListData.Birthday = dt; }
        }

        private void OnListDataSelectionChangedCmd(object parameter)
        {
            LblTxt += "yyy";
        }

        private void SortListData(object sortKey)
        {
            switch (sortKey as string)
            {

            }
        }

        private bool AlwaysTrue() { return true; }
        private bool AlwaysFalse() { return false; }

        #endregion

        #region Events

        #endregion
    }
}
