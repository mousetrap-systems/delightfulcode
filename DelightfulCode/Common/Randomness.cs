using System.Text;

namespace DelightfulCode
{
    /// <summary>
    /// Basic tools for key generation.
    /// </summary>
    public static class Randomness
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// Generate a unique key of specific size based on DateTime and GUID,
        /// also utilizes scrambling for extra randomness.
        /// NOTE: This signature uses only hexadecimal values.
        /// </summary>
        /// <param name="size">The length of the key to be generated.</param>
        /// <param name="include_letters">if you need specific characters to be included in the final output, list them here.</param>
        /// <returns>A unique key of the specified size.</returns>
        public static string GenerateKey(int size)
        {
            string unique_key = DateTime.Now.Day.ToString("X2") + DateTime.Now.Second.ToString("X2") + DateTime.Now.Minute.ToString("X2")
                                + DateTime.Now.Millisecond.ToString("X2") + Guid.NewGuid().ToString().GetHashCode().ToString("X2");

            unique_key = Randomness.ScrambleString(unique_key);

            // To prevent 'substring' from breaking, if the key (for whatever reason) ends up being shorter, make it safe.

            if (size > unique_key.Length)
                size = unique_key.Length;

            unique_key = unique_key.Substring(0, size);

            return unique_key.ToLowerInvariant();
        }

        /// <summary>
        /// Generate a COMB (Combined Guid and Timestamp) GUID that is sequentially sortable.
        /// </summary>
        /// <remarks>
        /// This method returns a GUID where the first part is replaced by the current timestamp in hexadecimal format.
        /// This ensures lexicographic sortability while maintaining uniqueness. This is particularly useful in storage
        /// mechanisms like Cosmos DB where GUIDs are often used as keys to improve performance by minimizing page splits.
        /// 
        /// Example of generated COMB: "61df48d0-aaa2-4eda-9b7f-3f33f5d1e489"
        /// </remarks>
        /// <returns>A new COMB GUID that is sequentially sortable.</returns>
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.UtcNow;

            // Calculate the days and milliseconds for the byte string
            TimeSpan days = now.Subtract(baseDate);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to bytes and reverse to match SQL Server's ordering
            byte[] daysArray = BitConverter.GetBytes(days.Days).Reverse().ToArray();
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333)).Reverse().ToArray();

            // Copy the bytes into the guid
            Buffer.BlockCopy(daysArray, 0, guidArray, 0, 2);
            Buffer.BlockCopy(msecsArray, 0, guidArray, 2, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// This method returns GUIDs sorted in reverse chronological order when viewing in A-Z.
        /// The most recently-created GUIDs will appear first in the listing. This can be handy when log records use a GUID.
        /// </summary>
        /// <returns>A new Reverse COMB GUID that is sequentially sortable in reverse.</returns>
        [Author("GPT-4", 2024)]
        public static Guid GenerateCombReverse()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);  // Furthest point into the future
            DateTime now = DateTime.UtcNow;

            // Calculate the days and milliseconds for the byte string
            TimeSpan days = baseDate.Subtract(now);
            TimeSpan msecs = baseDate.Subtract(now);

            // Convert to bytes and reverse to match SQL Server's ordering
            byte[] daysArray = BitConverter.GetBytes(days.Days).Reverse().ToArray();
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333)).Reverse().ToArray();

            // Copy the bytes into the guid
            Buffer.BlockCopy(daysArray, 0, guidArray, 0, 2);
            Buffer.BlockCopy(msecsArray, 0, guidArray, 2, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// Takes a string input and scrambles its characters randomly.
        /// Useful for generation of keys.
        /// </summary>
        /// <param name="input">The string whose characters need to be scrambled.</param>
        /// <returns>A string that is a scrambled version of the input.</returns>
        /// <summary>
        public static string ScrambleString(string input)
        {
            // Use the include_letters string if provided, otherwise use hexadecimal characters

            // string allowedChars = include_letters ?? "0123456789abcdef";
            char[] array = input.ToCharArray();
            var scrambledArray = Scramble<char>(array);
            return new string(scrambledArray);
        }

        /// <summary>
        /// Shuffles an array of items in-place.
        /// </summary>
        /// <param name="items">The array to be shuffled.</param>
        /// <returns>The array inputted but shuffled.</returns>
        public static T[] Scramble<T>(T[] items)
        {
            // For each spot in the array, pick a random item to swap into that spot.
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
            return items;
        }

        /// <summary>
        /// Generates a random 25-character product key, in the format 'XXXXX-XXXXX-XXXXX-XXXXX-XXXXX'.
        /// The key consists of 5 groups of 5 characters each, separated by hyphens. 
        /// The total length of the key, including hyphens, is 29 characters.
        /// </summary>
        /// <returns>A string representing the generated product key. For example, "ABC12-DE34F-GH567-IK89J-LM01N".</returns>
        [Author("GPT-4", 2024)]
        [Health(CodeStability.Experimental)]
        public static string GenerateProductKey()
        {
            // Using StringBuilder to efficiently append strings
            StringBuilder productKeyBuilder = new StringBuilder();
            Random random = new Random();

            // Loop to create 5 groups of characters
            for (int groupIndex = 0; groupIndex < 5; groupIndex++)
            {
                // Loop to create 5 characters within a group
                for (int charIndex = 0; charIndex < 5; charIndex++)
                {
                    int randomValue = random.Next(0, 36);

                    // If randomValue is less than 10, append it as a numeral
                    if (randomValue < 10)
                    {
                        productKeyBuilder.Append(randomValue);
                    }
                    // Else, convert it to a character (A-Z)
                    else
                    {
                        char character = Convert.ToChar('A' + randomValue - 10);
                        productKeyBuilder.Append(character);
                    }
                }

                // Append hyphen after each group, except the last one
                if (groupIndex < 4)
                {
                    productKeyBuilder.Append('-');
                }
            }

            // Return the generated product key
            return productKeyBuilder.ToString();
        }
    }
}
