using System.Collections.Generic;

namespace ncorep.Dtos;

public class ServiceResult
{
    public int StatusCode { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}