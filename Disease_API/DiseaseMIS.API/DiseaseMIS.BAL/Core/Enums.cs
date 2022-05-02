namespace DiseaseMIS.BAL.Core
{
    public static class Enums
    {
        public enum ReportFlags
        {
            Active = 1,
            Locked = 0,
            InProgress = 2,
        }

        public enum Status
        {
            Active = 1,
            Archived = 0
        }

        public enum FormsType
        {
            Infectious_Incidence = 1,
            Infectious_Mortality = 2,
            NonInfectious_Incidence = 3,
            NonInfectious_Mortality = 4,
            Laboratory = 5,
        }

    }
}
