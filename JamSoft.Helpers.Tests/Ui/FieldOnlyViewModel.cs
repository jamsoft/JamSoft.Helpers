using JamSoft.Helpers.Ui;

namespace JamSoft.Helpers.Tests.Ui
{
    public class FieldOnlyViewModel : IDirtyMonitoring
    {
        [IsDirtyMonitoring]
        public string Field;
    
        public bool IsDirty { get; set; }
        
        public string Hash { get; set; }
    }
}