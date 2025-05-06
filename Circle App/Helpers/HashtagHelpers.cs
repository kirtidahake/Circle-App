using System.Text.RegularExpressions;

namespace Circle_App.Helpers
{
    public static class HashtagHelpers
    {
        public static List<string> GetHashtage(string postContent)
        {
            var hashtagPattern = new Regex(@"#\w+");
            var matches = hashtagPattern.Matches(postContent)
                .Select(matches => matches.Value.TrimEnd(',', ',', '!', '?'))
                .Distinct()
                .ToList();
            return matches;
        }
    }
}
