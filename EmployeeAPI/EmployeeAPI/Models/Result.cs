namespace EmployeeAPI.Models
{
    public class Result
    {

        public int StatusCode { get; set; }

        public string StringContext { get; set; }


        public ResultData resultData { get; set; }

        public string message { get; set; }


        public Result(int StatusCode, string StringContext, ResultData resultData, string message)
        {
            this.message = message;
            this.resultData= resultData;
            this.StatusCode = StatusCode;
            this.StringContext = StringContext;

        }
    }
}
