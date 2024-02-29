using Lab1.Utils;

namespace Lab1.Models;

public class User : BaseBindable
{
    private DateTime _birthDate;
    private int _age;
    private bool _isBirthdayToday;
    private ChineseZodiac _chineseZodiac;
    private WesternZodiac _westernZodiac;

    public User(DateTime birthDate)
    {
        BirthDate = birthDate;
    }

    public User() : this(DateTime.Today)
    {
        
    }
    
    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (UpdatePropertyValidated(ref _birthDate, value, ValidateBirthDate, nameof(BirthDate)))
            {
                Age = CalculateAge(BirthDate);
                IsBirthdayToday = CalculateIsBirthdayToday(BirthDate);
                ChineseZodiac = ChineseZodiacExtension.FromDate(BirthDate);
                WesternZodiac = WesternZodiacExtension.FromDate(BirthDate);
            }
        }
    }

    public int Age
    {
        get => _age;
        private set => UpdateProperty(ref _age, value, nameof(Age));
    }

    public bool IsBirthdayToday
    {
        get => _isBirthdayToday;
        private set => UpdateProperty(ref _isBirthdayToday, value, nameof(IsBirthdayToday));
    }

    public ChineseZodiac ChineseZodiac
    {
        get => _chineseZodiac;
        private set => UpdateProperty(ref _chineseZodiac, value, nameof(ChineseZodiac));
    }

    public WesternZodiac WesternZodiac
    {
        get => _westernZodiac;
        private set => UpdateProperty(ref _westernZodiac, value, nameof(WesternZodiac));
    }
    
    private void ValidateBirthDate()
    {
        ClearPropertyErrors(nameof(BirthDate));
        var age = CalculateAge(BirthDate);
        if (age < 0)
            AddPropertyError(nameof(BirthDate), "User age can't be negative");
        if (age >= 135)
            AddPropertyError(nameof(BirthDate), "User age must be less than 135 years");
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate.AddYears(age) > today)
            --age;

        return age;
    }

    private static bool CalculateIsBirthdayToday(DateTime birthDate)
    {
        var today = DateTime.Today;

        return birthDate.Month == today.Month && birthDate.Day == today.Day;
    }
}
