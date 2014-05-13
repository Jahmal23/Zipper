
namespace Zipper.Helpers
{
    public static class Utils
    {

        //Takes in a string of the form NNNNNNN and transforms into NNN-NNN-NNNN
        public static string FormatPhoneNumber(string dirtyNum)
        {
            string formatted = dirtyNum;
            if (!string.IsNullOrWhiteSpace(dirtyNum))
            {
                try
                {
                    formatted = string.Format("{0:(###) ###-####}", double.Parse(dirtyNum));
                }
                catch
                {
                    ;//do nothing because we set formatted to dirtyNum above.
                }
            }

            return formatted;
        }
    }

}

    