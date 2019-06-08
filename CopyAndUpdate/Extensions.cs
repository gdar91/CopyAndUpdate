using System;
using System.Linq.Expressions;

namespace CopyAndUpdate
{
    public static class Extensions
    {
        public static TObject With<TObject, TValue>(
            this TObject target,
            Expression<Func<TObject, TValue>> memberSelector,
            TValue newValue
        )
            => WithModule.Template(target, (memberSelector, newValue));

        public static TObject With<TObject, TValue1, TValue2>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2)
            );
        
        public static TObject With<TObject, TValue1, TValue2, TValue3>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2,
            Expression<Func<TObject, TValue3>> memberSelector3,
            TValue3 newValue3
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2),
                (memberSelector3, newValue3)
            );

        public static TObject With<TObject, TValue1, TValue2, TValue3, TValue4>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2,
            Expression<Func<TObject, TValue3>> memberSelector3,
            TValue3 newValue3,
            Expression<Func<TObject, TValue4>> memberSelector4,
            TValue4 newValue4
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2),
                (memberSelector3, newValue3),
                (memberSelector4, newValue4)
            );
        
        public static TObject With<TObject, TValue1, TValue2, TValue3, TValue4, TValue5>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2,
            Expression<Func<TObject, TValue3>> memberSelector3,
            TValue3 newValue3,
            Expression<Func<TObject, TValue4>> memberSelector4,
            TValue4 newValue4,
            Expression<Func<TObject, TValue5>> memberSelector5,
            TValue5 newValue5
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2),
                (memberSelector3, newValue3),
                (memberSelector4, newValue4),
                (memberSelector5, newValue5)
            );

        public static TObject With<TObject, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2,
            Expression<Func<TObject, TValue3>> memberSelector3,
            TValue3 newValue3,
            Expression<Func<TObject, TValue4>> memberSelector4,
            TValue4 newValue4,
            Expression<Func<TObject, TValue5>> memberSelector5,
            TValue5 newValue5,
            Expression<Func<TObject, TValue6>> memberSelector6,
            TValue6 newValue6
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2),
                (memberSelector3, newValue3),
                (memberSelector4, newValue4),
                (memberSelector5, newValue5),
                (memberSelector6, newValue6)
            );

        public static TObject With<TObject, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(
            this TObject target,
            Expression<Func<TObject, TValue1>> memberSelector1,
            TValue1 newValue1,
            Expression<Func<TObject, TValue2>> memberSelector2,
            TValue2 newValue2,
            Expression<Func<TObject, TValue3>> memberSelector3,
            TValue3 newValue3,
            Expression<Func<TObject, TValue4>> memberSelector4,
            TValue4 newValue4,
            Expression<Func<TObject, TValue5>> memberSelector5,
            TValue5 newValue5,
            Expression<Func<TObject, TValue6>> memberSelector6,
            TValue6 newValue6,
            Expression<Func<TObject, TValue7>> memberSelector7,
            TValue7 newValue7
        )
            => WithModule.Template(
                target,
                (memberSelector1, newValue1),
                (memberSelector2, newValue2),
                (memberSelector3, newValue3),
                (memberSelector4, newValue4),
                (memberSelector5, newValue5),
                (memberSelector6, newValue6),
                (memberSelector7, newValue7)
            );
    }
}
