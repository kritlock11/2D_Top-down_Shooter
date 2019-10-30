using System.Text;
using UnityEngine;

namespace Shooter_2D_test
{
    public static partial class Extensions
    {
        public static string TextReBuilder(this StringBuilder builder, int contextLength, float value)
        {
            builder.Remove(contextLength, builder.Length - contextLength);
            builder.Append($"{value}");
            return builder.ToString();
        }

        public static string TextClearBuilder(this StringBuilder builder, int a, int b)
        {
            builder.Length = 0;
            builder.Capacity = 0;
            builder.Append($"{a} / {b}");
            return builder.ToString();
        }

        public static string TextStringBuilder(this StringBuilder builder, string a)
        {
            builder.Length = 0;
            builder.Capacity = 0;
            builder.Append($"{a}");
            return builder.ToString();
        }

    }
}
