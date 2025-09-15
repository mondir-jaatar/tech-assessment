using TCLab.BuildingBlocks.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Wrappers;

public class KeyValueResponse<T> : Response<T>
{
    public int Count { get; set; }

    public KeyValueResponse()
    {

    }
    public KeyValueResponse(T data, string message = null) : base(data, message)
    {

    }

    public KeyValueResponse(string message) : base(message)
    {

    }
}