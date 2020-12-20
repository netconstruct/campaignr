namespace Campaignr.Core.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = false;

        public string ResponseMessage { get; set; }
    }
}