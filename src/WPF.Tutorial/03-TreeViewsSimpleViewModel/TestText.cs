using PropertyChanged;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WpfTreeView
{
    /// <summary>
    /// 
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class TestText:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public string ButtonText { get; set; }

        public TestText()
        {
            Task.Run(async () => 
            {
                while(true)
                {
                    await Task.Delay(1000);
                    ButtonText = $"Current Time:{DateTime.Now.ToString("yyyy-HH-dd hh:mm:ss")}";
                }
            });
        }
    }
}
