namespace Core.Helpers
{
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
        public string Error { get; set; }
        public TItem Item { get; set; }
    }
}
