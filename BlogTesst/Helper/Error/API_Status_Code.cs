namespace BlogTesstAPI.Helper.Error
{
    public enum API_Status_Code
    {
        Accepted = 202,
        AlreadyReported = 208,
        Ambiguous = 300,
        BadGateway = 502,
        BadRequest = 400,
        Conflict = 409,
        Continue = 100,
        Created = 201,
        Forbidden = 403,
        Locked = 423,
        NotAcceptable = 406,
        NotExtended = 510,
        NotModified = 304,
        OK = 200,
        Redirect = 302,
        NotFound = 404,
        InternalServerError = 500
    }
}
