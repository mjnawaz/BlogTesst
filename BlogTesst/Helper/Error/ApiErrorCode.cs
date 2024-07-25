namespace BlogTesstAPI.Helper.Error
{
    internal enum ApiErrorCode
    {
        UNKNOWN = 0,
        NO_BLOGS_FOUND = 3001,
        NO_BLOG_AGAINST_THIS_ID = 3002,
        INVALID_REQUEST = 3003,
        INVALID_REQUEST_BODY = 3004,
        PLEASE_CONTACT_SUPPORT = 3005,
        OTHER_ERROR = 3006,
    }
}
