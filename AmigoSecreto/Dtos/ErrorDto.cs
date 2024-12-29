using System.Net;
using FluentValidation.Results;

namespace AmigoSecreto.Dtos;

public class ErrorDto
{
    public ErrorDto(
                    string type = "about:blank", 
                    string title = "Ocorreu um erro inesperado", 
                    string detail = "Ocorreu um erro inesperado.", 
                    HttpStatusCode status = HttpStatusCode.InternalServerError, 
                    ICollection<ValidationFailure>? moreDetails = null)
    {
        Type = type;
        Title = title;
        Detail = detail;
        Status = status;
        MoreDetails = moreDetails is not null 
                        ? moreDetails
                                        .Select(validationFailure => new DetailDto
                                        {
                                                        Name = validationFailure.PropertyName, 
                                                        Reason = validationFailure.ErrorMessage
                                        }).ToList() 
                        : [];
    }

    public static ErrorDto CreatedError400(ICollection<ValidationFailure>? moreDetails = null)
    {
        return new ErrorDto
        {
                        Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                        Status = HttpStatusCode.BadRequest,
                        Title = "Bad Request",
                        Detail = "Dados inválidos.",
                        MoreDetails = moreDetails is not null 
                                        ? moreDetails
                                                        .Select(validationFailure => new DetailDto
                                                        {
                                                                        Name = validationFailure.PropertyName, 
                                                                        Reason = validationFailure.ErrorMessage
                                                        }).ToList() 
                                        : [],
        };
    }
    
    public static ErrorDto CreatedError404(ICollection<ValidationFailure>? moreDetails = null)
    {
        return new ErrorDto
        {
                        Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
                        Status = HttpStatusCode.NotFound,
                        Title = "Not Found",
                        Detail = "Recurso não encontrado.",
                        MoreDetails = moreDetails is not null 
                                        ? moreDetails
                                                        .Select(validationFailure => new DetailDto
                                                        {
                                                                        Name = validationFailure.PropertyName, 
                                                                        Reason = validationFailure.ErrorMessage
                                                        }).ToList() 
                                        : [],
        };
    }
    //https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1
    
    public string Type { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public HttpStatusCode Status { get; set; }
    public ICollection<DetailDto> MoreDetails { get; set; }
}