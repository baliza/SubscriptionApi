namespace Core.Helpers
{
    public class SimpleTrueFalseActionResult
    {
        public SimpleTrueFalseActionResult()
        {
            Succeeded = true;
        }

        public SimpleTrueFalseActionResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Succeeded = false;
        }

        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ActionResult<TResult, TItem>
    {
        public ActionResult(TResult result)
        {
            Result = result;
        }

        public ActionResult(TResult result, TItem item)
        {
            Result = result;
            Item = item;
        }

        public TResult Result { get; set; }
        public string ErrorMessage { get; set; }
        public TItem Item { get; set; }
    }
}
