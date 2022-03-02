namespace Hsc.ApiFramework.Models.Responses
{
    public interface IRequestResponse
    {
        string? Message { get; set; }
        string? Status { get; set; }
    }
}