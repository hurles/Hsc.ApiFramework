namespace Hsc.Backend.Models.Responses
{
    public interface IRequestResponse
    {
        string? Message { get; set; }
        string? Status { get; set; }
    }
}