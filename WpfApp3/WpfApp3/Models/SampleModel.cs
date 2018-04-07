using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    /// <summary>
    /// サンプルモデル（リスト）
    /// </summary>
    public class SampleModel : BaseModel
    {
        private string _name;
        private int _age;
        private DateTime _birthday;
        private string _address;
        private string _job;
        private bool _isSelected;
        private bool _isEnabled = true;

        /// <summary>
        /// 名前
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        /// <summary>
        /// 年齢
        /// </summary>
        public int Age
        {
            get { return _age; }
            set { _age = value; NotifyPropertyChanged("Age"); }
        }
        /// <summary>
        /// 誕生日
        /// </summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; NotifyPropertyChanged("Birthday"); }
        }
        /// <summary>
        /// 住所
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged("Address"); }
        }
        /// <summary>
        /// 職業
        /// </summary>
        public string Job
        {
            get { return _job; }
            set { _job = value; NotifyPropertyChanged("Job"); }
        }
        /// <summary>
        /// 選択状態
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }
        /// <summary>
        /// 有効・無効
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; NotifyPropertyChanged("IsEnabled"); }
        }
    }
}
