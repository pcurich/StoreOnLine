using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class ScheduleTurn :DataBaseId
    {
        public string ScheduleTurnCode { get; set; }
        public string ScheduleTurnName { get; set; }

        public ScheduleTurn()
        {
        }

        public ScheduleTurn(int id, string code, string name)
        {
            ScheduleTurnCode = code;
            ScheduleTurnName = name;
            Id = id;
        }
    }
}
