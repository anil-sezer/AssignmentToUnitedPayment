namespace UPay.Domain.Helpers;

public static class EnumHelper // todo: Move to Utilities project
{
    public static string GetEnumValuesString<TEnum>() // todo: Is it possible to make this an Enum extension method?
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new ArgumentException("The type parameter must be an enum type.");
        }

        var enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));
        var enumValueStrings = Array.ConvertAll(enumValues, x => $"{x.ToString()}({Convert.ToInt32(x)})");

        return string.Join(", ", enumValueStrings);
    }
}