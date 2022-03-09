namespace InMemoryDatabase
{
    public enum SqliteType
    {
        unknown,
        integer, // signed integer stored in 1 to 8 bytes depending on the magnitude
        real, // floating point value, stored on 8 bytes
        text, // text string stored using database encoding (UTF-8, UTF-16BE or UTF-16LE
        blob, // blob of data, stored exactly as it was input
    }
}
