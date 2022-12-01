namespace Together.Core.Helpers
{
    public static class GuidHelper
    {
        public static Guid NewGuid(string guid = null)
        {
            return string.IsNullOrWhiteSpace(guid) ? Guid.NewGuid() : new Guid(guid);
        }
    }
}
