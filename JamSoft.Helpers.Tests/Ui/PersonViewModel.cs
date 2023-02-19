using JamSoft.Helpers.Ui;

namespace JamSoft.Helpers.Tests.Ui
{
    public class PersonViewModel : IDirtyMonitoring
    {
        public string Name { get; set; }

        [IsDirtyMonitoring]
        public string Field;
    
        [IsDirtyMonitoring]
        public string DisplayName { get; set; }
        
        [IsDirtyMonitoring]
        public string Address { get; set; }

        public bool IsDirty { get; set; }
        
        public string Hash { get; set; }
        
        [IsDirtyMonitoring]
        public MyComplexObject Complex { get; set; }

        [IsDirtyMonitoring] 
        public MyComplexObject ComplexField;
    }

    public class MyComplexObject
    {
        public string ComplexProperty1 { get; set; }
        
        public string ComplexProperty2 { get; set; }
    }
}