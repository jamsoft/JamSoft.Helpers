using JamSoft.Helpers.Ui;

namespace JamSoft.Helpers.Tests.Ui
{
    public class HashPropertyMonitoringViewModel : IDirtyMonitoring
    {
        [IsDirtyMonitoring]
        public string MyProp { get; set; }
    
        public bool IsDirty { get; set; }
     
        [IsDirtyMonitoring]
        public string Hash { get; set; }
    }
}