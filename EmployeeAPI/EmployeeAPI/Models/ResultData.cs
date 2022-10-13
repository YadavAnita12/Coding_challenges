namespace EmployeeAPI.Models
{
    public class ResultData
    {
        public TblEmployee tblemp { get; set; }

        public ResultData()
        {

        }
        public ResultData(TblEmployee tblemp)
        {
            this.tblemp = tblemp;
        }

        }
    }

